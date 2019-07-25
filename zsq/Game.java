import java.util.ArrayList;
import java.util.Deque;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Set;


public class Game {

    private List<User> users;
    private Deque<User> usersWon;
    private final int NUM_OF_USERS = 4;

    private Set<Card> deck;
    private final int NUM_OF_CARDS = 54;

    public Game() {
        usersWon = new LinkedList<User>();
        users = new LinkedList<User>();
        for (int i = 0; i < NUM_OF_USERS; ++i) {
            users.add(new User(i));
        }

        // shuffle users here if we want

        deck = new HashSet<deck>();
        for (int i = 0; i < NUM_OF_CARDS; ++i) {
            deck.add(new Card(i));
        }

        // shuffle cards here

        // this list should store cards that have been shuffled
        List<Card> shuffledCards = new LinkedList<Card>();

        // distributing cards to each user
        ArrayList<List<Card>> userHands = new ArrayList<List<Card>>(NUM_OF_USERS);
        int index = 0;
        for (Card c: shuffledCards) {
            userHands.get(index).add(c);
            index = (index + 1) % NUM_OF_USERS;
        }
        
        index = 0;
        for (User u: users) {
            u.acceptCards(userHands.get(index));
            ++index;
        }

        // insert logic in Begin.java here
    }

    public playGame() {
        // invokes Begin, and End
    }

    public void endGame() {
        assert(usersWon.size() == NUM_OF_USERS);

        if (usersWon.peekLast().isHeiCha()) {
            // hei cha has lost
            User jinGongTarget = usersWon.poll();
            User currentUser = usersWon.poll();

            // send query to frontend about which cards to jingong
            ArrayList<List<Card>> cardsToJinGong = new ArrayList<List<Card>>(NUM_OF_USERS);

            int index = 0;
            while (currentUser != null) {
                currentUser.giveCardsTo(jinGongTarget, cardsToJinGong.get(index));
                currentUser = usersWon.poll();
            }

            // hei cha should give cards back
        } else {
            // hei cha has lost
        }
    }
}