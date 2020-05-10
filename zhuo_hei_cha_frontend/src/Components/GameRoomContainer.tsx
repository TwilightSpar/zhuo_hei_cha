import React, { Fragment } from 'react';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import GameRoom from './GameRoom';
import GameCanvas from './GameCanvas';
import PlayerList from './PlayerList';
import CardDisplayArea from './CardDisplayArea';
import PlayerControlAreaContainer from './PlayerControlAreaContainer';
import PlayerModel from '../Models/PlayerModel';
import Alert from 'react-bootstrap/Alert';

type IGameRoomContainerState = {
    conn: HubConnection,
    playerList: PlayerModel[],
    activePlayerIndex: number,
    errorMessage: string,
    errorVisible: boolean
}

type PlayerListUpdateObject = {
    playerId: string;
    lastHand: string[];
}


class GameRoomContainer extends React.Component<{}, IGameRoomContainerState> {

    constructor(props: {}) {
        super(props);

        this.state = {
            conn: new HubConnectionBuilder().withUrl('http://localhost:5000/playerhub').build(),
            playerList: getTempPlayerList(),
            activePlayerIndex: 0,
            errorMessage: '',
            errorVisible: false
        };
    }

    componentDidMount() {
        this.state.conn.start().then(() => {
            // registering methods
            // this.state.conn.on('onPlayerListUpdate', this.onPlayerListUpdate);
            this.state.conn.on('showErrorMessage', this.showErrorMessage);

            // initialize state
            // this.initPlayerList();
            this.state.conn.invoke('CreatePlayerBackend', 'blaname');
        });
    }

    initPlayerList = () => {
        this.state.conn.invoke('GetAllPlayers').then((playerList: PlayerModel[]) => {
            this.setState({
                ...this.state,
                playerList: playerList
            });
        });
    }

    // onPlayerListUpdate = (obj: PlayerListUpdateObject) => {
    //     this.setState({
    //         playerList: this.state.playerList.map(p => {
    //             if (p.id === obj.playerId) {
    //                 return {
    //                     ...p,
    //                     lastHand: obj.lastHand
    //                 }
    //             } else {
    //                 return p;
    //             }
    //         })
    //     })
    // }

    showErrorMessage = (message: string) => {
        this.setState({
            ...this.state,
            errorMessage: message,
            errorVisible: true
        })

        setTimeout(() => {
            this.setState({
                ...this.state,
                errorVisible: false
            })
        }, 2000)
    }

    render() {
        return (
            <Fragment>
                {/* TODO: implement a better way of displaying error messages or notifications */}
                <div style={{position: 'relative', marginLeft: 'auto', marginRight: 'auto', width: '60%'}}>
                    <Alert
                        variant='danger'
                        show={this.state.errorVisible}
                        style={{position: 'absolute', width: '100%', height: 50, marginTop: 10, zIndex: 5000, textAlign: 'center'}}>
                        {this.state.errorMessage}
                    </Alert>
                </div>
                <GameRoom>
                    <GameCanvas>
                        <CardDisplayArea playerList={this.state.playerList} />
                        <PlayerControlAreaContainer conn={this.state.conn} />
                    </GameCanvas>
                    <PlayerList playerList={this.state.playerList} activePlayerIndex={this.state.activePlayerIndex} />
                </GameRoom>
            </Fragment>
        )
    }
}

const getTempPlayerList = (): PlayerModel[] => {
    const p1 = new PlayerModel(1, 'Player1');
    p1.cardCount = 10;
    p1.lastHand = ['3H', '4C', '4S', '6C', '7C', '8C', 'JD', 'AS'];
    p1.isPublicBlackAce = true;
    p1.isCurrentClient = true;
    p1.remainingHand = ['9H', '9C', '10S', '10C', 'JC', 'JC', 'QD', 'QS', 'KD', 'KH'];

    const p2 = new PlayerModel(2, 'Player2');
    p2.cardCount = 7;
    p2.lastHand = ['AC', 'AD', '2S', '2H'];

    const p3 = new PlayerModel(3, 'Player3');
    p3.cardCount = 3;
    p3.lastHand = ['10C', '10D', 'JS', 'QH', 'QS'];

    const p4 = new PlayerModel(4, 'Player4');
    p4.cardCount = 1;
    p4.lastHand = ['2D', '2C'];

    const p5 = new PlayerModel(5, 'Player5');
    p5.cardCount = 6;
    p5.lastHand = ['3D'];

    return [p1, p2, p3, p4, p5];
}

export default GameRoomContainer;