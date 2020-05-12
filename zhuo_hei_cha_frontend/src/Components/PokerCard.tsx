import React, { CSSProperties } from 'react';

const CARD_HEIGHT = 150;
const CARD_WIDTH = 100;
const CARD_WIDTH_OVERLAPPED = 20;

const CARD_SELECTED_OFFSET = -30;

type CardProps = {
    cardName: string,
    isSelected: boolean,
    style: CSSProperties
    onSelectCard: (cardName: string, isSelected: boolean) => void
}

const PokerCard: React.FunctionComponent<CardProps> = (props) => {
    const { style, cardName, isSelected, onSelectCard } = props;
    return (
        <div style={style}>
            <img 
                className={cardName}
                style={getPokerCardImageStyle(isSelected)}
                src={PokerCardImages[cardName]}
                alt={cardName}
                onClick={(e) => {
                    onSelectCard(e.currentTarget.className, !isSelected);
                }}
            />
        </div>
    )
}

const getPokerCardImageStyle = (isSelected: boolean): CSSProperties => {
    return {
        top: isSelected ? CARD_SELECTED_OFFSET : 0,
        height: CARD_HEIGHT,
        position: 'relative',
    }
}

interface ImageKVP {
    [key: string]: any
}

const PokerCardImages: ImageKVP = {
    '3C': require('../res/cards/3C.png'),
    '3D': require('../res/cards/3D.png'),
    '3H': require('../res/cards/3H.png'),
    '3S': require('../res/cards/3S.png'),
    '4C': require('../res/cards/4C.png'),
    '4D': require('../res/cards/4D.png'),
    '4H': require('../res/cards/4H.png'),
    '4S': require('../res/cards/4S.png'),
    '5C': require('../res/cards/5C.png'),
    '5D': require('../res/cards/5D.png'),
    '5H': require('../res/cards/5H.png'),
    '5S': require('../res/cards/5S.png'),
    '6C': require('../res/cards/6C.png'),
    '6D': require('../res/cards/6D.png'),
    '6H': require('../res/cards/6H.png'),
    '6S': require('../res/cards/6S.png'),
    '7C': require('../res/cards/7C.png'),
    '7D': require('../res/cards/7D.png'),
    '7H': require('../res/cards/7H.png'),
    '7S': require('../res/cards/7S.png'),
    '8C': require('../res/cards/8C.png'),
    '8D': require('../res/cards/8D.png'),
    '8H': require('../res/cards/8H.png'),
    '8S': require('../res/cards/8S.png'),
    '9C': require('../res/cards/9C.png'),
    '9D': require('../res/cards/9D.png'),
    '9H': require('../res/cards/9H.png'),
    '9S': require('../res/cards/9S.png'),
    '10C': require('../res/cards/10C.png'),
    '10D': require('../res/cards/10D.png'),
    '10H': require('../res/cards/10H.png'),
    '10S': require('../res/cards/10S.png'),
    'JC': require('../res/cards/JC.png'),
    'JD': require('../res/cards/JD.png'),
    'JH': require('../res/cards/JH.png'),
    'JS': require('../res/cards/JS.png'),
    'QC': require('../res/cards/QC.png'),
    'QD': require('../res/cards/QD.png'),
    'QH': require('../res/cards/QH.png'),
    'QS': require('../res/cards/QS.png'),
    'KC': require('../res/cards/KC.png'),
    'KD': require('../res/cards/KD.png'),
    'KH': require('../res/cards/KH.png'),
    'KS': require('../res/cards/KS.png'),
    'AC': require('../res/cards/AC.png'),
    'AD': require('../res/cards/AD.png'),
    'AH': require('../res/cards/AH.png'),
    'AS': require('../res/cards/AS.png'),
    '2C': require('../res/cards/2C.png'),
    '2D': require('../res/cards/2D.png'),
    '2H': require('../res/cards/2H.png'),
    '2S': require('../res/cards/2S.png'),
    '52J': require('../res/cards/52J.png'),
    '53J': require('../res/cards/53J.png')
};

export default PokerCard;

export {
    CARD_HEIGHT,
    CARD_WIDTH,
    CARD_WIDTH_OVERLAPPED
}