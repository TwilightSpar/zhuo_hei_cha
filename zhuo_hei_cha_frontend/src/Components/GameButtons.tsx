import React, { FunctionComponent, CSSProperties } from 'react';
import Button from 'react-bootstrap/Button';

type IGameButtonsProps = {
    handlePlayHandClick: () => void,
    handleSkipClick: () => void,
    handleAceGoPublicClick: () => void
};

const GameButtons: FunctionComponent<IGameButtonsProps> = (props) => {
    return (
        <div style={style}>
            <Button variant="light" style={buttonStyle} onClick={props.handlePlayHandClick}>
                Play Hand
            </Button>
            <Button variant="light" style={buttonStyle} onClick={props.handleSkipClick}>
                Skip
            </Button>
            <Button variant="light" style={buttonStyle} onClick={props.handleAceGoPublicClick}>
                Ace Go Public
            </Button>
        </div>
    )
}

const buttonStyle: CSSProperties = {
    marginLeft: 10,
    marginRight: 10
};

const style: CSSProperties = {
    marginBottom: 15,
    marginTop: 15,
    marginLeft: 'auto',
    marginRight: 'auto'
}

export default GameButtons;