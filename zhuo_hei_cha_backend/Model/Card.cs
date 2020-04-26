using System;
using System.Collections.Generic;

// understand the attribute of a card
public class Card{
    private int cardNumber;

    public Card(int cardNumber)
    {
        if(cardNumber<0 || cardNumber>53)
            throw new Exception("cardNumber is invalid");
        this.cardNumber = cardNumber;
    }

    public Color color()
    {
        if(cardNumber<13)
            return Color.Space;
        else if(cardNumber<26)
            return Color.Space;
        else if(cardNumber<39)
            return Color.Space;
        else if(cardNumber<52)
            return Color.Space;
        else if(cardNumber == 53)
            return Color.SmallJoker;
        else return Color.BigJoker;
    }

    // jokers' number are 52 and 53
    public int number()
    {
        if(cardNumber>=0 && cardNumber<52)
            return (cardNumber+3)%13;
        else return cardNumber;
    }
}