using System;
using System.Collections.Generic;

// understand the attribute of a card
public class Card{
    public static readonly Card BLACK_ACE = new Card(11);
    private readonly int _id;
    public static readonly int CARD_MAX = 53;

    public Card(int cardNumber)
    {        
        if(cardNumber<0 || cardNumber>53)
            throw new Exception("cardNumber is invalid");
        this._id = cardNumber;

    }

    // constructor that takes in the name of card in the form of "<number><suit>"
    // like "3C"
    public Card(string cardName)
    {
        Suit suit;
        switch(cardName[cardName.Length - 1])
        {
            case 'C':
                suit = Suit.Club;
                break;
            case 'D':
                suit = Suit.Diamond;
                break;
            case 'H':
                suit = Suit.Heart;
                break;
            case 'S':
                suit = Suit.Spade;
                break;
            default:
                throw new Exception("Invalid suit in cardName");
        }

        cardName = cardName.Substring(0, cardName.Length - 1);
        int cardNumber;
        bool isSuccessful = Int32.TryParse(cardName, out cardNumber);
        if (!isSuccessful) {
            switch(cardName[0])
            {
                case 'J':
                    cardNumber = 11;
                    break;
                case 'Q':
                    cardNumber = 12;
                    break;
                case 'K':
                    cardNumber = 13;
                    break;
                case 'A':
                    cardNumber = 14;
                    break;
                default:
                    throw new Exception("Invalid card number");
            }
        } else {
            if (cardNumber == 2)
            {
                cardNumber = 15;
            }
        }

        _id = (int)suit * 13 + cardNumber - 3;
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
