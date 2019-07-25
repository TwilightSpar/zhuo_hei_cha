public class Action {

    enum ActionType {
        PLAY_CARD,
        GIVE_CARD
    }

    private final User source;
    private final User target;
    private final ActionType type;
    private final Object data;

    public Action(User src, User dest, ActionType t, Object d) {
        this.source = src;
        this.target = dest;
        this.type = t;
        this.data = d;
    }

    /**
     * @return the source
     */
    public User getSource() {
        return source;
    }

    /**
     * @return the target
     */
    public User getTarget() {
        return target;
    }

    /**
     * @return the type
     */
    public ActionType getType() {
        return type;
    }

    /**
     * @return the data
     */
    public Object getData() {
        return data;
    }
}