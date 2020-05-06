export default class PlayerModel {
    id: number
    name: string
    cardCount: number
    lastHand: string[]
    isPublicBlackAce: boolean

    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
        this.cardCount = 0;
        this.lastHand = [];
        this.isPublicBlackAce = false;
    }
}