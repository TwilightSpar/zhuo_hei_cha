import { HubConnection } from "@aspnet/signalr";

export default class HubClient {

    // register all the methods to be called by the server
    public static registerClientMethods(conn: HubConnection) {
        conn.on('sampleClientMethod', HubClient.sampleClientMethod);
    }

    private static sampleClientMethod = (): void => {
        alert('sampleClientMethod is being called by the server!')
    }

    private static StartGameFrontend = (conn: HubConnection): void => {
        alert('sampleClientMethod is being called by the server!');
        //conn.on('StartGameBackend')
    }
    
    // return a list of card that users played
    private static AskForPlayFrontend = (conn: HubConnection): void => {
        alert('AskForPlayFrontend is being called by the server!');
        // get cards of user
        // conn.on('ReturnUserHandBackend', cards)
    }
    
    // return a list of card of which users return tribute
    private static AskReturnTributeFrontend = (conn: HubConnection): void => {
        alert('AskReturnTributeFrontend is being called by the server!');
        // return tirbutes
        // conn.on('ReturnTributeBackend', cards)
    }
    
    // return whether all users agree to play one more round
    private static AskPlayOneMoreRoundFrontend = (conn: HubConnection): void => {
        alert('AskPlayOneMoreRoundFrontend is being called by the server!');
        // PlayOneMore = user1 && user2 &&...&&userN 
        // if PlayOneMore == true
        // conn.on('ReturnPlayOneMoreTimeBackend', PlayOneMore)
    }

    // Ask the BlackAce user, whether go public
    private static AskAceGoPublicFrontend = (conn: HubConnection): void => {
        alert('AskAceGoPublicFrontend is being called by the server!');
        // PlayOneMore = user1 && user2 &&...&&userN 
        // if PlayOneMore == true
        // conn.on('ReturnplayOneMoreTime', PlayOneMore)
    }

    
    // alert it is valid
    private static HandIsValidFrontend = (): void => {
        alert('HandIsValidFrontend is being called by the server!');        
    }

    // alert it is not valid
    private static HandIsNotValidFrontend = (): void => {
        alert('HandIsValidFrontend is being called by the server!');        
    }

    
    private static ReturnNotValidFrontend = (): void => {
        alert('ReturnNotValidFrontend is being called by the server!');        
    }
}