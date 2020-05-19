import React, { FunctionComponent, CSSProperties, Fragment } from 'react';
import Button from 'react-bootstrap/Button';

type IGameButtonsProps = {
    onPlayHandClick: () => void,
    onSkipClick: () => void,
    onAceGoPublicClick: () => void,
    onPlayOneMoreRoundClick: () => void,
    onReturnTributeClick: () => void,
    onQuit: () => void,
    isAskingBlackAceGoPublic: boolean,
    isCurrentPlayerTurn: boolean,
    isAskingPlayOneMoreRound: boolean,
    isAskingReturnTribute: boolean
};

const GameButtons: FunctionComponent<IGameButtonsProps> = (props) => {
    return (
        <div style={style}>
            <Button
                variant="light"
                style={buttonStyle}
                onClick={props.onPlayHandClick}
                disabled={!props.isCurrentPlayerTurn}
            >
                Play Hand
            </Button>
            <Button
                variant="light"
                style={buttonStyle}
                onClick={props.onSkipClick}
                disabled={!props.isCurrentPlayerTurn}
            >
                Skip
            </Button>
            {props.isAskingBlackAceGoPublic ?            
                <Button variant="light" style={buttonStyle} onClick={props.onAceGoPublicClick}>
                    Ace Go Public
                </Button> 
                : null
            }
            {props.isAskingReturnTribute ?            
                <Button variant="light" style={buttonStyle} onClick={props.onReturnTributeClick}>
                    Return Tribute
                </Button> 
                : null
            }
            {props.isAskingPlayOneMoreRound ?            
                <Fragment>
                    <Button variant="light" style={buttonStyle} onClick={props.onPlayOneMoreRoundClick}>
                    play one more round
                    </Button>
                    <Button variant="light" style={buttonStyle} onClick={props.onQuit}>
                    quit
                    </Button>
                </Fragment>
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