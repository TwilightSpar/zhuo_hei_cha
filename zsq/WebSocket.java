/**
 * Reference: https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_a_WebSocket_server_in_Java,
 * https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_servers
 */

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.security.MessageDigest;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Base64;
import java.util.List;
import java.util.Scanner;
import java.util.concurrent.Executors;
import java.util.regex.*;
import java.util.regex.Matcher;


public class WebSocket {

    private ServerSocket server;
    private final int PORT_NUMBER = 10000;
    
    public WebSocket() throws IOException {

        server = new ServerSocket(PORT_NUMBER);
        try {
            System.out.println("Server is listening on port " + PORT_NUMBER);
            Socket client = server.accept();
            System.out.println("Incoming connection received!");
            InputStream in = client.getInputStream();
            OutputStream out = client.getOutputStream();
            Scanner s = new Scanner(in, "UTF-8");

            try {

                // establishing websocket connection
                String data = s.useDelimiter("\\r\\n\\r\\n").next();
                Matcher get = Pattern.compile("^GET").matcher(data);

                if (get.find()) {
                    Matcher match = Pattern.compile("Sec-WebSocket-Key: (.*)").matcher(data);
                    match.find();
                    byte[] response = ("HTTP/1.1 101 Switching Protocols\r\n"
                        + "Connection: Upgrade\r\n"
                        + "Upgrade: websocket\r\n"
                        + "Sec-WebSocket-Accept: "
                        + Base64.getEncoder().encodeToString(MessageDigest.getInstance("SHA-1").digest((match.group(1) + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11").getBytes("UTF-8")))
                        + "\r\n\r\n").getBytes("UTF-8");
                    out.write(response, 0, response.length);
                }

                while (true) {
                    Thread.sleep(300);
                    if (in.available() != 0) {

                        byte[] buffer = new byte[4096];
                        int size = in.read(buffer);
                        System.out.println(size);

                        
                        int currentPosition = 0;
                        
                        // reading the first byte for Fin bit
                        byte currentByte = buffer[currentPosition++];
                        if (getBit(currentByte, 7) == 0) {
                            System.out.println("Fin bit not set");
                        } else {
                            System.out.println("Fin bit set");
                        }

                        // reading the second byte for Masked bit
                        currentByte = buffer[currentPosition++];
                        if (getBit(currentByte, 7) == 0) {
                            System.out.println("Masked bit not set");
                        } else {
                            System.out.println("Masked bit set");
                        }

                        // calculating the payload length from the second byte
                        long payloadLen = currentByte & 0x7f;
                        if (payloadLen <= 125) {
                            System.out.println("Payload Len is " + payloadLen);
                        } else if (payloadLen == 126) {
                            // in this case the next two bytes (16 bits) will be 
                            // used to represent the length. However we still 
                            // need to use four bytes to store it, since Java 
                            // can only  interpret a byte array of size that is 
                            // a multiple of 4 as int (or int types in general).
                            // Thus, we will need to fill the two most significant
                            // bytes with 0 and populate the two least significant
                            // bytes with actual data.
                            byte[] actualLen = new byte[4];
                            actualLen[0] = 0;
                            actualLen[1] = 0;
                            actualLen[2] = buffer[currentPosition++];
                            actualLen[3] = buffer[currentPosition++];

                            ByteBuffer wrapped = ByteBuffer.wrap(actualLen);
                            payloadLen = (long) wrapped.getInt();
                            System.out.println("Now payload len is " + payloadLen);
                        } else if (payloadLen == 127) {
                            // in this case we will use the next 8 bytes (64 bits)
                            // to store the size
                            byte[] actualLen = new byte[8];
                            for (int i = 0; i < 8; ++i)
                                actualLen[i] = buffer[currentPosition++];
                            
                            ByteBuffer wrapped = ByteBuffer.wrap(actualLen);
                            payloadLen = wrapped.getLong();
                            System.out.println("Now payload len is " + payloadLen);
                        }

                        // reading the masking keys
                        byte[] keys = new byte[4];
                        for (int i = 0; i < 4; ++i)
                            keys[i] = buffer[currentPosition++];

                        // reading and decoding the payload
                        byte[] decoded = new byte[payloadLen];
                        for (int i = 0; i < payloadLen; ++i) {
                            decoded[i] = (byte) (buffer[currentPosition++] ^ keys[i % 4]);
                        }
                        System.out.println(new String(decoded));
                        System.out.println("Done");
                    }       
                }
            } catch (Exception e) {
                System.err.println("Exception in sending response");
            } finally {
                s.close();
            }
        } catch (Exception e) {
            System.err.println("Exception in establishing connection");
        } finally {
            server.close();
        }
    }

    public int getBit(byte b, int position) {
        return (b >> position) & 1;
    }

    public static void main(String[] args) {
        try {
            new WebSocket();
        } catch (Exception e) {
            System.err.println("Exception in Main method");
        }
    }
}
