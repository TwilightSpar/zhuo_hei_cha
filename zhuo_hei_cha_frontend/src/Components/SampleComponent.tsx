import React, { useState } from 'react';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';
import HubClient from '../Services/HubClient';
import { Link } from 'react-router-dom';

const signalREndpointUrl = 'http://localhost:5000/playerhub';

type ISampleProps = {}

const SampleComponent: React.FunctionComponent<ISampleProps> = () => {
    const [connection, setConnection] = useState<HubConnection | null>(null);
    
    return(
        <div>
            <button onClick={() => initConnection(setConnection)}>Connect to server</button>
            <br />

            <Link to='/game'>
                <button onClick={() => StartGameFrontend(connection)}>StartGame</button>
            <br />
            </Link>

            <button onClick={() => enterRoom(connection)}>enter room</button>
            
        </div>
    );
};

const startGameFrontend = async ()=>{
    
}

// helper methods below. Directly invoke backend methods

const initConnection = async (callback: any) => {
    const conn = new HubConnectionBuilder()
        .withUrl(signalREndpointUrl)
        .build();
    
    if (!conn) {
        alert('Unable to establish connection to the server!')
        return;
    }
    
    await conn.start();
    HubClient.registerClientMethods(conn);
    callback(conn);
};


const StartGameFrontend = async (conn: HubConnection | null) => {
    alert('invoke startgame on the server!');
    if (!conn) {
        alert('Please connect to the server first!');
        return;
    }
    await conn.invoke('StartGameBackend');
};

const enterRoom = async (conn: HubConnection | null) => {
    if (!conn) {
        alert('Please connect to the server first!');
        return;
    }
    await conn.invoke('CreatePlayerBackend',"aaa");
};

// end game at anytime, but how to stop backend?
const EndGameFrontend = async (conn: HubConnection | null) => {
    conn?.stop();
};

export default SampleComponent;