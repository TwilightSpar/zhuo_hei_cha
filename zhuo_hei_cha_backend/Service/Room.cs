using System.Collections.Generic;
using System.Threading.Tasks;

public class Room
{
    public static Room activeRoom; 

    int roomId;
    private List<Player> _playerList;
    public IReadOnlyCollection<Player> PlayerList { get{ return _playerList.AsReadOnly(); } }
    Game activeGame;

    public Room()
    {
        _playerList = new List<Player>();
    }

    public void AddPlayer(Player p)
    {
        _playerList.Add(p);
    }

    public static async Task AskPlayOneMoreRound()
    {
        await BackToFront.AskPlayOneMoreRoundBackend();
    }

    public bool CanStartGame()
    {
        // return true;
        return _playerList.Count >= 3 && _playerList.Count <= 5;
    }

    public async void StartGame()
    {
        activeGame = new Game(_playerList);
        await activeGame.GameProcess();
    }
}