using System;
using System.Collections.Generic;
using System.Linq;

public class Hand
{
	string handName;
	int group = 1; //( single, flush(straightFlush=顺+13), pair<(hong<plane<大飞机… )<(boom<轰炸机,…)<cats)(4 level)
	CardValue cardValue;  

    public Hand(List<Card> cards){
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
        int returnValue = IsHongOrBomb(cards, out type);
        if (returnValue > 0)
        {
            handName = type.ToString();
            cardValue = new HongCardValue(returnValue, cards.Count() / (int)type);
            group = (type == HongOrBomb.hong) ? 2 : 3;
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
        
        else if(tempCards[cardLength-1].Number == 15)     // no KKAA22
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

    // return -1 if not a Hong or Bomb; otherwise, return its value
    // (that is determined by value of the end of the Hong sequence)
    private int IsHongOrBomb(List<Card> cards, out HongOrBomb resultType)
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
        const int ACE_VALUE = 14;
        const int TWO_VALUE = 15;
        var cardValues = (List<int>)cardGroups
            .Select(group => group.Value)
            .Select(value => (value == TWO_VALUE) ? 2: value)
            .OrderBy(value => value);

        if (cardValues.Contains(ACE_VALUE))
        if (AreNumbersConsecutive(cardValues))
        {
            resultType = tempType;
            return cardValues.Last();
        }

        return -1;
    }

    private bool AreNumbersConsecutive(List<int> numbers)
    {
        // assume numbers are sorted in ascending order without duplicates

        for (int i = 0; i < numbers.Count() - 1; ++i)
        {
            if (numbers[i + 1] != numbers[i] + 1)
                return false;
        }

        return true;
    }

    // 先比大级别，级别一样但是类型不一样，先出的上家大，如果类别一样，进入比值环节。
    public bool CompareHand(Hand yourHand)
    {
		if(this.group != yourHand.group)
        {
	        return this.group > yourHand.group;
        }
        else if(this.handName != yourHand.handName)
        {
	        return true;
        }
        else
        {
	        return this.cardValue.CompareValue(yourHand.cardValue);
        }
    }

}