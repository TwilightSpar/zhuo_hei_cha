import React, { useState } from 'react';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';
import HubClient from '../Services/HubClient';

const signalREndpointUrl = 'http://localhost:5000/playerhub';

type ISampleProps = {}

const SampleComponent: React.FunctionComponent<ISampleProps> = () => {
    const [connection, setConnection] = useState<HubConnection | null>(null);
    
    return(
        <div>
            <button onClick={() => initConnection(setConnection)}>Connect to server</button>
            <br />
            <button onClick={() => invokeServerMethod(connection)}>Invoke server method</button>
            <br />
            <button onClick={() => StartGameFrontend(connection)}>StartGame</button>
            <br />
            <button onClick={() => enterRoom(connection)}>enter room</button>
            
        </div>
    );
};

// helper methods below

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

const invokeServerMethod = async (conn: HubConnection | null) => {
    if (!conn) {
        alert('Please connect to the server first!');
        return;
    }

    await conn.invoke('SendMessage');
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

export default SampleComponent;