using System;
using System.Collections.Generic;

// understand the attribute of a card
public class Card{
    private readonly int _id;
    private int number;

    private void InitNumber(){      // A is 14, 2 is 15
        if(_id>51)
        number = _id%13;
    else 
        number = _id%13+3;
    }
    
    
    public static readonly int CARD_MAX = 53;

    public Card(int cardNumber)
    {        
        if(cardNumber<0 || cardNumber>CARD_MAX)
            throw new Exception("cardNumber is invalid");
        this._id = cardNumber;

        InitNumber();
    }

    public Suit Suit
    {
        get{return (Suit)(_id/13);}        
    }

    // jokers' number are 0 and 1
    public int Number
    {
        get{
            return number;
        }
        set{ number += value;}
    }
}
