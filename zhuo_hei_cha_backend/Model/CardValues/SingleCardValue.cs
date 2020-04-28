public class SingleCardValue: CardValue{
    private readonly int cardNumber;

    public SingleCardValue(int card){
        this.cardNumber = card;
    }
    
    public override bool CompareValue(CardValue otherValue){
        var a = (SingleCardValue)otherValue;
        if (this.cardNumber > a.cardNumber)
            return true;
        else
            return false;
    }
}