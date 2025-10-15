using UnityEngine;

public enum ItemCondition
{
    None,
    Use,
    ItemCheck,
    ItemCheckOneTime,
    CategoryCheck,
    CategoryCheckOneTime,
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

public enum ValueType
{
    FixedValue,
    TempValue,
    CountValue,
    DelayValue,
}

public enum TargetType
{
    Self,
    CheckItems
}

[System.Serializable]
public class ItemEffect
{
    [Header("Condition")]
    public ItemCondition condition;
    public SlotRange Range;
    public ItemCategory checkCategory;
    public ItemSO checkSO;
    public bool selfCheck = false;

    [Header("Result")]
    public int value;
    public ValueType valueType;
    public TargetType targetType;
    public bool checkedRemove = false;
}