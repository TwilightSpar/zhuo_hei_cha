import React, { CSSProperties } from 'react';
import { Container, Col, Row, ListGroup } from 'react-bootstrap';
import GameCanvas from './GameCanvas';
import PlayerModel from '../Models/PlayerModel';

type IPlayerListProps = {
    playerList: PlayerModel[],
    activePlayerIndex: number
};

type IPlayerListItemProps = {
    player: PlayerModel,
    isActivePlayer: boolean
};

const BLACK_ACE_CHARACTER = '\u2660';

const getTempPlayerList = (): PlayerModel[] => {
    const p1 = new PlayerModel(1, 'Player1');
    p1.cardCount = 10;
    p1.lastHand = ['3H', '4C', '4S', '6C', '7C', '8C', 'JD', 'AS'];
    p1.isPublicBlackAce = true;

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

const GameRoom: React.FunctionComponent<{}> = () => {
    const activePlayerIndex = 3;
    return (
        <Container fluid>
            <Row style={{height: '100%'}}>
                <Col lg={10} style={{backgroundColor: 'green' }}><GameCanvas playerList={getTempPlayerList()} /></Col>
                <Col lg={2} 
                // style={{backgroundColor: 'red'}}
                >
                    <PlayerList playerList={getTempPlayerList()} activePlayerIndex={activePlayerIndex} />
                    <hr />
                </Col>
            </Row>
        </Container>
    )
}

const PlayerList: React.FunctionComponent<IPlayerListProps> = (props) => {
    const playerList = props.playerList.map((p, index) => 
        <PlayerListItem 
            player={p}
            isActivePlayer={props.activePlayerIndex === index}
        />
    );
    return (
        <ListGroup style={PlayerListStyle}>
            {playerList}
        </ListGroup>
    )
}

const PlayerListItem: React.FunctionComponent<IPlayerListItemProps> = (props) => {
    return (
        <ListGroup.Item
            style={playerItemStyle}
            active={props.isActivePlayer}
        >
            <text>{props.player.name}</text>
            {props.player.isPublicBlackAce ?
                <text style={{float: 'right'}}>{BLACK_ACE_CHARACTER}</text> :
                null}
        </ListGroup.Item>
    )
}

const PlayerListStyle: CSSProperties = {
    listStyleType: 'none',
    width: '100%',
    padding: 10,
}

const playerItemStyle: CSSProperties = {
    fontSize: 16,
    paddingTop: 15,
    textAlign: 'start'
}

export default GameRoom;