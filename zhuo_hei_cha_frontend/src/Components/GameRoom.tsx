import React, { Fragment, CSSProperties } from 'react';
import { Container, Col, Row } from 'react-bootstrap';
import PokerHand from './PokerHand';
import _ from 'lodash';

const testCards = _.flatten(_.range(3, 9).map(n => {
    return ['C', 'D', 'H', 'S'].map(s => n + s)
}));

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

const GameCanvas: React.FunctionComponent<{}> = () => {
    // return <label style={GameCanvasStyle}>Canvas here</label>
    return (
        <div style={GameCanvasStyle}>
            <PokerHand cardNames={testCards}/>
        </div>
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

// const GameRoomStyle: CSSProperties = {
//     display: 'flex',
//     flexDirection: 'row',
// };

const GameCanvasStyle: CSSProperties = {
    // backgroundColor: 'grey',
    // paddingTop: '56.25%',
    paddingTop: '45%',
    width: '100%'

};

const PlayerListStyle: CSSProperties = {
    listStyleType: 'none',
    backgroundColor: 'yellow',
    // justifySelf: 'flex-end',
    width: '100%'
}

const PlayerItemStyle: CSSProperties = {

}

export default GameRoom;