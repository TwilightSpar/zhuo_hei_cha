import React, { useState, CSSProperties } from 'react';
import _ from 'lodash';
import PokerHand from './PokerHand';
import GameButtons from './GameButtons';

const testCards = _.flatten(_.range(3, 11).map(n => n.toString()).concat('J', 'Q', 'K', 'A', '2').map(n => {
    return ['C', 'D', 'H', 'S'].map(s => n + s)
}));

const GameCanvas: React.FunctionComponent<{}> = () => {
    const [hand, setHand] = useState<string[]>(testCards);
    const [selectedHand, setSelectedHand] = useState<Set<string>>(new Set());
    const [lastPlayedHand, setLastPlayedHand] = useState<string[]>([]);

    return (
        <div style={GameCanvasStyle}>
            <PokerHand 
                hand={lastPlayedHand}
                selectedHand={new Set()}
                selectCardCallback={()=>{}}
            />
            <GameButtons 
                handlePlayHandClick={() => {
                    // remove cards from hand and clear selectedHand
                    
                    // need server validation here
                    const isValid = true;
                    if (selectedHand.size !== 0 && isValid) {
                        setHand(hand.filter(card => !selectedHand.has(card)));
                        setLastPlayedHand(Array.from(selectedHand));
                        setSelectedHand(new Set());
                    }
                }}
                handleSkipClick={() => {}}
            />
            <PokerHand
                hand={hand}
                selectedHand={selectedHand}
                selectCardCallback={(cardName: string, isSelected: boolean) => {
                    if (isSelected) {
                        setSelectedHand(new Set(selectedHand.add(cardName)));
                    } else {
                        const tempSet = new Set(selectedHand);
                        tempSet.delete(cardName);
                        setSelectedHand(tempSet);
                    }
                }}
            />
        </div>
    )
}

const GameCanvasStyle: CSSProperties = {
    // backgroundColor: 'grey',
    // paddingTop: '56.25%',
    paddingTop: '40%',
    width: '100%'
};

export default GameCanvas;