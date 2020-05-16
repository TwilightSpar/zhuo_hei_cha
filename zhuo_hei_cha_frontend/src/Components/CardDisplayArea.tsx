import React, { CSSProperties, Fragment } from 'react';
import ReadOnlyPokerHand from './ReadOnlyPokerHand';
import PlayerModel from '../Models/PlayerModel';

type ICardDisplayArea = {
    playerList: PlayerModel[]
}

const CardDisplayArea: React.FunctionComponent<ICardDisplayArea> = (props) => {
    if(props.playerList.length === 0)
        return(<Fragment/>);
    const opponents = props.playerList.filter(p => !p.isMe);
    const currentPlayer = props.playerList.filter(p => p.isMe)[0];
    return (
        <div style={cardDisplayAreaStyle}>
            {opponents.map((player, index) =>
                <div style={{flexBasis: '50%', backgroundColor: tempColors[index]}} key={player.connectionId}>
                    <label>Cards Remaining: {player.cardCount}</label>
                    <ReadOnlyPokerHand hand={player.lastHand} />
                </div>
            )}
            <hr style={{width: '100%', visibility: 'hidden'}} />
            <div style={{flexBasis: '50%'}}>
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