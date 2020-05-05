import React, { useState, CSSProperties } from 'react';
import PokerCard, { CARD_HEIGHT, CARD_WIDTH, CARD_WIDTH_OVERLAPPED } from './PokerCard';

type HandProps = {
    cardNames: string[]
}

const PokerHand: React.FunctionComponent<HandProps> = (props) => {
    const [hand, setHand] = useState<string[]>(props.cardNames)
    const [selectedHand, setSelectedHand] = useState<string[]>([]);

    const cards = hand.map((name, index) => 
        <PokerCard
            cardName={name}
            style={getImageStyle(index)}
            selectCardCallback={(cardName:string, isSelected: boolean) => {
                if (isSelected) {
                    setSelectedHand(selectedHand.concat(cardName));
                } else {
                    setSelectedHand(selectedHand.filter(name => name !== cardName))
                }
            }} 
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
    };
};

export default PokerHand;