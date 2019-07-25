/**
 * 在 wzg 的 Card 类的基础上增加了 suit 和 pip 两个属性，分别代表花色和点数
 */


public class Card implements Comparable<Card> {

    enum Suit {
        HEARTS,
        DIAMONDS,
        CLUBS,
        SPADES,
        JOKER
    }

    final private int SUIT_SIZE = 13;
    final private int DECK_SIZE = 54;

    int card_number;
    Suit suit;
    int pip;
    
	public Card(int no) {
        if (no >= DECK_SIZE) {
            throw new IllegalArgumentException("Card number out of range!");
        }

        this.card_number = no;
        this.pip = no % SUIT_SIZE;
        switch (no / SUIT_SIZE) {
            case 0:
                this.suit = Suit.HEARTS;
                break;
            case 1:
                this.suit = Suit.DIAMONDS;
                break;
            case 2:
                this.suit = Suit.CLUBS;
                break;
            case 3:
                this.suit = Suit.SPADES;
                break;
            case 4:
                this.suit = Suit.JOKER;
                break;
            default:
                throw new Exception("Something's wrong");
        }
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

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || obj.getClass() != this.getClass()) return false;

        Card cardObj = (Card) obj;
        return this.card_number == cardObj.card_number;
    }

    @Override
    public int hashCode() {
        return this.card_number;
    }
}
