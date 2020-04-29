public class FlushCardValue : CardValue
{
    private int lastNumber;
    private int flushSize;

    public FlushCardValue(int v1, int v2)
    {
        this.lastNumber = v1;
        this.flushSize = v2;
    }

    public override bool CompareValue(CardValue otherValue)
    {
        var a = (FlushCardValue)otherValue;
        if(this.flushSize != a.flushSize)
            return true;
        else return this.lastNumber >= a.lastNumber;
    }
}