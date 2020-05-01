import React, { useState } from 'react';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';
import HubClient from '../Services/HubClient';

const signalREndpointUrl = 'http://localhost:5000/samplehub';

type ISampleProps = {}

const SampleComponent: React.FunctionComponent<ISampleProps> = () => {
    const [connection, setConnection] = useState<HubConnection | null>(null);
    
    return(
        <div>
            <button onClick={() => initConnection(setConnection)}>Connect to server</button>
            <br />
            <button onClick={() => invokeServerMethod(connection)}>Invoke server method</button>
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

export default SampleComponent;