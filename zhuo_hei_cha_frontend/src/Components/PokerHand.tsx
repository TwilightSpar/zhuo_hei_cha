import React, { useState, CSSProperties } from 'react';
import PokerCard, { CARD_HEIGHT, CARD_WIDTH, CARD_WIDTH_OVERLAPPED } from './PokerCard';

type HandProps = {
    hand: string[],
    selectedHand: Set<string>,
    selectCardCallback: (cardName: string, isSelected: boolean) => void
}

const PokerHand: React.FunctionComponent<HandProps> = (props) => {
    const { hand, selectedHand, selectCardCallback } = props;

    const cards = hand.sort(compareCards).map((name, index) => 
        <PokerCard
            cardName={name}
            style={getImageStyle(index)}
            isSelected={selectedHand?.has(name)}
            selectCardCallback={selectCardCallback}
        />
    );
    return (
        <div style={getHandStyle(hand.length)}>
            <div style={{ height: CARD_HEIGHT, width: '100%' }} />
            {cards}
        </div>
    )
}

const getImageStyle = (index: number): CSSProperties => {
    return {
        zIndex: index,
        left: CARD_WIDTH_OVERLAPPED * index,
        height: CARD_HEIGHT,
        position: 'absolute',
        display: 'inline',
        whiteSpace: 'nowrap',
        top: 0,
        // backgroundColor: 'orange'
    }
};

const getHandStyle = (n: number): CSSProperties => {
    return {
        width: (n - 1) * CARD_WIDTH_OVERLAPPED + CARD_WIDTH,
        height: '100%',
        position: 'relative',
        // backgroundColor: 'blue',
        marginLeft: 'auto',
        marginRight: 'auto',
        marginTop: 20,
        marginBottom: 20
    };
};

const compareCards = (c1: string, c2: string): number => {
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


export default PokerHand;