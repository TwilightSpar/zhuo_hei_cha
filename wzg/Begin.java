package zhuo_hei_cha;

import java.util.*;
import java.util.Random;

public class Begin {	
	public static void main(String[] arg) {
		int TOTAL_CARDS_NUMBER = 54;
		int USER_NUMBER = 4;
		User[] userlist = new User[4];
		ArrayList<Card> origin_card_list = new ArrayList<Card>(54);
		for(int i = 0; i<TOTAL_CARDS_NUMBER; i++)	// init all cards as a list in order
			origin_card_list.add(new Card(i));
		
		ArrayList<Card> randomList = new ArrayList<Card>(origin_card_list.size());	// create random list
		do{    		
			int randomIndex = new Random().nextInt(origin_card_list.size());
			randomList.add(origin_card_list.remove(randomIndex));
		}while(origin_card_list.size() > 0);
		// now we can use random list 
		
		int[] jin_gong = {-1,-1,-1,-1};	// at the begining, no one need to jingong
		int[] huan_pai = {-1,-1,-1,-1};	// at the begining, no one need to huanpai
		int is_liang_hei_cha = -1;	// at the begining, no one liang hei cha
		
		// allocate userid and cards
		for(int i=0; i < USER_NUMBER; i++) {
			User u;
			if(i/2 == 0) {	// user0,1 has 14 cards, 2,3 has 13 cards
				u = new User(i, TOTAL_CARDS_NUMBER/USER_NUMBER+1);
				u.user_cards = new ArrayList<Card>(TOTAL_CARDS_NUMBER/USER_NUMBER+1);
				u.user_cards.add(randomList.get(52+i));	// 先给1,2号放1张牌,分别是52号和53号
			}
			else {
				u = new User(i, TOTAL_CARDS_NUMBER/USER_NUMBER);
				u.user_cards = new ArrayList<Card>(TOTAL_CARDS_NUMBER/USER_NUMBER);
			}
			u.user_cards.addAll(randomList.subList(i*13, i*13+13));	// everyone has 13 cards, user0,1 has 14
			userlist[i] = u;			
		}
		// everyone has cards
		
		
		//jin gong and huan pai
		for(int i=0; i < USER_NUMBER; i++) {
			User u = userlist[i];
			//first u should huanpai if necessary
			/*alert player to huanpai
			 * if(is_liang_hei_cha > -1) return 2 cards
			 * else return 1 cards
			 * u.user_cards.remove the/these cards
			 * then sort
			 * */
			
			Collections.sort(u.user_cards);	// sort in order to find max
			if(jin_gong[i] > -1) {	// need to jingong
				if(is_liang_hei_cha > -1) {	// and heicha had showoff his/her heicha
					int card_length = u.user_cards.size();
					userlist[jin_gong[i]].user_cards.addAll(u.user_cards.subList(card_length-2, card_length));
					u.user_cards.remove(card_length-1);
					u.user_cards.remove(card_length-1);
				}
				else {	// dosen't liang hei cha
					userlist[jin_gong[i]].user_cards.add(u.user_cards.remove(u.user_cards.size()-1));
				}
			}		
		}
		
		
		// an zhong jiao yi
		/* for(int i=0; i < USER_NUMBER; i++){
		 *		every one can show a card he/she doesn't want,
		 *		Cards that_card =  
		 * 		others can choose to have it or not
		 * 		heicha can't an zhong jiao yi, but user1(wanggeng) can have it. 
		 * 		if some one want the cards
		 * 		User u = userlist[i];
		 * 		userlist[u].user_cards.add(u.user_cards.remove(that_card));
		 * }
		 *  */
		
		
		// liang hei cha
		/*get to know which user liang hei cha
		 * then send back his/her id
		 * is_liang_hei_cha = 0;
		 * */
		
		for(User u : userlist) {
			for(int i =0; i<u.user_cards.size(); i++)
				System.out.print(u.user_cards.get(i).card_number+"\t");
			System.out.println();
		}
	}
}
