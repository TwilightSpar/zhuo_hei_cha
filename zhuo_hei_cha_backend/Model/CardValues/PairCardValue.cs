public class PairCardValue : CardValue
{
    private int pairNumber;
    private int number;

    public PairCardValue(int number, int pairNumber)
    {
        this.number = number;
        this.pairNumber = pairNumber;
    }

    public override bool CompareValue(CardValue otherValue)
    {
        var a = (PairCardValue)otherValue;
        if(this.pairNumber != a.pairNumber)
            return true;
        else return this.number >= a.number;
    }
}