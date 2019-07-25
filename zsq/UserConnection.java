import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

public class UserConnection implements Runnable {
    
    private InputStream input;
    private OutputStream output;
    private int chamberID;

    public UserConnection(Socket socket) {
        try {
            this.input = socket.getInputStream();
            this.output = socket.getOutputStream();
        } catch (Exception e) {
            System.err.println("Exception in constructing UserConnection Object");
        }
    }

    private void getChamberID() {

    }
    
    @Override
    public void run() {
        
    }
}

