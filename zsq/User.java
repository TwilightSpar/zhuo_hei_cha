import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Queue;
import java.util.TreeSet;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.Executors;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;

public class User implements ProducerConsumer {

    private boolean isHeiCha;
    private TreeSet<Card> hand;
    private int id;
    private Socket userConnection;
    private InputStream input;
    private OutputStream output;

    private final Queue<Action> pendingActions = new LinkedBlockingQueue<Action>();
	
	public User(int id, Socket socket) {
		this.id = id;
        this.hand = new TreeSet<Card>();
        this.userConnection = socket;
        this.input = new ObjectInputStream(socket.getInputStream());
        this.output = new ObjectOutputStream(socket.getOuputStream());

        Executors.newScheduledThreadPool(1).scheduleAtFixedRate(new Runnable(){
            @Override
            public void run() {
                if (input.available() != 0) {
                    Action newAction = (Action) input.readObject();
                    enqueueAction(newAction);
                }

                if (pendingActions.size() > 0)
                    processPendingActions();
            }
        }, 500, 500, TimeUnit.MILLISECONDS);
    }

    @Override
    public void processPendingActions() {
        synchronized (this.pendingActions) {
            while (pendingActions.size() > 0) {
                Action currentAction = pendingActions.poll();

                /**
                 * code to process action
                 */
            }
        }
    }

    @Override
    public void enqueueAction(Action action) {
        synchronized (this.pendingActions) {
            pendingActions.add(action);
        }
    }
    
    public boolean isHeiCha() {
        return isHeiCha;
    }

    /**
     * Remove `cards` from current user's hand and add them to the user u's hand
     * 
     * Note: we should guarantee that cards are within current user's posession 
     * before invoking this method
     * 
     * @param u         the user to give the cards to
     * @param cards     the cards to remove from user's hand and give to the user
     */
    public void giveCardsTo(User u, List<Card> cards) {
        u.acceptCards(cards);
        for (Card c: cards) {
            this.hand.remove(c);
        }
    }

    /**
     * Add `cards` to the user's hand
     * 
     * @param cards     the cards to be added
     */
    public void acceptCards(List<Card> cards) {
        for (Card c: cards) {
            hand.add(c);
        }
    }
}
