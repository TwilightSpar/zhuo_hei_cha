public class SingleCardValue: CardValue{
    private readonly int cardNumber;

    public SingleCardValue(int card){
        this.cardNumber = card;
    }
    
    public override bool CompareValue(CardValue otherValue){
        var a = (SingleCardValue)otherValue;
        return this.cardNumber >= a.cardNumber;
    }
}