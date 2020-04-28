public class FlushCardValue : CardValue
{
    private int firstNumber;
    private int flushSize;

    public FlushCardValue(int v1, int v2)
    {
        this.firstNumber = v1;
        this.flushSize = v2;
    }

    public override bool CompareValue(CardValue otherValue)
    {
        var a = (FlushCardValue)otherValue;
        if(this.flushSize != a.flushSize)
            return true;
        else return this.firstNumber >= a.firstNumber;
    }
}