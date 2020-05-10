import React, { CSSProperties } from 'react';
import { Container } from 'react-bootstrap';

type IGameCanvasProps = {
    children: React.ReactNodeArray
}

const GameCanvas: React.FunctionComponent<IGameCanvasProps> = (props) => {
    return (
        <Container style={GameCanvasStyle}>
            {props.children[0]}
            {props.children[1]}
        </Container>
    )
}

const GameCanvasStyle: CSSProperties = {
    width: '100%',
    padding: 0
};

export default GameCanvas;