import React, { Fragment } from 'react';
import { Row } from 'react-bootstrap';
import _ from 'lodash';

type IPlayerControlAreProps = {
    children: React.ReactNodeArray
}

const PlayerControlArea: React.FunctionComponent<IPlayerControlAreProps> = (props) => {
    return (
        <Fragment>
            <Row style={{backgroundColor: 'blue'}}>
                {props.children[0]}
            </Row>
            <Row style={{backgroundColor: 'purple'}}>
                {props.children[1]}
            </Row>
        </Fragment>
    )
}

// compare function for sorting cards
const cardComparator = (c1: string, c2: string): number => {
    c1 = c1.substring(0, c1.length - 1);
    c2 = c2.substring(0, c2.length - 1);

    const n1 = RANK_MAP.get(c1);
    const n2 = RANK_MAP.get(c2);
    if (!n1 || !n2) {
        throw new Error('Invalid card name used for comparison');
    } else {
        return n1 - n2;
    }
}

// maps card ranks to numbers
const RANK_MAP = new Map([
    ['3', 3],
    ['4', 4],
    ['5', 5],
    ['6', 6],
    ['7', 7],
    ['8', 8],
    ['9', 9],
    ['10', 10],
    ['J', 11],
    ['Q', 12],
    ['K', 13],
    ['A', 14],
    ['2', 15],
])

export default PlayerControlArea;