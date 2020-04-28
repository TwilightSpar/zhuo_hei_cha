using System;

// understand the attribute of a card
public class Card
{
    // max card id (inclusive)
    public static readonly int CARD_MAX = 53;

    // max non-Joker card id (inclusive)
    public static readonly int NON_JOKER_CARD_MAX = 51;

    // number of cards in a suit
    public static readonly int CARDS_PER_SUIT = 13;

    // offset applied to all non-Joker cards when calculating the Number property
    public static readonly int CARD_NUMBER_OFFSET = 3;

    private readonly int _id;

    public Card(int id)
    {        
        if (id < 0 || id > CARD_MAX)
            throw new ArgumentOutOfRangeException("cardNumber is invalid");
        
        this._id = id;
    }

    public Suit Suit
    {
        get
        {
            return (Suit)(_id / CARDS_PER_SUIT);
        }
    }
    
    public int Number
    {
        get
        {
            if (_id <= NON_JOKER_CARD_MAX)
            {
                return _id % CARDS_PER_SUIT + CARD_NUMBER_OFFSET;
            }
            
            // jokers' number are 0 and 1
            return _id % CARDS_PER_SUIT;
        }
    }
}
