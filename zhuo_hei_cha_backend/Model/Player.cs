using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

public class Player 
{
    string _name;
    private IClientProxy _client;
    bool isBlackAce = false;
    bool isPublic = false;
    List<Card> cardsInHand;

    public Player(string name, IClientProxy client)
    {
        cardsInHand = new List<Card>{};
        _name = name;
        _client = client;
    }

    public void CheckAce()
    {
        foreach(var card in cardsInHand)
        {
            if(card.Equals(Card.BLACK_ACE))
                isBlackAce = true;
        }
    }

    public bool IsBlackAce()
    {
        return isBlackAce;
    }
    public bool IsPublicAce()
    {
        return isPublic;
    }
    public bool IsTwoCats()
    {
        return cardsInHand.Contains(new Card(52)) && cardsInHand.Contains(new Card(53));
    }

    public bool IsFourTwo()
    {
        return cardsInHand.Contains(new Card(12)) && cardsInHand.Contains(new Card(25))
            && cardsInHand.Contains(new Card(38)) && cardsInHand.Contains(new Card(51));
    }

    // return true if go public, otherwise not.
    // if one player has two ace, no solution now
    public void AceGoPublic()
    {
        if (isBlackAce)
        {
            BackToFront.AskAceGoPublicBackend(_client);
            isPublic = PlayerHubTempData.aceGoPublic;
            PlayerHubTempData.aceGoPublic = false;
        }
    }

    public void AddCards(List<Card> cards)
    {
        cardsInHand.AddRange(cards);
    }
    public void ClearCard() 
    {
        cardsInHand.Clear();
    }
    public void clearAce()
    {
        isBlackAce = false;
        isPublic = false;
    }

    public bool hasCard()
    {
        return cardsInHand.Count != 0;
    }

    public bool isFinished()
    {
        return this.cardsInHand.Count == 0;
    }

    // sort(descending order) + set black ace
    public void OrganizeHand() 
    {
        cardsInHand.Sort((x, y) => y.Number.CompareTo(x.Number));
        if(cardsInHand.Contains(Card.BLACK_ACE))
            isBlackAce = true;
    }

    // exclude Black Ace for now
    public void PayTribute(Player p) {
        Card tribute = cardsInHand[0];
        int i = 0;
        while(tribute == Card.BLACK_ACE)
        {
            i++;
            tribute = cardsInHand[i];
        }
        cardsInHand.Remove(tribute);
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
            BackToFront.HandIsNotValidBackend(_client);
            return false;
        }
        if(hand.CompareHand(lastHand))
        {
            foreach(var card in cards)
                cardsInHand.Remove(card);
            BackToFront.HandIsValidBackend(_client);
            return true;
        }
        BackToFront.HandIsNotValidBackend(_client);
        return false;
    }


    public void GetPlayerHand()
    {
        BackToFront.AskForPlayBackend(_client);
    }

    public void SendCurrentCardListBackend()
    {
        BackToFront.SendCurrentCardListBackend(_client, cardsInHand);
    }
}
