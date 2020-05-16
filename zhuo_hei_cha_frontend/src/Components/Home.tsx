import React from 'react';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import GameRoomContainer from './GameRoomContainer';


const signalREndpointUrl = 'http://localhost:5000/playerhub';

type IHomeState = {
    conn: HubConnection,
    isGameStarted: boolean,
    isEnterRoomAble: boolean,
    playerName: string
};

class Home extends React.Component<{}, IHomeState> {
    

    constructor(props: {}) {
        super(props);
        this.state = {
            conn: new HubConnectionBuilder().withUrl(signalREndpointUrl).build(),
            isGameStarted: false,
            isEnterRoomAble: true,
            playerName: ""
        }
    }
    initConnection = () => {
        this.state.conn.start().then(() => {
            this.state.conn.invoke('CreatePlayerBackend', this.state.playerName);    // enter username
            this.state.conn.on('NotifyOthersFrontend', this.NotifyOthersFrontend);
            this.state.conn.on('BreakGameFrontend', this.BreakGameFrontend);
        });
        this.setState({
            ...this.state,
            isEnterRoomAble: false
        })

    }

    BreakGameFrontend = () => {
        this.state.conn.stop().then(() => {
            this.setState({
                ...this.state,
                isGameStarted: false,
                isEnterRoomAble: true
            })
        })
    }

    onEnterName = (event: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            playerName: event.currentTarget.value
        })
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

    render() {
        if (this.state.isGameStarted)
            return <GameRoomContainer conn={this.state.conn} />

        else return (
            <div>
                <input type="text" placeholder="enter username" onChange={this.onEnterName}/>
                <button onClick={this.initConnection} disabled={!this.state.isEnterRoomAble}>Enter Room</button>
                
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