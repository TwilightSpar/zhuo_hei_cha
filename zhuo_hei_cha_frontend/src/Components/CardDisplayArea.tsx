import React, { CSSProperties } from 'react';
import ReadOnlyPokerHand from './ReadOnlyPokerHand';
import PlayerModel from '../Models/PlayerModel';

type ICardDisplayArea = {
    playerList: PlayerModel[],
    lastHand: string[]
}

const CardDisplayArea: React.FunctionComponent<ICardDisplayArea> = (props) => {
    const opponents = props.playerList.slice(0, props.playerList.length - 1);
    const currentPlayer = props.playerList[props.playerList.length - 1];
    return (
        <div style={cardDisplayAreaStyle}>
            {opponents.map((player, index) =>
                <div style={{flexBasis: '50%', backgroundColor: tempColors[index]}} key={player.id}>
                    <ReadOnlyPokerHand hand={player.lastHand} />
                </div>
            )}
            <hr style={{width: '100%', visibility: 'hidden'}} />
            <div style={{flexBasis: '50%', backgroundColor: 'purple'}}>
                <ReadOnlyPokerHand hand={currentPlayer.lastHand} />
            </div>
        </div>
    )
}

const cardDisplayAreaStyle: CSSProperties = {
    display: 'flex',
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'center'
}

const tempColors = [
    'yellow',
    'red',
    'white',
    'black',
    'purple'
]

export default CardDisplayArea;