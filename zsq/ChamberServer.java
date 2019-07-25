import java.io.IOException;
import java.io.OutputStream;
import java.net.InetSocketAddress;

import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpServer;


public class ChamberServer {

    private class ChamberServerHandler implements HttpHandler {
        @Override
        public void handle(HttpExchange t) throws IOException {
            String response = t.getRequestURI().getPath();
            t.sendResponseHeaders(200, response.length());
            OutputStream os = t.getResponseBody();
            os.write(response.getBytes());
            os.close();
        }
    }

    private final HttpServer chamberServer;
    
    public ChamberServer() throws IOException {
        try {
            chamberServer = HttpServer.create(new InetSocketAddress("localhost", 9999), 10);
        } catch (Exception e) {
            System.err.println("Exception in initializing chamberServer");

            throw e;
        }

        chamberServer.createContext("/createChamber", new ChamberServerHandler());
        chamberServer.start();
    }

    public static void main(String[] args) throws IOException {
        new ChamberServer();
    }
}