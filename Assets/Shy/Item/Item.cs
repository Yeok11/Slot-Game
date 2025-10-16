using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct ItemEffectData
{
    public Item target;
    public ValueType valueType;
    public int value;

    public ItemEffectData(Item _target, ValueType _type, int _value)
    {
        target = _target;
        valueType = _type;
        value = _value;
    }

    public void Use()
    {
        switch (valueType)
        {
            case ValueType.FixedValue: target.fixedValue += value; break;
            case ValueType.TempValue: target.tempValue += value; break;
            case ValueType.CountValue: target.count += value; break;
            case ValueType.DelayValue: target.delay += value; break;
        }
    }
}

public class Item
{
    public ItemSO itemSO { get; private set; }
    public int fixedValue = 0, tempValue = 0;
    public int count { get => Count; set => Count = Mathf.Max(0, value); }
    public int delay { get => Delay; set => Delay = Mathf.Max(0, value); }
    
    [HideInInspector] public bool isDelete = false;

    public List<ItemEffectData> effectDatas = new();
    private int Count, Delay;

    public Item(ItemSO _so)
    {
        itemSO = _so;

        if (_so.category == ItemCategory.Supply)
        {
            count = _so.defaultValue;
        }
    }

    public ItemEffect[] GetEffects() => itemSO.itemEffects;

    public void UseAddValueDatas()
    {
        if (isDelete) return;

        foreach (var _data in effectDatas) _data.Use();

        effectDatas.Clear();
    }

    public bool LifeZeroCheck() => itemSO.lifeExist && count == 0; 

    public int GetValue() => (isDelete ? 0 : itemSO.defaultValue + fixedValue + tempValue);
}
