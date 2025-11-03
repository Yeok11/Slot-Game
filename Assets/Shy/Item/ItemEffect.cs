using UnityEngine;

public enum ItemCondition
{
    None, Use, ItemCheck, CategoryCheck, Kill
}

public enum SlotRange
{
    None, Row,Column, Cross, Xmark, Round, All
}

public enum ValueCategory
{
    FixedValue, TempValue, CountValue, DelayValue
}

public enum RemoveWay
{
    None, SelfRemove, CheckItemsRemove, AllRemove
}

public struct ItemEffectData
{
    public Item targetItem;
    public string valueKey;
    public ValueCategory valueCategory;
    public int value;

    public ItemEffectData(Item _target, ItemEffect _effect)
    {
        targetItem = _target;
        valueKey = _effect.targetKey;
        valueCategory = _effect.valueCategory;
        value = _effect.value;
    }

    private ValueInfo FindValueInfo()
    {
        ValueInfo[] valueInfos = targetItem.GetValueInfos();

        foreach (ValueInfo _valueInfo in valueInfos)
        {
            if (_valueInfo.key == valueKey) return _valueInfo;
        }

        return null;
    }

    public void Use()
    {
        switch (valueCategory)
        {
            case ValueCategory.FixedValue:
                ValueInfo _info = FindValueInfo();
                if (_info != null) _info.value += value;
                break;

            case ValueCategory.TempValue:
                _info = FindValueInfo();
                if (_info != null) _info.tempValue += value;
                break;

            case ValueCategory.CountValue: targetItem.count += value; break;
            case ValueCategory.DelayValue: targetItem.delay += value; break;
        }
    }
}

[System.Serializable]
public class ItemEffect
{
    public string targetKey = "";

    [Header("Condition")]
    public BaseConditionSO condition;
    public bool oneTimeCheck = false;
    public SlotRange rangeType;
    [Range(1, 4)] public int range = 1;
    public bool selfCheck = false;

    [Header("Result")]
    public int value;
    public ValueCategory valueCategory;
    public RemoveWay removeWay;

    public Item[] GetItems() => condition.GetItems(oneTimeCheck);
}