using System;
using System.Collections.Generic;
using System.Linq;

public class Hand
{
	string handName;
	int group = 1; //( single, flush(straightFlush=顺+13), pair<(hong<plane<大飞机… )<(boom<轰炸机,…)<cats)(4 level)
	CardValue cardValue;  

    private static readonly int ACE_VALUE = 14;
    private static readonly int TWO_VALUE = 15;

    public Hand(List<Card> cards){
        if (cards.Count == 0)
        {
            group = 0;
            return;
        }

        // tell HandType
        cards.Sort((x, y) => x.Number.CompareTo(y.Number)); 

        if(cards.Count == 1)
        {
            handName = "single";
            cardValue = new SingleCardValue(cards[0].Number);
            return;
        }
        
        var flushLastValue = IsFlush(cards);
        if(flushLastValue > 0)
        {
            handName = "flush";
            //  tell straight Flush
            bool isStraightFlush = true;
            foreach(var card in cards)
                if(card.Suit != cards[0].Suit)
                {
                    isStraightFlush = false;
                    break;
                }
            if(isStraightFlush)
                cardValue = new FlushCardValue(flushLastValue+13, cards.Count);
            else
                cardValue = new FlushCardValue(flushLastValue, cards.Count);
            return;
        }
        
        var pairLastValue = IsPair(cards);
        if(pairLastValue>0)
        {
            handName = "pair";
            cardValue = new PairCardValue(pairLastValue,cards.Count / 2);
            return;
        }

        // checking for Hong or Bomb
        HongOrBomb type;
        int returnValue = CheckHongOrBomb(cards, out type);
        if (returnValue > 0)
        {
            handName = type.ToString();
            if (type == HongOrBomb.hong)
            {
                cardValue = new HongCardValue(returnValue, cards.Count() / (int)HongOrBomb.hong);
                group = 2;
            }
            else
            {
                cardValue = new BombCardValue(returnValue, cards.Count() / (int)HongOrBomb.bomb);
                group = 3;
            }
            return;
        }
        
        if(IsCats(cards))
        {
            handName = "cats";
            group = 4;
            return;
        }
    
        throw new Exception("not a valid hand");
    }

    private bool IsCats(List<Card> cards)
    {
        if(cards.Count != 2)
            return false;
        if(cards[0].Suit != cards[1].Suit)
            return false;
        if(cards[0].Suit != Suit.Joker)
            return false;
        return true;
    }


    // return -1 if not a Pair; otherwise, return its value
    // (that is determined by value of the end of the Pair sequence)
    private int IsPair(List<Card> cards){
        int cardLength = cards.Count;
        if((cardLength % 2 == 1) || (cardLength == 4))  // no tractor: 3344
            return -1;

        var tempCards = cards.Select(card => new Card(card)).ToList();
        if((tempCards[0].Number == 3) && (tempCards[cardLength-1].Number == 15))
        {
            var two = tempCards[cardLength-1];
            var two2 = tempCards[cardLength-2];
            var Ace = tempCards[cardLength-3];
            var Ace2 = tempCards[cardLength-4];
            tempCards.RemoveRange(cardLength-4, 4);
            tempCards.Insert(0, two);
            tempCards.Insert(0, two2);
            tempCards.Insert(0, Ace);
            tempCards.Insert(0, Ace2);
        }
        
        else if(tempCards[cardLength-1].Number == 15 && cardLength != 2)     // no KKAA22
            return -1;
        
        // check pair
        for(int i = 0; i< cardLength/2; i++){            
            if((tempCards[2*i].Number != tempCards[2*i+1].Number))
                return -1;
        }

        // check continueous pair
        for(int i = 0; i< cardLength/2-1; i++){            
            if(((tempCards[2*i].Number + 1) )% 13 != (tempCards[2*(i+1)].Number % 13))
                return -1;
        }
        return tempCards[cardLength-1].Number;
    }

