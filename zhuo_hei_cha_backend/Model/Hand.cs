using System;
using System.Collections.Generic;
using System.Linq;

public class Hand
{
	string handName;
	int group = 0; //( single, flush(straightFlush=顺+13), pair<(hong<plane<大飞机… )<(boom<轰炸机,…)<cats)(4 level)
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
                {
                    foreach(var card in cards)
                        card.Number += 13;
                }
                cardValue = new FlushCardValue(cards[0].Number, cards.Count);
                break;
            case "pair":
                int pairNumber = IsPair(cards);
                cardValue = new PairCardValue(cards[0].Number,pairNumber);
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
        for(int i=0; i<cards.Count-1; i++){
            if(cards[i].Number + 1 != cards[i+1].Number)
            return false;
        }
        return true;
    }

    private int IsPair(List<Card> pair){
        if(pair.Count % 2 == 1)
            return -1;
        int i = 0;
        for(; i< pair.Count/2; i++){
            if(pair[2*i].Number != pair[2*i+1].Number)
                return -1;
        }
        Console.Write(i);
        return i;
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