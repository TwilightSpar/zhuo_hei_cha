public class BombCardValue : CardValue
{
    private int _value;
    private int _length;

    public BombCardValue(int value, int length)
    {
        _value = value;
        _length = length;
    }

    public override bool CompareValue(CardValue otherValue)
    {
        var valueToBeCompared = (BombCardValue)otherValue;

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