using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class Player 
{
    #region fields
    private IClientProxy _client;
    private List<Card> _cardsInHand;
    #endregion

    #region properties
    public string Name { get; }
    public string ConnectionId { get; }
    public bool IsBlackAce { get; private set; }
    public bool IsBlackAcePublic { get; private set; }
    public int CardCount { get{ return _cardsInHand.Count; } }
    // public IReadOnlyCollection<Card> CardsInHand { get{ return _cardsInHand.AsReadOnly(); } }
    #endregion

    public Player(string name, IClientProxy client, string connectionId)
    {
        _client = client;
        _cardsInHand = new List<Card>{};
        Name = name;
        ConnectionId = connectionId;
        IsBlackAce = false;
        IsBlackAcePublic = false;
    }

    public void CheckAce()
    {
        foreach(var card in _cardsInHand)
        {
            if(card.Equals(Card.BLACK_ACE)){
                IsBlackAce = true;
                break;
            }                
            
        }
    }

    public bool IsTwoCats()
    {
        return _cardsInHand.Contains(new Card(52)) && _cardsInHand.Contains(new Card(53));
    }

    public bool IsFourTwo()
    {
        return _cardsInHand.Contains(new Card(12)) && _cardsInHand.Contains(new Card(25))
            && _cardsInHand.Contains(new Card(38)) && _cardsInHand.Contains(new Card(51));
    }

    // return true if go public, otherwise not.
    // if one player has two ace, no solution now
    public async Task AceGoPublic()
    {
        if (IsBlackAce)
        {
            await BackToFront.AskAceGoPublicBackend(_client);
            IsBlackAcePublic = PlayerHubTempData.aceGoPublic;
            PlayerHubTempData.aceGoPublic = false;
        }
    }

    public void AddCards(List<Card> cards)
    {
        _cardsInHand.AddRange(cards);
    }
    public void ClearCard() 
    {
        _cardsInHand.Clear();
    }
    public void clearAce()
    {
        IsBlackAce = false;
        IsBlackAcePublic = false;
    }

    public bool hasCard()
    {
        return _cardsInHand.Count != 0;
    }

    public bool isFinished()
    {
        return this._cardsInHand.Count == 0;
    }

    // sort(descending order) + set black ace
    public void OrganizeHand() 
    {
        _cardsInHand.Sort((x, y) => y.Number.CompareTo(x.Number));
        if(_cardsInHand.Contains(Card.BLACK_ACE))
            IsBlackAce = true;
    }

    // exclude Black Ace for now
    public void PayTribute(Player p) {
        Card tribute = _cardsInHand[0];
        int i = 0;
        while(tribute == Card.BLACK_ACE)
        {
            i++;
            tribute = _cardsInHand[i];
        }
        _cardsInHand.Remove(tribute);
        p.AddCards(new List<Card>{tribute});
        p.OrganizeHand();
    }
    public void ReturnTribute(Player p, int returnNumber)
    {
        bool valid = false;
        while(!valid)
        {
            BackToFront.AskReturnTributeBackend(_client);
            var userReturn = PlayerHubTempData.returnCards;
        
            if(userReturn.Count != returnNumber)
                BackToFront.TributeReturnNotValidBackend(_client);
            else
                valid = true;
        }
        p.AddCards(PlayerHubTempData.returnCards);
        p.OrganizeHand();
    }

    /// <summary>
    /// return true if is a valid hand and is greater than lastHand
    /// return false if not valid
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool PlayHand(List<Card> cards, Hand lastHand) 
    {
        Hand hand;
        try
        {
            hand = new Hand(cards);
        }
        catch(Exception e)
        {
            BackToFront.CreateErrorMessage(_client, "Hand is not valid");
            return false;
        }
        if(hand.CompareHand(lastHand))
        {
            foreach(var card in cards)
                _cardsInHand.Remove(card);
            BackToFront.HandIsValidBackend(_client);
            return true;
        }
        BackToFront.CreateErrorMessage(_client, "Hand is not greater than the previous one");
        return false;
    }


    public void GetPlayerHand()
    {
        BackToFront.AskForPlayBackend(_client);
    }

    public void SendCurrentCardListBackend()
    {
        BackToFront.SendCurrentCardListBackend(_client, _cardsInHand);
    }

    // override object.Equals
    public override bool Equals(object obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //
        
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        return ((Player)obj).ConnectionId.Equals(this.ConnectionId);
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return this.ConnectionId.GetHashCode();
    }
}
