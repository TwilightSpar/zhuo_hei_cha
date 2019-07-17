package zhuo_hei_cha;

public class Card implements Comparable<Card> {
	int card_number;
	public Card(int no) {
		this.card_number = no;
	}
	@Override
	public int compareTo(Card c1) {
		if(this.card_number < c1.card_number)
			return -1;
		else if(this.card_number > c1.card_number)
			return 1;
		else
			return 0;
	}
	
}
