using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class Room
{
    public static Room activeRoom = new Room();

    int id;
    List<Player> playerList;
    Game activeGame;

    // public Room()
    // {
    //     playerList = new List<Player>();
    // }
    public Room()
    {
        // _hubContext = hubContext;
        playerList = new List<Player>();
    }

    public void AddPlayer(Player p)
    {
        playerList.Add(p);
    }

    public void AskPlayOneMoreRound()
    {
        BackToFront.AskPlayOneMoreRoundBackend();       
    }

    public void StartGame()
    {
        activeGame = new Game(playerList);
        activeGame.GameProcess();
    }
}