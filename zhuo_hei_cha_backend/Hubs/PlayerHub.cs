using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerHub: Hub
{
    public async void CreatePlayerBackend(string name)
    {
        // connectionid and other attributes of player
        Player player = new Player(name, Clients.Caller);
        if (Room.activeRoom == null)
        {
            Room.activeRoom = new Room();
        }
        Room.activeRoom.AddPlayer(player);

        // no need to wait here?
        await Clients.Caller.SendAsync("enterRoom");
    }

    public void StartGameBackend()
    {
        // frontend do:
        // do some check
        // Room.activeRoom.playerList.Count

        Room.activeRoom.StartGame();
    }

    public async void StartGame()
    {
        if (Room.activeRoom.CanStartGame())
        {
            // no need to wait here?
            Room.activeRoom.StartGame();
        }
        else
        {
            await Clients.Caller.SendAsync("createErrorMessage", "Cannot start the game!");
        }
    }

    public static void ReturnUserHandBackend(List<Card> cards)
    {
        PlayerHubTempData.userHand = cards;
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


}

// a => b => client
// client => b => a

// a => b => client
// test =>?b => ?a

// A (server) => A (client) => Return (server)
// Return (server) => A (client) => A (server)