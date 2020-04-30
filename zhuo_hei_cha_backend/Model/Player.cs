using System;
using System.Collections.Generic;

public class Player 
{
    int id; 
    object Connection;
    bool BlackAce = false;
    List<Card> cardsInHand;

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

    // sort(descending order) + set black ace
    public void OrganizeHand() 
    {
        cardsInHand.Sort((x, y) => y.Number.CompareTo(x.Number));
        if(cardsInHand.Contains(Card.BLACK_ACE))
            BlackAce = true;
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
    public List<Card> ReturnTribute(Player p) 
    {
        // ask for users
        return new List<Card>(){p.cardsInHand[p.cardsInHand.Count-1]};
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
            return false;
        }
        if(hand.CompareHand(lastHand))
        {
            foreach(var card in cards)
                cardsInHand.Remove(card);
            return true;
        }
        return false;
    }
}
