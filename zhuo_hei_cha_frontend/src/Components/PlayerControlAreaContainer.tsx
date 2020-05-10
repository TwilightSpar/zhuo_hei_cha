import React from 'react';
import PlayerControlArea from './PlayerControlArea';
import GameButtons from './GameButtons';
import PokerHand from './PokerHand';
import { HubConnection } from '@aspnet/signalr';
import _ from 'lodash';

type IPlayerControlAreaContainerProps = {
    conn: HubConnection
}

type IPlayerControlAreaContainerState = {
    hand: string[],
    selectedHand: Set<string>,
    isAskingBlackAceGoPublic: boolean
}

const testCards = _.flatten(_.range(3, 11).map(n => n.toString()).concat('J', 'Q', 'K', 'A', '2').map(n => {
    return ['C', 'D', 'H', 'S'].map(s => n + s)
}));

// A container component for PlayerControlArea that handles the data manipulation
class PlayerControlAreaContainer extends React.Component<
    IPlayerControlAreaContainerProps, IPlayerControlAreaContainerState
> {
    
    constructor(props: IPlayerControlAreaContainerProps) {
        super(props);
        this.state = {
            hand: testCards,
            selectedHand: new Set(),
            isAskingBlackAceGoPublic: false
        }
    }

    componentDidMount() {
        // register methods used by this component
        this.props.conn.on('HandIsValidFrontend', this.HandIsValidFrontend);
        this.props.conn.on('AskAceGoPublicFrontend', this.AskAceGoPublicFrontend);
        this.props.conn.on('HideAceGoPublicButton', this.HideAceGoPublicButton);
    }

    AskAceGoPublicFrontend = () => {
        this.setState({
            ...this.state,
            isAskingBlackAceGoPublic: true
        })
    }

    // to be called by backend if hand validation succeeded
    // if validation failed, backend will call 
    HandIsValidFrontend = () => {
        const newHand = this.state.hand.filter(card => !this.state.selectedHand.has(card));
        this.setState({
            hand: newHand,
            selectedHand: new Set()
        });
    }

    HideAceGoPublicButton = () => {
        setTimeout(() => {
            this.setState({
                ...this.state,
                isAskingBlackAceGoPublic: false
            })
        }, 2000)
    }

    onAceGoPublicClick = () => {
        this.props.conn.invoke('ReturnAceGoPublicBackend', true)
    }

    // send the hand to backend for validation
    onPlayHandClick = () => {
        // this.props.conn.invoke('ReturnUserHandBackend', Array.from(this.state.selectedHand));
        this.props.conn.invoke('Test');
    }

    onSelectCard = (cardName: string, isSelected: boolean) => {
        if (isSelected) {
            this.setState({
                selectedHand: new Set(this.state.selectedHand).add(cardName)
            })
        } else {
            const newSelectedHand = new Set(this.state.selectedHand);
            newSelectedHand.delete(cardName);
            this.setState({
                selectedHand: newSelectedHand
            })
        }
    }

    onSkipClick = () => {}

    render() {

        return (
            <PlayerControlArea>
                <GameButtons
                    onPlayHandClick={this.onPlayHandClick}
                    onSkipClick={this.onSkipClick}
                    onAceGoPublicClick={this.onAceGoPublicClick}
                    isAskingBlackAceGoPublic={this.state.isAskingBlackAceGoPublic}
                />
                <PokerHand
                    hand={this.state.hand}
                    selectedHand={this.state.selectedHand}
                    onSelectCard={this.onSelectCard}
                />
            </PlayerControlArea>
        )
    }
}

export default PlayerControlAreaContainer;