import React, { FunctionComponent, CSSProperties } from 'react';
import Button from 'react-bootstrap/Button';

type IGameButtonsProps = {
    handlePlayHandClick: () => void,
    handleSkipClick: () => void
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
        </div>
    )
}

const buttonStyle: CSSProperties = {
    marginLeft: 10,
    marginRight: 10
};

const style: CSSProperties = {
    marginBottom: 40,
    marginTop: 40,
    marginLeft: 'auto',
    marginRight: 'auto'
}

export default GameButtons;