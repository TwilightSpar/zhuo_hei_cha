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
        await Clients.All.SendAsync("showErrorMessage", "Someone disconnected from the game. The game will be restarted in 5 seconds");
        await Task.Delay(5000);
        await Clients.All.SendAsync("BreakGameFrontend");
    }
}

// a => b => client
// client => b => a

// a => b => client
// test =>?b => ?a

// A (server) => A (client) => Return (server)
// Return (server) => A (client) => A (server)