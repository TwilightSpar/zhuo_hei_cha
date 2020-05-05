import React, { CSSProperties } from 'react';
import { Container, Col, Row } from 'react-bootstrap';
import GameCanvas from './GameCanvas';

type IProps = {
    player: number
};

const GameRoom: React.FunctionComponent<{}> = () => {
    return (
        <Container fluid>
            <Row>
                <Col lg={10} style={{backgroundColor: 'green'}}><GameCanvas /></Col>
                <Col lg={2} style={{backgroundColor: 'red'}}><PlayerList /></Col>
            </Row>
        </Container>
    )
}

const PlayerList: React.FunctionComponent<{}> = () => {
    const playerList = [1, 2, 3, 4, 5];
    return (
        <ul style={PlayerListStyle}>
            {playerList.map(p => <li>{p}</li>)}
        </ul>
    )
}

const PlayerItem: React.FunctionComponent<IProps> = (props) => {
    return (
        <li style={PlayerItemStyle}>props.player</li>
    )
}

const PlayerListStyle: CSSProperties = {
    listStyleType: 'none',
    backgroundColor: 'yellow',
    // justifySelf: 'flex-end',
    width: '100%'
}

const PlayerItemStyle: CSSProperties = {

}

export default GameRoom;