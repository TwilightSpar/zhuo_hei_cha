public class HongCardValue : CardValue
{
    private readonly int _value;
    private readonly int _length;

    public HongCardValue(int value, int length)
    {
        _value = value;
        _length = length;
    }

    public override bool CompareValue(CardValue otherValue)
    {
        var valueToBeCompared = (HongCardValue)otherValue;

        if (this._length != valueToBeCompared._length)
        {
            return this._length > valueToBeCompared._length;
        }
        else 
        {
            return this._value >= valueToBeCompared._value;
        }
    }
}