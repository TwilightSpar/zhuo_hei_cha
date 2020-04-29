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
        if(IsPair(cards)>0)
            handName = "pair";
        else if(IsFlush(cards))
            handName = "flush";
        else if(IsCats(cards))
            handName = "cats";
        
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
        else
        {
            throw new Exception("not a valid hand");
        }

            
        // CardValueFactory a = new CardValueFactory(cards);
        switch(handName)
        {
            case "single": 
                
                break;
            case "flush":
                //  tell straight Flush
                bool isStraightFlush = true;
                foreach(var card in cards)
                    if(card.Suit != cards[0].Suit)
                    {
                        isStraightFlush = false;
                        break;
                    }
                if(isStraightFlush)
                    cardValue = new FlushCardValue(cards[cards.Count-1].Number+13, cards.Count);
                else
                    cardValue = new FlushCardValue(cards[cards.Count-1].Number, cards.Count);
                break;
            case "pair":
                int pairNumber = IsPair(cards);
                cardValue = new PairCardValue(cards[cards.Count-1].Number,pairNumber);
                break;
            case "cats":
                cardValue = new CatsCardValue();
                group = 4;
                break;


        }
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

    private bool IsFlush(List<Card> cards)
    {
        if(cards.Count<3)
            return false;
        
        // if the flush is A23, then the order is 3A2, so here i adjust the order.
        // if there are 3 to 2, then change it to A to K does not matter.
        if((cards[0].Number == 3) && (cards[cards.Count-1].Number == 15))
        {
            var two = cards[cards.Count-1];
            var Ace = cards[cards.Count-2];
            cards.RemoveRange(cards.Count-2, 2);
            cards.Insert(0, two);
            cards.Insert(0, Ace);
        }

        else if(cards[cards.Count-1].Number == 15)      // no KA2
            return false;


        for(int i=0; i<cards.Count-1; i++){
            if(((cards[i].Number + 1) % 13) != (cards[i+1].Number % 13))
                return false;
        }
        return true;
    }

    private int IsPair(List<Card> pair){
        if((pair.Count % 2 == 1) || (pair.Count == 4))  // no tractor: 3344
            return -1;

        if((pair[0].Number == 3) && (pair[pair.Count-1].Number == 15))
        {
            var two = pair[pair.Count-1];
            var two2 = pair[pair.Count-2];
            var Ace = pair[pair.Count-3];
            var Ace2 = pair[pair.Count-4];
            pair.RemoveRange(pair.Count-4, 4);
            pair.Insert(0, two);
            pair.Insert(0, two2);
            pair.Insert(0, Ace);
            pair.Insert(0, Ace2);
        }
        
        else if(pair[pair.Count-1].Number == 15)     // no KKAA22
            return -1;
        int i = 0;
        for(; i< pair.Count/2; i++){            
            if((pair[2*i].Number != pair[2*i+1].Number))
                return -1;
        }
        i=0;
        for(; i< pair.Count/2-1; i++){
            // no 335577 should be continuous
            if(((pair[2*i].Number + 1) )% 13 != (pair[2*(i+1)].Number % 13))
                return -1;
        }
        return i+1;
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
        {
            // we add another copy of ACE at the beginning and treat it as 1
            cardValues.Insert(0, 1);
        }

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