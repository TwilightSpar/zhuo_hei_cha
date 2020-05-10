import React, { FunctionComponent, CSSProperties } from 'react';
import Button from 'react-bootstrap/Button';

type IGameButtonsProps = {
    onPlayHandClick: () => void,
    onSkipClick: () => void,
    onAceGoPublicClick: () => void,
    isAskingBlackAceGoPublic: boolean
};

const GameButtons: FunctionComponent<IGameButtonsProps> = (props) => {
    return (
        <div style={style}>
            <Button variant="light" style={buttonStyle} onClick={props.onPlayHandClick}>
                Play Hand
            </Button>
            <Button variant="light" style={buttonStyle} onClick={props.onSkipClick}>
                Skip
            </Button>
            {props.isAskingBlackAceGoPublic ?            
                <Button variant="light" style={buttonStyle} onClick={props.onAceGoPublicClick}>
                    Ace Go Public
                </Button> 
                : null
            }
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