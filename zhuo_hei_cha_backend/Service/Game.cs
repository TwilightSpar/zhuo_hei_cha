using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class Game
{
    private static readonly Hand EMPTY_HAND = new Hand(new List<Card>() { });
    List<Player> playerList;
    List<Player> tributeList;
    List<Player> stillPlay;     // prepare for checkEnded
    // int playerLeft = 0;         // prepare for checkEnded
    bool isGameStarted;

    Hand lastHand = EMPTY_HAND;

    int dealerIndex = 0;

    int playerIndex = 0;

    // order of finishing
    List<Player> publicBlackAceList;

    public Game(List<Player> playerList)
    {
        this.playerList = playerList;
    }


    // add AnnounceBlackACE button
    private void InitCardList()	// shuffle the deck and distribute to players
    {
        List<Card> wholeCard = new List<Card> { };
        for (int i = 0; i < 54; i++)
        {
            wholeCard.Add(new Card(i));
        }
        Shuffle<Card>(wholeCard);

        var deckForUser = wholeCard.Select((s, i) => new { s, i })
                    .GroupBy(x => x.i % playerList.Count)
                    .Select(g => g.Select(x => x.s).ToList())
                    .ToList();
        for (int i = 0; i < playerList.Count; i++)
            playerList[i].AddCards(deckForUser[i]);
    }

    private void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


    /// <summary>
    /// group tributeList according to Black ACEs
    /// call tribute in order
    /// refer to public ace list
    /// </summary>
    private void PayTribute()
    {

    }

    /// <summary>
    /// call returnTribute in reverse order
    /// </summary>
    private void ReturnTribute()
    {

    }

    // get invoked if player decided to announce Ace before game starts
    // add to public ace list
    private void AceGoPublic()
    {

    }

    /// <summary>
    /// Ask players whether play or not 
    /// call player.HasCard, and if true, add player to tributeList
    /// 
    /// </summary>
    /// <param name="playerId"></param>


    private async void AskForPlay()
    {
        bool valid = false;
        while (!valid)
        {
            PlayerHub ph = new PlayerHub();
            // front-end invoke this method
            string userName = "find username by playerIndex";
            await ph.Clients.User(userName).SendAsync("AskForPlay");
            List<Card> userHand = PlayerHubTempData.userHand;

            if (userHand.Count == 0)
                return;

            if (playerList[playerIndex].PlayHand(userHand, this.lastHand))
            {
                // tell user that the hand is valid and update their cardInHand
                dealerIndex = playerIndex;
                lastHand = new Hand(userHand);
                valid = true;
            }
            else
            {
                // tell user that the hand is not greater than the lastHand or is not valid
            }

        }

    }

    public void checkEnded()
    {
        int remainingGroupCount = stillPlay.Select(x => x.isBlackAce()).GroupBy(x => x).Count();
        if (remainingGroupCount == 1)
        {
            tributeList.AddRange(stillPlay);
            stillPlay.Clear();
            this.isGameStarted = false;
            return;
        }
        Player p = playerList[playerIndex];
        if (p.isFinished())
        {
            tributeList.Add(p);
            stillPlay.Remove(p);
        }
    }

    public async void GameProcess()
    {
        while (true)
        {
            InitCardList();
            // not needed for the first round
            PayTribute();
            ReturnTribute();
            tributeList = new List<Player> { };   // init tributeList
            isGameStarted = true;
            stillPlay = playerList.Select(x => x).ToList();
            AceGoPublic();

            while (isGameStarted)   // skip means hand are empty
            {
                if (dealerIndex == playerIndex)
                    lastHand = EMPTY_HAND;

                AskForPlay();
                // GetUserHand();

                checkEnded();
                playerIndex = (playerIndex + 1) % playerList.Count;
            }
            reInital();

            if (!await toPlayOneMoreRound())
                break;
        }

    }   // PayTribute, ReturnTribute, AskForAce, AceGoPublic, start AskForPlay(id) by turns and check whether the game is stoped.

    private Task<bool> toPlayOneMoreRound()
    {
        // as what the name says.
        return Room.activeRoom.AskPlayOneMoreRound();
    }

    private void reInital()
    {
        stillPlay = new List<Player> { };
        dealerIndex = 0;
        playerIndex = 0;
    }
}