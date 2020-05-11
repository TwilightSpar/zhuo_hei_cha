import React from 'react';
import { Link } from 'react-router-dom';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Redirect } from "react-router-dom";
import { connect } from 'http2';


const signalREndpointUrl = 'http://localhost:5000/playerhub';

type IHomeState = {
    conn: HubConnection,
    isGameStarted: boolean
};

class Home extends React.Component<{}, IHomeState> {
    constructor(props: {}) {
        super(props);
        this.state = {
            conn: new HubConnectionBuilder().withUrl(signalREndpointUrl).build(),
            isGameStarted: false
        }
    }
    initConnection = () => {
        this.state.conn.start().then(() => {
            this.state.conn.invoke('CreatePlayerBackend', "aaa");    // enter username
            this.state.conn.on('NotifyOthersFrontend', this.NotifyOthersFrontend);
        });

    }

    StartGameFrontend = () => {
        this.state.conn.invoke('StartGameBackend');
    }

    NotifyOthersFrontend = () => {
        this.setState({
            ...this.state,
            isGameStarted: true
        })
    }

    // end game at anytime, but how to stop backend?
    EndGameFrontend = async () => {
        this.state.conn.stop();
    };

    render() {
        if (this.state.isGameStarted)
            return <Redirect to="/game" Connect=conn/>

        else return (
            <div>
                <button onClick={this.initConnection}>Enter Room</button>
                <br />

                <button onClick={this.StartGameFrontend}>Start Game</button>
                <br />

            </div>

        );
    }

}

// const Home: React.FunctionComponent<IProps> = () => {
//     return (
//         <div className="App">
//             <header className="App-header">

//                 <SampleComponent />
//             </header>
//         </div>
//     )
// }

export default Home;