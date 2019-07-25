public interface ProducerConsumer {
    public void processPendingActions();
    public void enqueueAction(Action action);
}