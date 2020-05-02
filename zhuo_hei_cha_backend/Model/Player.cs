using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

public class Player 
{
    string _name;
    private IClientProxy _client;
    bool isBlackAce = false;
    List<Card> cardsInHand;

    public Player(string name, IClientProxy client)
    {
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

    // return true if go public, otherwise not.
    public bool AceGoPublic()
    {
        BackToFront.AskAceGoPublicBackend(_client);
        return PlayerHubTempData.aceGoPublic;
    }

    public void AddCards(List<Card> cards)
    {
        cardsInHand.AddRange(cards);
    }
    public void ClearCard() 
    {
        cardsInHand.Clear();
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
    public void ReturnTribute() 
    {
        BackToFront.AskReturnTributeBackend(_client);
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
}
