import { HubConnection } from "@aspnet/signalr";

export default class HubClient {

    // register all the methods to be called by the server
    public static registerClientMethods(conn: HubConnection) {
        conn.on('sampleClientMethod', HubClient.sampleClientMethod);
    }

    private static sampleClientMethod = (): void => {
        alert('sampleClientMethod is being called by the server!')
    }
}