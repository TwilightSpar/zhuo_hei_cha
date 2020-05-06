export default class PlayerModel {
    id: number
    name: string
    cardCount: number
    lastHand: string[]
    isPublicBlackAce: boolean
    isCurrentClient: boolean

    // the following property will have value only when isCurrentClient is true
    remainingHand?: string[]

    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
        this.cardCount = 0;
        this.lastHand = [];
        this.isPublicBlackAce = false;
        this.isCurrentClient = false;
    }
}