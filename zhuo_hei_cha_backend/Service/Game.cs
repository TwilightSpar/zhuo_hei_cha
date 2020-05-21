using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Game
{
    private static readonly Hand EMPTY_HAND = new Hand(new List<Card>() { });
    List<Player> playerList;
    List<Player> finishOrder;

    List<List<int>> tributeList = new List<List<int>>{};
    List<Player> stillPlay;     // prepare for checkEnded
    bool isGameStarted;

    Hand lastHand = EMPTY_HAND;

    int dealerIndex = 0;

    int playerIndex = 0;

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
        {
            playerList[i].AddCards(deckForUser[i]);
            playerList[i].OrganizeHand();
            playerList[i].CheckAce();
        }

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


    private void SetTributeList()
    {
        for (int i = 0; i < finishOrder.Count; i++)
        {
            tributeList.Add(new List<int>{});
            for (int j = 0; j < finishOrder.Count; j++)
                tributeList[i].Add(0);

        }
        if(finishOrder.Any(x => x.IsFourTwo()) && !finishOrder[0].IsFourTwo())
            return;
        for (int i = 0; i < finishOrder.Count; i++)
        {
            var thisPlayer = finishOrder[i];
            for (int j = i+1; j < finishOrder.Count; j++)
            {
                var otherPlayer = finishOrder[j];
                if(otherPlayer.IsTwoCats())
                {
                    tributeList[i][j] = 0;
                    continue;
                }
                if(thisPlayer.IsBlackAce != otherPlayer.IsBlackAce)
                    if(thisPlayer.IsBlackAcePublic || otherPlayer.IsBlackAcePublic)
                    {
                        tributeList[i][j] = 2;
                    }
                    else
                    {
                        tributeList[i][j] = 1;
                    }
            }
        }
    }

    /// <summary>
    /// group tributeList according to Black ACEs
    /// call tribute in order
    /// refer to public ace list
    /// </summary>
    private void PayTribute()
    {
        // consider sotuations that do not need totribute
        // player1.PayTribute(player2) only tribute one card
        // if one person have two black ace
        for (int i = 0; i < finishOrder.Count-1; i++)
        {            
            var thisPlayer = finishOrder[i];
            for(int j = i+1; j<playerList.Count; j++)
            {
                var otherPlayer = finishOrder[j];
                for(int round = 0; round < tributeList[i][j]; round ++)
                    otherPlayer.PayTribute(thisPlayer);
            }
            
        }
    }

    /// <summary>
    /// call returnTribute in reverse order
    /// </summary>
    private void ReturnTribute()
    {        
        for (int i = finishOrder.Count-1; i > 0; i--)
        {
            var thisPlayer = finishOrder[i];
            for(int j = i-1; j>=0; j--)
            {
                var otherPlayer = finishOrder[j];
                if(tributeList[j][i] == 0)
                    continue;
                otherPlayer.ReturnTribute(thisPlayer, tributeList[j][i]);
                SendCurrentCardListBackend();
            }
            
        }
    }

    // get invoked if player decided to announce Ace before game starts
    // add to public ace list
    private async Task AceGoPublic()
    {
        foreach (var p in playerList)
            await p.AceGoPublic();
        
    }

    /// <summary>
    /// Ask players whether play or not 
    /// check whether player is finished, and if true, add player to tributeList
    /// 
    /// </summary>
    /// <param name="playerId"></param>
    private async Task AskForPlay()
    {
        bool valid = false;
        while (!valid)
        {
            await playerList[playerIndex].GetPlayerHand();

            List<Card> userHand = PlayerHubTempData.userHand;
            if (userHand.Count == 0 && dealerIndex != playerIndex)    // dealer cannot skip
            {
                playerList[playerIndex].PlayerListUpdateBackend(new List<Card>{});
                return;
            }

            if (dealerIndex == playerIndex)
                lastHand = EMPTY_HAND;

            if (playerList[playerIndex].PlayHand(userHand, this.lastHand))
            {
                dealerIndex = playerIndex;
                lastHand = new Hand(userHand);
                valid = true;
                playerList[playerIndex].PlayerListUpdateBackend(userHand);
            }
            PlayerHubTempData.userHand = new List<Card>{};
        }

    }

    private void checkEnded()
    {
        Player p = playerList[playerIndex];
        if (p.isFinished())
        {
            finishOrder.Add(p);
            stillPlay.Remove(p);
            lastHand = EMPTY_HAND;
            p.PlayerListUpdateBackend(new List<Card>{});
        }
        
        var group = stillPlay.Select(x => x.IsBlackAce).GroupBy(x => x);
        bool blackAceLose = group.SelectMany(o=>o).ToList()[0];
        int remainingGroupCount = group.Count();
        if (remainingGroupCount == 1)
        {
            finishOrder.AddRange(stillPlay);
            stillPlay.Clear();
            this.isGameStarted = false;
            GameOverBackend(blackAceLose);
            return;
        }
        
    }

    private void GameOverBackend(bool blackAceLose)
    {
        string message = "";
        if(blackAceLose)
            message = "GameOver,and non-blackAce win";
        else
            message = "GameOver,and blackAce escaped";
        foreach(var p in playerList)
            p.GameOverBackend(message);
    }

    /// <summary>
    /// main process
    /// </summary>
    public async Task GameProcess()
    {
        int roundNumber = 1;
        while (true)
        {
            if(roundNumber != 1)
                SetTributeList();
            
            foreach(var p in playerList)
                p.clearAce();
            
            InitCardList();
            SendCurrentCardListBackend();
            
            await Task.Delay(5000);
            if(roundNumber != 1)
            {
                // alert user that we sill start to pay tribute
                foreach(var p in playerList)
                {
                    p.PaytributeBegin(finishOrder);                      
                }
                await Task.Delay(2000);                
                PayTribute();
                SendCurrentCardListBackend();

                ReturnTribute();
            }

            isGameStarted = true;

            finishOrder = new List<Player> {};   // init tributeList
            PlayerHubTempData.returnCards = new List<Card>{};
            stillPlay = playerList.Select(x => x).ToList();
            PlayerHubTempData.userHand = new List<Card>{};
            await AceGoPublic();

            while (isGameStarted)   // skip means hand are empty
            {
                ShowCurrentPlayerTurn();
                await AskForPlay();
                SendCurrentCardListBackend();

                checkEnded();

                playerIndex = (playerIndex + 1) % playerList.Count;
                while(playerList[playerIndex].isFinished() && isGameStarted == true)
                    playerIndex = (playerIndex + 1) % playerList.Count;
            }
            reInital();
            
            await Room.AskPlayOneMoreRound();
            if (!toPlayOneMoreRound())
            {
                BackToFront.BreakGameBackend();
                break;
            }
            PlayerHubTempData.playOneMoreRound = true;
            roundNumber += 1;
            ClearLastHandBackend();
        }

    }

    private void ClearLastHandBackend()
    {
        foreach(var p in playerList)
            p.PlayerListUpdateBackend(new List<Card>{});
    }

    private void ShowCurrentPlayerTurn()
    {
        foreach(var p in playerList)
        {
           p.ShowCurrentPlayerTurn(playerIndex);
        }
    }

    private bool toPlayOneMoreRound()
    {
        // as what the name says.
        return PlayerHubTempData.playOneMoreRound;
    }

    /// <summary>
    /// Not only do some change in backend, also change layout in frontend
    /// </summary>
    private void SendCurrentCardListBackend()
    {
        foreach(var p in playerList)
        {
            p.OrganizeHand();
            p.CheckAce();
            p.SendCurrentCardListBackend();

        }
    }

    private void reInital()
    {
        BackToFront.ResetState();
        foreach(var p in playerList){
            p.ClearCard();            
        }
           
        stillPlay = new List<Player> { };
        for(int i = 0; i<playerList.Count; i++)
            if(playerList[i].IsBlackAce)
            {
                dealerIndex = i;
                playerIndex = i;
                break;
            }
            
    }
}