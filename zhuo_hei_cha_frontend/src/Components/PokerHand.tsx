import React, { CSSProperties } from 'react';
import PokerCard, { CARD_HEIGHT, CARD_WIDTH, CARD_WIDTH_OVERLAPPED } from './PokerCard';

type IPokerHandProps = {
    hand: string[],         // hand should be already sorted
    selectedHand: Set<string>,
    onSelectCard: (cardName: string, isSelected: boolean) => void
}

const PokerHand: React.FunctionComponent<IPokerHandProps> = (props) => {
    const { hand, selectedHand, onSelectCard } = props;

    const cards = hand.map((name, index) => 
        <PokerCard
            key={name}
            cardName={name}
            style={getImageStyle(index)}
            isSelected={selectedHand.has(name)}
            onSelectCard={onSelectCard}
        />
    );
    return (
        <div style={getPokerHandStyle(hand.length)}>
            {/* Document flow placeholder */}
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
        top: 0
    }
};

const getPokerHandStyle = (n: number): CSSProperties => {
    return {
        width: (n - 1) * CARD_WIDTH_OVERLAPPED + CARD_WIDTH,
        height: '100%',
        position: 'relative',
        marginLeft: 'auto',
        marginRight: 'auto',
        marginTop: 25,
        marginBottom: 20
    };
};


export default PokerHand;
export {
    getImageStyle
}