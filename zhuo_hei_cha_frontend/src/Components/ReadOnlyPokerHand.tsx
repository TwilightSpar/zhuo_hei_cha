import React, { CSSProperties, Fragment } from 'react';
import PokerCard, { CARD_HEIGHT, CARD_WIDTH, CARD_WIDTH_OVERLAPPED } from './PokerCard';
import { getImageStyle } from './PokerHand';

type IReadOnlyPokerHandProps = {
    hand: string[];     // hand should be already sorted
}

const ReadOnlyPokerHand: React.FunctionComponent<IReadOnlyPokerHandProps> = (props) => {
    if(props.hand.length === 0)
        return(<Fragment/>)
    const cards = props.hand.map((name, index) =>
        <PokerCard
            key={name}
            cardName={name}
            style={getImageStyle(index)}
            isSelected={false}
            onSelectCard={() => {}}
        />
    );

    return (
        <div style={getReadOnlyPokerHandStyle(props.hand.length)}>
            {/* document flow placeholder */}
            <div style={{ height: CARD_HEIGHT, width: '100%' }} />
            {cards}
        </div>
    )
}

const getReadOnlyPokerHandStyle = (n: number): CSSProperties => {
    return {
        width: (n - 1) * CARD_WIDTH_OVERLAPPED + CARD_WIDTH,
        height: '100%',
        position: 'relative',
        // backgroundColor: 'blue',
        marginLeft: 'auto',
        marginRight: 'auto',
        marginTop: 10,
        marginBottom: 10
    };
};

export default ReadOnlyPokerHand;