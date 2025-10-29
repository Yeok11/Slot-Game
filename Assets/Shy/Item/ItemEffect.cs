using UnityEngine;

public enum ItemCondition
{
    None,
    Use,
    ItemCheck,
    TypeCheck,
    Kill
}

public enum SlotRange
{
    None,
    Row, RowOne,
    Column, ColumnOne,
    Cross, CrossOne,
    Xmark, XmarkOne,
    Round, All
}

public enum ValueCategory
{
    FixedValue,
    TempValue,
    CountValue,
    DelayValue,
}

public enum TargetCategory
{
    Self,
    CheckItems
}

[System.Serializable]
public class ItemEffect
{
    [Header("Condition")]
    public BaseConditionSO condition;
    public bool oneTimeCheck = false;
    public SlotRange Range;
    public bool selfCheck = false;

    [Header("Result")]
    public int value;
    public ValueCategory valueCategory;
    public TargetCategory targetType;
    public bool RemoveCheckItem = false;

    public Item[] GetItems() => condition.GetItems(oneTimeCheck);
}