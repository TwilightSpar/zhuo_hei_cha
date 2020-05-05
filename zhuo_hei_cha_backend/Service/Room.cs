using System.Collections.Generic;

public class Room
{
    public static Room activeRoom; 

    int id;
    List<Player> playerList;
    Game activeGame;

    public Room()
    {
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

    public bool CanStartGame()
    {
        // return true;
        return playerList.Count >= 3 && playerList.Count <= 5;
    }

    public async void StartGame()
    {
        activeGame = new Game(playerList);
        await activeGame.GameProcess();
    }
}