using System.Collections.Generic;

public class Game
{
    private static readonly Hand EMPTY_HAND = new Hand(new List<Card>(){});
    List<Player> playerList;
    bool isGameStarted;

    Hand lastHand = EMPTY_HAND;

    int dealer = 0;

    int playerIndex = 0;

    // order of finishing
    List<Player> tributeList;

    List<Player> publicBlackAceList;

    private void InitPlayer() {}

    // add AnnounceBlackACE button
    private void InitCardList() {}	// shuffle the deck and distribute to players
    
    /// <summary>
    /// if isActivated is false, stop the game and compute level and tribute and so on
    /// put the remaining players onto the tributeList
    /// </summary>
    private void Stop() {}

    /// <summary>
    /// group tributeList according to Black ACEs
    /// call tribute in order
    /// refer to public ace list
    /// </summary>
    private void PayTribute() {}	// change tribute cards

    /// <summary>
    /// call returnTribute in reverse order
    /// </summary>
    private void ReturnTribute() {}	// return tribute cards

    // get invoked if player decided to announce Ace before game starts
    // add to public ace list
    private void AceGoPublic() {}	

    /// <summary>
    /// Ask players whether play or not 
    /// call player.HasCard, and if true, add player to tributeList
    /// 
    /// </summary>
    /// <param name="playerId"></param>
    private void AskForPlay(int playerId)
    {
        // update dealer
        // update lastHand
    }

    private bool isGameEnd() {return true;}

    public void GameProcess()
    {
        InitPlayer();
        while (true)
        {
            InitCardList();
            PayTribute();
            ReturnTribute();
            isGameStarted = true;

            while (!isGameEnd())
            {
                if (dealer == playerIndex)
                    lastHand = EMPTY_HAND;
                
                AskForPlay(playerIndex);
                playerIndex = (playerIndex + 1) % playerList.Count;
            }

            isGameStarted = false;

            // decide whether to play one more round here
            // break;
        }

    }	// PayTribute, ReturnTribute, AskForAce, AceGoPublic, start AskForPlay(id) by turns and check whether the game is stoped.
}