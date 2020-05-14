using Microsoft.AspNetCore.SignalR;
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

        return player;
    }

    public async void StartGameBackend()
    {
        if (Room.activeRoom.CanStartGame())
        {
            // await here?
            Clients.All.SendAsync("NotifyOthersFrontend");
            BackToFront.clients = Clients;
            // no need to wait here?
            Room.activeRoom.StartGame();
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

    public async Task ReturnUserHandBackend(List<string> cards)
    {
        var formattedCards = cards.Select(cardString => new Card(cardString)).ToList();
        PlayerHubTempData.userHand = formattedCards;
        PlayerHubTempData.finishPlay = true;
    }
    public static void ReturnTributeBackend(List<Card> cards)
    {
        PlayerHubTempData.returnCards = cards;
    }

    public void ReturnPlayOneMoreTimeBackend(bool returnvalue)
    {
        PlayerHubTempData.playOneMoreTime = returnvalue;
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

}

// a => b => client
// client => b => a

// a => b => client
// test =>?b => ?a

// A (server) => A (client) => Return (server)
// Return (server) => A (client) => A (server)