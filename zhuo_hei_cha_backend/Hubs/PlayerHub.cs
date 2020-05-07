using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

public class PlayerHub: Hub
{
    public Player CreatePlayerBackend(string name)
    {
        // connectionid and other attributes of player
        Player player = new Player(name, Clients.Caller);
        if (Room.activeRoom == null)
        {
            Room.activeRoom = new Room();
        }
        Room.activeRoom.AddPlayer(player);

        // return the created Player object to frontend
        return player;
    }

    public async void StartGameBackend()
    {
        if (Room.activeRoom.CanStartGame())
        {
            // await here?
            Clients.All.SendAsync("startGameFrontend");
            BackToFront.clients = Clients;
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


}

// a => b => client
// client => b => a

// a => b => client
// test =>?b => ?a

// A (server) => A (client) => Return (server)
// Return (server) => A (client) => A (server)