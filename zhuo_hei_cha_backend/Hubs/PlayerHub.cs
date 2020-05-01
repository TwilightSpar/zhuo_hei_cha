using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerHub: Hub
{
    public void CreatePlayer(string name)
    {
        Player player = new Player(name);
        Room.activeRoom.AddPlayer(player);
    }

    public void StartGame()
    {
        // do some check
        // Room.activeRoom.playerList.Count

        Room.activeRoom.StartGame();
    }
    public async void AskForPlay()
    {
        await Clients.Caller.SendAsync("AskForPlay");
    }

    public static void GetUserHand(List<Card> cards)
    {
        PlayerHubTempData.userHand = cards;
    }

    public void Return(bool returnvalue)
    {
        Room.activeRoom.playOneMore = returnvalue;
        // Room.activeRoom.isPlayOneMoreSet = true;   
    }

}

// a => b => client
// client => b => a

// a => b => client
// test =>?b => ?a

// A (server) => A (client) => Return (server)
// Return (server) => A (client) => A (server)