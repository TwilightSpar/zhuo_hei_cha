/**
 * Code from https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_a_WebSocket_server_in_Java
 */

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.security.MessageDigest;
import java.util.Base64;
import java.util.Scanner;
import java.util.concurrent.Executors;
import java.util.regex.*;
import java.util.regex.Matcher;


// public class TestSocket {

//     private ServerSocket server;
//     private final int PORT_NUMBER = 10000;
    
//     public TestSocket() throws IOException {

//         server = new ServerSocket(PORT_NUMBER);
//         try {
//             System.out.println("Server is listening on port " + PORT_NUMBER);
//             Socket client = server.accept();
//             System.out.println("Incoming connection received!");
//             InputStream in = client.getInputStream();
//             OutputStream out = client.getOutputStream();
//             Scanner s = new Scanner(in, "UTF-8");

//             try {
//                 String data = s.useDelimiter("\\r\\n\\r\\n").next();
//                 Matcher get = Pattern.compile("^GET").matcher(data);

//                 if (get.find()) {
//                     Matcher match = Pattern.compile("Sec-WebSocket-Key: (.*)").matcher(data);
//                     match.find();
//                     byte[] response = ("HTTP/1.1 101 Switching Protocols\r\n"
//                         + "Connection: Upgrade\r\n"
//                         + "Upgrade: websocket\r\n"
//                         + "Sec-WebSocket-Accept: "
//                         + Base64.getEncoder().encodeToString(MessageDigest.getInstance("SHA-1").digest((match.group(1) + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11").getBytes("UTF-8")))
//                         + "\r\n\r\n").getBytes("UTF-8");
//                     out.write(response, 0, response.length);
//                     byte[] decoded = new byte[6];
//                     byte[] encoded = new byte[] { (byte) 198, (byte) 131, (byte) 130, (byte) 182, (byte) 194, (byte) 135 };
//                     byte[] key = new byte[] { (byte) 167, (byte) 225, (byte) 225, (byte) 210 };
//                     for (int i = 0; i < encoded.length; i++) {
//                         decoded[i] = (byte) (encoded[i] ^ key[i & 0x3]);
//                     }
//                 }
//             } catch (Exception e) {
//                 System.err.println("Exception in sending response");
//             } finally {
//                 s.close();
//             }
//         } catch (Exception e) {
//             System.err.println("Exception in establishing connection");
//         } finally {
//             server.close();
//         }
//     }

//     public static void main(String[] args) {
//         try {
//             new TestSocket();
//         } catch (Exception e) {
//             System.err.println("Exception in Main method");
//         }
//     }
// }


public class WebSocket {
    private ServerSocket userServer;
    private final int USER_PORT_NUMBER = 10000;

    public WebSocket() {
        try {
            userServer = new ServerSocket(USER_PORT_NUMBER);
            chamberServer = new ServerSocket(CHAMBER_PORT_NUMBER);
        } catch (Exception e) {
            throw e;
        }

        Executors.newSingleThreadExecutor().execute(new Runnable() {
            @Override
            public void run() {
                while (true) {
                    userServer.accept();
                }
            }
        });
    }

    public static void main(String[] args) {
        try {
            new WebSocket();
        } catch (Exception e) {

        }
    }
}