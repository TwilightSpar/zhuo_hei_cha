import java.util.HashSet;
import java.util.Map;
import java.util.Queue;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executors;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class Chamber implements ProducerConsumer {

    // class variables
    public static final Map<Integer, Chamber> activeChambers = new ConcurrentHashMap<Integer, Chamber>();
    public static final int MAX_NUM_OF_CHAMBER = 20;
    private static int nextChamberID = 0;

    // instance variables
    private final Queue<Action> pendingActions = new LinkedBlockingQueue<Action>();
    private final Queue<User> usersInChamber = new LinkedBlockingQueue<User>(); // might need to reconsider its type
    private Game currentGame = new Game();
    
    private final int chamberID;

    public Chamber(int ID) {
        this.chamberID = ID;

        Executors.newScheduledThreadPool(1).scheduleAtFixedRate(new Runnable() {
            @Override
            public void run() {
                if (pendingActions.size() > 0)
                    processPendingActions();
            }
        }, 500, 500, TimeUnit.MILLISECONDS);
    }

    public addUser(User user) {
        synchronized (this.usersInChamber) {
            usersInChamber.add(user);
        }
    }

    @Override
    public void processPendingActions() {
        synchronized (this.pendingActions) {
            while (pendingActions.size() > 0) {
                Action currentAction = pendingActions.poll();
    
                /**
                 * code to process current actions
                 * 
                 * e.g., 
                 * switch (currentAction.getType) {
                 *      case Action.ActionType.GIVE_CARD:
                 *          List cardList = (List) currentAction.getData();
                 *          currentAction.getSource().giveCardsTo(currentAction.getTarget(), cardList);
                 * 
                 * }
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
}