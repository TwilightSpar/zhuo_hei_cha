import { HubConnection } from "@aspnet/signalr";

export default class HubClient {

    // register all the methods to be called by the server
    public static registerClientMethods(conn: HubConnection) {
        conn.on('sampleClientMethod', HubClient.sampleClientMethod);
    }

    private static sampleClientMethod = (): void => {
        alert('sampleClientMethod is being called by the server!')
    }


    // invoke by backend, do something then return the value
    
    // return a list of card that users played
    private static AskForPlayFrontend = (conn: HubConnection): void => {
        alert('AskForPlayFrontend is being called by the server!');
        // get cards of user
        // conn.invoke('ReturnUserHandBackend', cards)
    }

     
    // return whether all users agree to play one more round
    private static AskPlayOneMoreRoundFrontend = (conn: HubConnection): void => {
        alert('AskPlayOneMoreRoundFrontend is being called by the server!');
        // PlayOneMore = user1 && user2 &&...&&userN 
        // if PlayOneMore == true
        // conn.invoke('ReturnPlayOneMoreTimeBackend', PlayOneMore)
    }

    
    // return a list of card of which users return tribute
    private static AskReturnTributeFrontend = (conn: HubConnection): void => {
        alert('AskReturnTributeFrontend is being called by the server!');
        // return tirbutes
        // conn.invoke('ReturnTributeBackend', cards)
    }

    // Ask the BlackAce user, whether go public
    private static AskAceGoPublicFrontend = (conn: HubConnection): void => {
        alert('AskAceGoPublicFrontend is being called by the server!');
        // PlayOneMore = user1 && user2 &&...&&userN 
        // if PlayOneMore == true
        // conn.invoke('ReturnAceGoPublicBackend', PlayOneMore)
    }





    // invoke by backend, do not need to return value
    
    // alert it is valid
    private static HandIsValidFrontend = (): void => {
        alert('HandIsValidFrontend is being called by the server!');        
    }

    // alert it is not valid
    private static HandIsNotValidFrontend = (): void => {
        alert('HandIsValidFrontend is being called by the server!');        
    }

    // alert users that their tribute return is not valid
    private static TributeReturnNotValidFrontend = (): void => {
        alert('TributeReturnNotValidFrontend is being called by the server!');        
    }

    // alert users that their cards have changed
    private static SendCurrentCardListFrontend = (): void => {
        alert('SendCurrentCardListFrontend is being called by the server!');
    }

    
}