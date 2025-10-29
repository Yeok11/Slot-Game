using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct ItemEffectData
{
    public Item target;
    public ValueCategory valueType;
    public int value;

    public ItemEffectData(Item _target, ValueCategory _type, int _value)
    {
        target = _target;
        valueType = _type;
        value = _value;
    }

    public void Use()
    {
        switch (valueType)
        {
            case ValueCategory.FixedValue: target.fixedValue += value; break;
            case ValueCategory.TempValue: target.tempValue += value; break;
            case ValueCategory.CountValue: target.count += value; break;
            case ValueCategory.DelayValue: target.delay += value; break;
        }
    }
}

public class Item
{
    public NormalItemSO itemSO { get; private set; }
    public int fixedValue = 0, tempValue = 0;
    public int delay { get => Delay; set => Delay = Mathf.Max(0, value); }
    public int count { get => Count; 
        set
        {
            Count = Mathf.Max(0, Mathf.Min(value, MaxCount));
        }
    }
    private int Delay, Count, MaxCount = 99;
    
    public bool isDelete = false;
    private bool isSupplyItem = false;

    public List<ItemEffectData> effectDatas = new();

    public Item(NormalItemSO _so)
    {
        itemSO = _so;

        if (_so is SupplyItemSO _supplyItem)
        {
            isSupplyItem = true;
            MaxCount = _supplyItem.maxLife;
            count = _supplyItem.defaultLife;
        }
    }

    public ItemEffect[] GetEffects() => itemSO.itemEffects;

    public void UseAddValueDatas()
    {
        if (isDelete) return;

        foreach (var _data in effectDatas) _data.Use();

        effectDatas.Clear();
    }

    public bool LifeZeroCheck() => isSupplyItem && count == 0; 

    public int GetValue() => (isDelete ? 0 : itemSO.defaultValue.value + fixedValue + tempValue);
}
