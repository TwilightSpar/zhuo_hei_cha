using System;
using System.Collections.Generic;

// understand the attribute of a card
public class Card{
    public static readonly Card BLACK_ACE = new Card(11);
    private readonly int _id;
    public static readonly int CARD_MAX = 53;

    public Card(int cardNumber)
    {        
        if(cardNumber<0 || cardNumber>CARD_MAX)
            throw new Exception("cardNumber is invalid");
        this._id = cardNumber;

    }

    public Card(Card previousCard)
    {
        _id = previousCard._id;
    }

    public Suit Suit
    {
        get{return (Suit)(_id/13);}        
    }

    // jokers' number are 0 and 1
    public int Number
    {
        get{
            if(_id>51)
                return _id;
            else 
                return _id%13+3;
        }
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
        var otherCard = (Card)obj;
        return this._id.Equals (otherCard._id);
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return this._id.GetHashCode();
    }
}
