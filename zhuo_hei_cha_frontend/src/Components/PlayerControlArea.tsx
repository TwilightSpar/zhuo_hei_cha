import React, { useState, Fragment } from 'react';
import { Row } from 'react-bootstrap';
import _ from 'lodash';
import GameButtons from './GameButtons';
import PokerHand from './PokerHand';


type IPlayerControlAreProps = {
    setLastHand: (lastHand: string[]) => void
}

const testCards = _.flatten(_.range(3, 11).map(n => n.toString()).concat('J', 'Q', 'K', 'A', '2').map(n => {
    return ['C', 'D', 'H', 'S'].map(s => n + s)
}));

const PlayerControlArea: React.FunctionComponent<IPlayerControlAreProps> = (props) => {
    const [hand, setHand] = useState<string[]>(testCards);
    const [selectedHand, setSelectedHand] = useState<Set<string>>(new Set());

    return (
        <Fragment>
            <Row style={{backgroundColor: 'blue'}}>
                <GameButtons
                    handlePlayHandClick={() => {
                        // remove cards from hand and clear selectedHand
                        
                        // need server validation here
                        const isValid = true;
                        if (selectedHand.size !== 0 && isValid) {
                            setHand(hand.filter(card => !selectedHand.has(card)));
                            props.setLastHand(Array.from(selectedHand).sort(cardComparator));
                            setSelectedHand(new Set());
                        }
                    }}
                    handleSkipClick={() => {}}
                    handleAceGoPublicClick={() => {}}
                />
            </Row>
            <Row style={{backgroundColor: 'purple'}}>
                <PokerHand
                    hand={hand}
                    selectedHand={selectedHand}
                    onSelectCard={(cardName: string, isSelected: boolean) => {
                        if (isSelected) {
                            setSelectedHand(new Set(selectedHand.add(cardName)));
                        } else {
                            const tempSet = new Set(selectedHand);
                            tempSet.delete(cardName);
                            setSelectedHand(tempSet);
                        }
                    }}
                />
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