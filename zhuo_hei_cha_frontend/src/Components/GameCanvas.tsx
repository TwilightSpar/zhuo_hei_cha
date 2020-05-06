import React, { useState, CSSProperties } from 'react';
import { Container } from 'react-bootstrap';
import PlayerModel from '../Models/PlayerModel';
import CardDisplayArea from './CardDisplayArea';
import PlayerControlArea from './PlayerControlArea';

type IGameCanvasProps = {
    playerList: PlayerModel[]
}

const GameCanvas: React.FunctionComponent<IGameCanvasProps> = (props) => {
    const [lastHand, setLastHand] = useState<string[]>([]);

    return (
        <Container style={GameCanvasStyle}>
            <CardDisplayArea lastHand={lastHand} playerList={props.playerList} />
            <PlayerControlArea setLastHand={(hand) => {
                setLastHand(hand);

            }} />
        </Container>
    )
}

const GameCanvasStyle: CSSProperties = {
    width: '100%',
    padding: 0
};

export default GameCanvas;