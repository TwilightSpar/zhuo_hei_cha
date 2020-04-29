using System.Collections.Generic;

public class Player 
{
    int id; 
    object Connection;
    bool BlackAce = false;
    List<Card> cardsInHand;

    public void AddCards(List<Card> cards)
    {

    }
    public void ClearCard() {}

    public bool hasCard(){return false;}

    // sort + set black ace
    public void OrganizeHand() {}

    // exclude Black Ace for now
    public void PayTribute(Player p) {
        // this.removecard
        // p.AddCards()
    }
    public void ReturnTribute(Player p, List<Card> cards) {}

    public void PlayHand(List<Card> cards) {}
}
