using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class Room
{
    public static Room activeRoom = new Room();

    int id;
    List<Player> playerList;
    Game activeGame;

    private IHubContext<PlayerHub> _hubContext;

    public bool playOneMore;

    public Room()
    {
        playerList = new List<Player>();
    }
    // public Room(IHubContext<PlayerHub> hubContext)
    // {
    //     _hubContext = hubContext;
    //     playerList = new List<Player>();
    // }

    public void AddPlayer(Player p)
    {
        playerList.Add(p);
    }

    public async Task<bool> AskPlayOneMoreRound()
    {
        // await _hubContext.Clients.All.SendAsync("AskPlayOneMoreRound");

        return playOneMore;
    }

    public void StartGame()
    {
        activeGame = new Game();
        activeGame.GameProcess();
    }
}