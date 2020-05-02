using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerHub: Hub
{
    public void CreatePlayerBackend(string name, string connectionId)
    {
        // connectionid and other attributes of player
        Player player = new Player(name, connectionId);
        Room.activeRoom.AddPlayer(player);
    }

    public void StartGameBackend()
    {
        // frontend do:
        // do some check
        // Room.activeRoom.playerList.Count

        Room.activeRoom.StartGame();
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