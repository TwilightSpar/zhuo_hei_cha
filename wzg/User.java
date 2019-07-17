package zhuo_hei_cha;

import java.util.ArrayList;

public class User {
	
	int id;
	int cards_number;
	ArrayList<Card> user_cards;
	public User(int id, int cards_number) {
		this.id = id;
		this.cards_number = cards_number;
	}
}
