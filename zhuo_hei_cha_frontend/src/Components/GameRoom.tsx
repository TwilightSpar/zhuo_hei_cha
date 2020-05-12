import React from 'react';
import { Container, Col, Row } from 'react-bootstrap';

type IGameRoomProps = {
    children: React.ReactNodeArray
}

const GameRoom: React.FunctionComponent<IGameRoomProps> = (props) => {
    return (
        <Container fluid>
            <Row style={{height: '100%'}}>
                <Col lg={10} style={{backgroundColor: 'green' }}>
                    {props.children[0]}
                </Col>
                <Col lg={2}>
                    {props.children[1]}
                    <hr />
                </Col>
            </Row>
        </Container>
    )
}

export default GameRoom;