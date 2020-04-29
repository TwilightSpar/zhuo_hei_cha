public class PairCardValue : CardValue
{
    private int pairNumber;
    private int lastNumber;

    public PairCardValue(int number, int pairNumber)
    {
        this.lastNumber = number;
        this.pairNumber = pairNumber;
    }

    public override bool CompareValue(CardValue otherValue)
    {
        var a = (PairCardValue)otherValue;
        if(this.pairNumber != a.pairNumber)
            return true;
        else return this.lastNumber >= a.lastNumber;
    }
}