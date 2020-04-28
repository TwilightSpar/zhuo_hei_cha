using System;
using System.Collections.Generic;

public class Hand
{
	string handName;
	int group = 1; //( single, flush(straightFlush=顺+13), pair<(hong<plane<大飞机… )<(boom<轰炸机,…)<cats)(4 level)
	CardValue cardValue;  

    public Hand(List<Card> cards){
        // tell HandType
        cards.Sort((x, y) => x.Number.CompareTo(y.Number)); 

        if(cards.Count == 1)
            handName = "single";
        else if(IsPair(cards)>0)
            handName = "pair";
        else if(ISFlush(cards))
            handName = "flush";
        else
        {
            throw new Exception("not a valid hand");
        }

            
        // CardValueFactory a = new CardValueFactory(cards);
        switch(handName)
        {
            case "single": 
                cardValue = new SingleCardValue(cards[0].Number);
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
            // case "hong":
            //     cardValue = new HongCardValue(3,5);
            //     break;
            // case "boom":
            //     cardValue = new BoomCardValue(3,5);
            //     break;
            // case "cats":
            //     cardValue = new CatsCardValue();
            //     break;


        }
    }

    private bool ISFlush(List<Card> cards)
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