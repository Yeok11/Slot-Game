public enum ActionType
{
    None,
    PhysicalAttack,
    MagicalAttack,
    Shield
}

public struct ActionValues
{
    public int physicalValue, magicalValue, shieldValue;

    public void AddValue(ActionType _actionType, int _value)
    {
        switch (_actionType)
        {
            case ActionType.PhysicalAttack:
                physicalValue += _value;
                break;
            case ActionType.MagicalAttack:
                magicalValue += _value;
                break;
            case ActionType.Shield:
                shieldValue += _value;
                break;
        }
    }
}