    // return -1 if not a Flush; otherwise, return its value
    // (that is determined by value of the end of the Flush sequence)
    private int IsFlush(List<Card> cards)
    {
        int cardLength = cards.Count;
        if(cardLength<3)
            return -1;
        
        // if the flush is A23, then the order is 3A2, so here i adjust the order.
        // if there are 3 to 2, then change it to A to K does not matter.
        var tempFlush = cards.Select(card => new Card(card)).ToList();
        if((tempFlush[0].Number == 3) && (tempFlush[cardLength-1].Number == 15))
        {
            var two = tempFlush[cardLength-1];
            var Ace = tempFlush[cardLength-2];
            tempFlush.RemoveRange(cardLength-2, 2);
            tempFlush.Insert(0, two);
            tempFlush.Insert(0, Ace);
        }

        else if(tempFlush[cardLength-1].Number == 15)      // no KA2
            return -1;


        for(int i=0; i<cardLength-1; i++){
            if(((tempFlush[i].Number + 1) % 13) != (tempFlush[i+1].Number % 13))
                return -1;
        }
        return tempFlush[cardLength-1].Number;
    }


    enum HongOrBomb { hong = 3, bomb = 4, neither }

    /// <summary>
    /// return the value of the Hong or Bomb if they cards are valid. Otherwise,
    /// return -1.
    /// </summary>
    /// <param name="cards">list of cards to be checked</param>
    /// <param name="resultType"></param>
    /// <returns></returns>
    private int CheckHongOrBomb(List<Card> cards, out HongOrBomb resultType)
    {
        resultType = HongOrBomb.neither;

        // a Hong or Bomb must have at least 3 cards
        if (cards.Count() < 3)
            return -1;

        var cardGroups = cards.GroupBy(
            card => card.Number,
            (cardNumber, cardNumberGroup) => new
            {
                Value = cardNumber,
                Repetition = cardNumberGroup.Count()
            });
        
        // checking card repetitions
        var repetitionGroups = cardGroups.GroupBy(group => group.Repetition);
        
        // a Hong or a Bomb cannot have cards that repeat different number of times
        if (repetitionGroups.Count() != 1)
            return -1;

        HongOrBomb tempType = HongOrBomb.neither;
        switch (repetitionGroups.First().Key)
        {
            case (int)HongOrBomb.hong:
                tempType = HongOrBomb.hong;
                break;
            case (int)HongOrBomb.bomb:
                tempType = HongOrBomb.bomb;
                break;
            default:
                return -1;
        }

        // checking consecutive numbers
        var cardValues = cardGroups
            .Select(group => group.Value)
            .Select(value => (value == TWO_VALUE) ? 2: value)
            .OrderBy(value => value)
            .ToList();

        if (cardValues.Contains(ACE_VALUE))
            cardValues.Insert(0, 1);
        
        int sequenceValue = CheckConsecutiveSequence(cardValues);
        if (sequenceValue > 0)
            resultType = tempType;

        return sequenceValue;
    }

    /// <summary>
    /// Return the value of the sequence if it is consecutive.
    /// Otherwise, return -1
    /// </summary>
    /// <param name="sequence">sequence of numbers to be checked, sorted in ascending order with no duplicates</param>
    /// <param name="length">length of the pattern to be checked</param>
    /// <returns></returns>
    private int CheckConsecutiveSequence(List<int> sequence)
    {
        if (sequence.Count <= 1) return sequence.First();

        if (sequence.Last() == ACE_VALUE)
        {
            bool consecutiveSoFar = true;
            
            // treat ACE as ACE and ignore the 1 inserted at the beginning
            for (int i = sequence.Count - 1; i >= 2; --i)
            {
                if (sequence[i - 1] != sequence[i] - 1)
                {
                    consecutiveSoFar = false;
                    break;
                }
            }

            if (consecutiveSoFar) return ACE_VALUE;

            // treat ACE as 1 only from this point on and remove ACE_VALUE from
            // the end of the sequence
            sequence = sequence.Take(sequence.Count - 1).ToList();
        }

        for (int i = 0; i < sequence.Count - 1; ++i)
        {
            if (sequence[i + 1] != sequence[i] + 1) return -1;
        }

        return sequence.Last();
    }

    // 先比大级别，级别一样但是类型不一样，先出的上家大，如果类别一样，进入比值环节。
    public bool CompareHand(Hand LastHand)
    {
        if(this.group == 0 && LastHand.group == 0)
            return false;
		if(this.group != LastHand.group)
        {
	        return this.group > LastHand.group;
        }
        else if(this.handName != LastHand.handName)
        {
	        return false;
        }
        else
        {
	        return this.cardValue.CompareValue(LastHand.cardValue);
        }
    }

}