import React from 'react';
import PlayerControlArea from './PlayerControlArea';
import GameButtons from './GameButtons';
import PokerHand from './PokerHand';
import { HubConnection } from '@aspnet/signalr';
import PlayerModel from '../Models/PlayerModel';

type IPlayerControlAreaContainerProps = {
    conn: HubConnection,
    playerList: PlayerModel[]
}

type IPlayerControlAreaContainerState = {
    hand: string[],
    selectedHand: Set<string>,
    isAskingBlackAceGoPublic: boolean,
    isCurrentPlayerTurn: boolean,
    isAskingPlayOneMoreRound: boolean,
    isAskingReturnTribute: boolean
}

// A container component for PlayerControlArea that handles the data manipulation
class PlayerControlAreaContainer extends React.Component<
    IPlayerControlAreaContainerProps, IPlayerControlAreaContainerState
> {
    
    constructor(props: IPlayerControlAreaContainerProps) {
        super(props);
        this.state = {
            hand: [],
            selectedHand: new Set(),
            isAskingBlackAceGoPublic: false,
            isCurrentPlayerTurn: false,
            isAskingPlayOneMoreRound: false,
            isAskingReturnTribute: false
        }
    }

    componentDidMount() {
        // register methods used by this component
        this.props.conn.on('HandIsValidFrontend', this.HandIsValidFrontend);
        this.props.conn.on('ReturnIsValidFrontend', this.ReturnIsValidFrontend);        
        this.props.conn.on('AskAceGoPublicFrontend', this.AskAceGoPublicFrontend);
        this.props.conn.on('AskForPlayFrontend', this.AskForPlayFrontend);
        this.props.conn.on('AskReturnTributeFrontend', this.AskReturnTributeFrontend);
        this.props.conn.on('DisablePlayerButtons', this.DisablePlayerButtons);
        this.props.conn.on('HideAceGoPublicButton', this.HideAceGoPublicButton);
        this.props.conn.on('SendCurrentCardListFrontend', this.SendCurrentCardListFrontend);
        this.props.conn.on('AskPlayOneMoreRoundFrontend', this.AskPlayOneMoreRoundFrontend);
        this.props.conn.on('HidePlayOneMoreRoundFrontend', this.HidePlayOneMoreRoundFrontend);
        
    }
    AskReturnTributeFrontend = async () =>  {
        this.setState({
            ...this.state,
            isAskingReturnTribute: true
        });
    }
    
    AskPlayOneMoreRoundFrontend= async () => {
        this.setState({
            ...this.state,
            isAskingPlayOneMoreRound: true
        });
    }

    AskAceGoPublicFrontend = async () => {
        this.setState({
            ...this.state,
            isAskingBlackAceGoPublic: true
        });
    }

    AskForPlayFrontend = () => {
        this.setState({
            ...this.state,
            isCurrentPlayerTurn: true
        })
    }

    // to be called by backend if hand validation succeeded
    // if validation failed, backend will call 
    // The HandIsValidFrontend and ReturnIsValidFrontend is very important. 
    // they set selectedHand to empty, so selected can be null in the next time.
    HandIsValidFrontend = () => {
        const newHand = this.state.hand.filter(card => !this.state.selectedHand.has(card));
        this.setState({
            hand: newHand,
            selectedHand: new Set()
        });
    }

    ReturnIsValidFrontend = () => {
        const newHand = this.state.hand.filter(card => !this.state.selectedHand.has(card));
        this.setState({
            hand: newHand,
            selectedHand: new Set()
        });
    }
    

    HideAceGoPublicButton = () => {
        this.setState({
            ...this.state,
            isAskingBlackAceGoPublic: false
        });
    }

    DisablePlayerButtons = () => {
        this.setState({
            ...this.state,
            isCurrentPlayerTurn: false
        })
    }

    HidePlayOneMoreRoundFrontend = () => {
        this.setState({
            ...this.state,
            isAskingPlayOneMoreRound: false
        });
    }

    SendCurrentCardListFrontend = (cards: string[]) => {
        this.setState({
            hand: cards
        })
    }

    onAceGoPublicClick = () => {
        this.props.conn.invoke('ReturnAceGoPublicBackend', true);
        this.props.conn.invoke('showAceIdPlayerListBackend');
    }

    // send the hand to backend for validation
    onPlayHandClick = () => {
        this.props.conn.invoke('ReturnUserHandBackend', Array.from(this.state.selectedHand));
        this.setState({
            ...this.state,
            isCurrentPlayerTurn: false      // temperory disable button.
        })
    }

    // return tribute cards
    onReturnTributeClick = () => {
        this.props.conn.invoke('ReturnTributeBackend', Array.from(this.state.selectedHand));
        this.setState({
            ...this.state,
            isAskingReturnTribute: false      // temperory disable button.
        })
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

    onSkipClick = () => {
        this.props.conn.invoke('ReturnUserHandBackend', []);
        this.setState({
            ...this.state,
            isCurrentPlayerTurn: false      // temperory disable button.
        })
    }
    onPlayOneMoreRoundClick = () => {
        this.props.conn.invoke('ReturnPlayOneMoreRoundBackend', true);
        this.setState({
            ...this.state,
            isAskingPlayOneMoreRound: false
        });
    }
    onQuit = () => {
        this.props.conn.invoke('ReturnPlayOneMoreRoundBackend', false);
        this.setState({
            ...this.state,
            isAskingPlayOneMoreRound: false
        });
    }

    render() {

        return (
            <PlayerControlArea>
                <GameButtons
                    onPlayHandClick={this.onPlayHandClick}
                    onSkipClick={this.onSkipClick}
                    onAceGoPublicClick={this.onAceGoPublicClick}
                    onPlayOneMoreRoundClick = {this.onPlayOneMoreRoundClick}
                    onReturnTributeClick = {this.onReturnTributeClick}
                    onQuit = {this.onQuit}
                    isAskingBlackAceGoPublic={this.state.isAskingBlackAceGoPublic}
                    isCurrentPlayerTurn={this.state.isCurrentPlayerTurn}
                    isAskingPlayOneMoreRound = {this.state.isAskingPlayOneMoreRound}
                    isAskingReturnTribute = {this.state.isAskingReturnTribute}
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