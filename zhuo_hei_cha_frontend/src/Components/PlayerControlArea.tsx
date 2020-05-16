import React, { Fragment } from 'react';
import { Row } from 'react-bootstrap';

type IPlayerControlAreProps = {
    children: React.ReactNodeArray
}

const PlayerControlArea: React.FunctionComponent<IPlayerControlAreProps> = (props) => {
    return (
        <Fragment>
            <Row>
                {props.children[0]}
            </Row>
            <Row>
                {props.children[1]}
            </Row>
        </Fragment>
    )
}

export default PlayerControlArea;