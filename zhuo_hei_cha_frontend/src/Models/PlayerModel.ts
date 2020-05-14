
export interface IPlayerObject{
    connectionId: string
    name: string
}

export default class PlayerModel {
    readonly connectionId: string
    readonly name: string
    cardCount: number
    isBlackAcePublic: boolean
    isMe: boolean
    lastHand: string[]

    constructor(player: IPlayerObject) {
        this.connectionId = player.connectionId;
        this.name = player.name;
        this.cardCount = 0;
        this.isBlackAcePublic = false;
        this.isMe = false;
        this.lastHand = []
    }
}