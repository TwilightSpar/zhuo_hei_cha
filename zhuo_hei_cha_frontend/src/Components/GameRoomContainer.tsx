import React, { Fragment } from 'react';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import GameRoom from './GameRoom';
import GameCanvas from './GameCanvas';
import PlayerList from './PlayerList';
import CardDisplayArea from './CardDisplayArea';
import PlayerControlAreaContainer from './PlayerControlAreaContainer';
import PlayerModel, { IPlayerObject } from '../Models/PlayerModel';
import Alert from 'react-bootstrap/Alert';

interface IGameRoomContainerProps {
    conn: HubConnection
    // history: History
}

interface IGameRoomContainerState {
    conn: HubConnection,
    playerList: PlayerModel[],
    activePlayerIndex: number,
    errorMessage: string,
    errorVisible: boolean
}


class GameRoomContainer extends React.Component<
    IGameRoomContainerProps, IGameRoomContainerState
    > {
    constructor(props: IGameRoomContainerProps) {
        super(props);

        this.state = {
            conn: this.props.conn,
            playerList: [],
            activePlayerIndex: 0,
            errorMessage: '',
            errorVisible: false,
        };

        this.initPlayerList();
    }

    componentDidMount() {

        this.state.conn.on('PlayerListUpdateFrontend', this.PlayerListUpdateFrontend);
        this.state.conn.on('GameOverFrontend', this.GameOverFrontend);        
        this.state.conn.on('showErrorMessage', this.showErrorMessage);
        this.state.conn.on('ShowCurrentPlayerTurnFront', this.ShowCurrentPlayerTurnFront);
        this.state.conn.on('showAceIdPlayerListFrontend', this.showAceIdPlayerListFrontend);
    }
    GameOverFrontend(blackAceLose: boolean) {
        if(blackAceLose)
            alert("GameOver,and non-blackAce win");
        else
            alert("GameOver,and blackAce win");
    }

    initPlayerList = () => {
        this.state.conn.invoke("getMyConnectionId").then((myConnectionId: string) => {
            this.state.conn.invoke('GetAllPlayers').then((playerObjects: IPlayerObject[]) => {
                this.setState({
                    ...this.state,
                    playerList: playerObjects.map((p: IPlayerObject) => {
                        const player = new PlayerModel(p);
                        if (player.connectionId === myConnectionId)
                            player.isMe = true;
                        return player;
                    })
                })
            });
        });
    }

    PlayerListUpdateFrontend = (lastHand: string[], lastPlayerId: string) => {
        this.setState({
            ...this.state,
            playerList: this.state.playerList.map(p => {                
                if (p.connectionId === lastPlayerId)
                    p.lastHand = lastHand;
                return p;
            })
        })
    }

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
    ShowCurrentPlayerTurnFront = (currentPlayerIndex: number) => {
        this.setState({
            ...this.state,
            activePlayerIndex: currentPlayerIndex
        })
    }
    showAceIdPlayerListFrontend = (aceId: string) => {
        this.setState({
            ...this.state,
            playerList: this.state.playerList.map(p => {                
                if (p.connectionId === aceId)
                    p.isBlackAcePublic = true;
                return p;
            })
        })
    }

    render() {
        return (
            <Fragment>
                {/* TODO: implement a better way of displaying error messages or notifications */}
                <div style={{ position: 'relative', marginLeft: 'auto', marginRight: 'auto', width: '60%' }}>
                    <Alert
                        variant='danger'
                        show={this.state.errorVisible}
                        style={{ position: 'absolute', width: '100%', height: 50, marginTop: 10, zIndex: 5000, textAlign: 'center' }}>
                        {this.state.errorMessage}
                    </Alert>
                </div>
                <GameRoom>
                    <GameCanvas>
                        <CardDisplayArea playerList= {this.state.playerList} />
                        <PlayerControlAreaContainer conn={this.state.conn} playerList={this.state.playerList}/>
                    </GameCanvas>
                    <PlayerList playerList={this.state.playerList} activePlayerIndex={this.state.activePlayerIndex}/>
                </GameRoom>
            </Fragment>
        )
    }
}

export default GameRoomContainer;