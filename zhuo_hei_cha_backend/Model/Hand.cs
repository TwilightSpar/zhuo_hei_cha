using System;
using System.Collections.Generic;

public class Hand
{
	string handName;
	int group; //( single, flush(straightFlush=顺+13), pair<(hong<plane<大飞机… )<(boom<轰炸机,…)<cats)(4 level)
	CardValue cardValue;  

    Hand(List<Card> cards){
        // tell HandType



        handName = name;
        // CardValueFactory a = new CardValueFactory(cards);
        switch(handName)
        {
            case "single": 
                cardValue = new SingleCardValue(1);
                break;
            case "flush":
                //  tell straight Flush
                cardValue = new FlushCardValue(3,5);
                break;
            case "pair":
                cardValue = new PairCardValue(3,5);
                break;
            case "hong":
                cardValue = new HongCardValue(3,5);
                break;
            case "boom":
                cardValue = new BoomCardValue(3,5);
                break;
            case "cats":
                cardValue = new CatsCardValue();
                break;


        }
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