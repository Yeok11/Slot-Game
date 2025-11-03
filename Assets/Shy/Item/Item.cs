using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item
{
    public NormalItemSO itemSO { get; private set; }

    // Value
    private ValueInfo[] valueInfos;
 
    public int delay { get => Delay; set => Delay = Mathf.Max(0, value); }
    public int count { get => Count; set => Count = Mathf.Max(0, Mathf.Min(value, MaxCount));}
    private int Delay, Count, MaxCount = 99;
    
    // State
    public bool isDelete = false;
    private bool isSupplyItem = false;

    // Method variable
    public List<ItemEffectData> effectDatas = new();
    private ResourceType[] resourceTypes;

    public Item(NormalItemSO _so)
    {
        itemSO = _so;
        valueInfos = _so.itemValues.ToArray();

        if (_so is SupplyItemSO _supplyItem)
        {
            isSupplyItem = true;
            MaxCount = _supplyItem.maxLife;
            count = _supplyItem.defaultLife;
        }
    }

    public bool LifeZeroCheck() => isSupplyItem && count == 0;

    public ItemEffect[] GetEffects() => itemSO.itemEffects;

    public ValueInfo[] GetValueInfos() => itemSO.itemValues;

    public void Init()
    {
        int _loop = valueInfos.Length;
        for (int i = 0; i < _loop; i++)
        {
            valueInfos[i].tempValue = 0;
        }
    }

    public void UseAddValueDatas()
    {
        if (isDelete) return;

        foreach (ItemEffectData _data in effectDatas)
        {
            _data.Use();
        }

        effectDatas.Clear();
    }

    public bool EffectResourceCheck(ResourceType food)
    {
        foreach (ValueInfo _valueInfo in itemSO.itemValues)
        {
            if (_valueInfo.resourceType == food) return true;
        }
        return false;
    }
}