using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PlayerHub: Hub
{
    public Player CreatePlayerBackend(string name)
    {
        Player player = new Player(name, Clients.Caller, Context.ConnectionId);
        if (Room.activeRoom == null)
        {
            Room.activeRoom = new Room();
        }
        Room.activeRoom.AddPlayer(player);

        Console.WriteLine("createplayer"+name);
        return player;
    }

    public async void StartGameBackend()
    {
        if (Room.activeRoom.CanStartGame())
        {
            Console.WriteLine("can start game");
            // await here?
            BackToFront.clients = Clients;
            BackToFront.NotifyOthersBackend();
            // no need to wait here?
            Room.activeRoom.StartGame();
            Console.WriteLine("finish start game");
        }
        else
        {
            await Clients.Caller.SendAsync("showErrorMessage", "Cannot start the game!");
        }
    }

    public List<object> GetAllPlayers()
    {
        return Room.activeRoom.PlayerList.Select((o)=>new {
            connectionId = o.ConnectionId,
            name = o.Name
        }).ToList<object>();
    }

    public void ReturnUserHandBackend(List<string> cards)
    {
        var formattedCards = cards.Select(cardString => new Card(cardString)).ToList();
        PlayerHubTempData.userHand = formattedCards;
        PlayerHubTempData.finishPlay = true;
    }
    public void ReturnTributeBackend(List<string> cards)
    {
        var formattedCards = cards.Select(cardString => new Card(cardString)).ToList();
        PlayerHubTempData.returnCards = formattedCards;
        PlayerHubTempData.finishTribute = true;
    }

    public void ReturnPlayOneMoreRoundBackend(bool returnvalue)
    {
        PlayerHubTempData.playOneMoreRound &= returnvalue;
    }
    public void ReturnAceGoPublicBackend(bool returnvalue)
    {
        if(returnvalue)
            PlayerHubTempData.aceGoPublic = returnvalue;
    }
    public string getMyConnectionId()
    {
        return Context.ConnectionId;
    }
    public void showAceIdPlayerListBackend()
    {
        BackToFront.showAceIdPlayerListBackend(Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(System.Exception exception)
    {
        Console.WriteLine("disconnected");
        Console.WriteLine();
        Room.activeRoom = new Room();
        await Clients.All.SendAsync("showErrorMessage", "Someone disconnected from the game. The game will be restarted in 5 seconds");
        await Task.Delay(5000);
        await Clients.All.SendAsync("BreakGameFrontend");
        
        // we should reinit some static variable and static class. Because they are not reset into default 
        // value when the connection is stopped.
        PlayerHubTempData.reinitTempData();
    }
}

// a => b => client
// client => b => a

// a => b => client
// test =>?b => ?a

// A (server) => A (client) => Return (server)
// Return (server) => A (client) => A (server)