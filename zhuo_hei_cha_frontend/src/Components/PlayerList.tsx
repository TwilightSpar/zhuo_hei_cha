import React, { CSSProperties } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import PlayerModel from '../Models/PlayerModel';

const BLACK_ACE_CHARACTER = '\u2660';
const DEALER_ARROW = '\u261A'

type IPlayerListProps = {
    playerList: PlayerModel[],
    activePlayerIndex: number
};

type IPlayerListItemProps = {
    player: PlayerModel,
    isActivePlayer: boolean
};

const PlayerList: React.FunctionComponent<IPlayerListProps> = (props) => {
    const playerList = props.playerList.map((p, index) => 
        <PlayerListItem
            key={p.connectionId}
            player={p}
            isActivePlayer={props.activePlayerIndex === index}
        />
    );
    return (
        <ListGroup style={PlayerListStyle}>
            {playerList}
        </ListGroup>
    )
}

const PlayerListItem: React.FunctionComponent<IPlayerListItemProps> = (props) => {
    return (
        <ListGroup.Item
            style={playerItemStyle}
            active={props.player.isMe}
        >
            {props.player.name}
            {props.isActivePlayer?
                <span style={{padding: 5, fontSize: 20}}>{DEALER_ARROW}</span> :
                null}
            {props.player.isBlackAcePublic?
                <span style={{float: 'right', fontSize: 20}}>{BLACK_ACE_CHARACTER}</span> :
                null}
        </ListGroup.Item>
    )
}

const PlayerListStyle: CSSProperties = {
    listStyleType: 'none',
    width: '100%',
    padding: 10,
}

const playerItemStyle: CSSProperties = {
    fontSize: 16,
    paddingTop: 15,
    textAlign: 'start'
}

export default PlayerList;