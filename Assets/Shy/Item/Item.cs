using UnityEngine;

public class Item
{
    public ItemSO itemSO { get; private set; }
    public int fixedValue = 0, tempValue = 0;
    public int count { get => Count; set => Count = Mathf.Max(0, value); }
    public int delay { get => Delay; set => Delay = Mathf.Max(0, value); }

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

    public void OnEffect()
    {
        foreach (var _effect in itemSO.itemEffects)
        {
            switch (_effect.condition)
            {
                case ItemCondition.None:
                    break;
                case ItemCondition.Use:
                    break;
                case ItemCondition.ItemCheck:
                    break;
                case ItemCondition.CategoryCheck:
                    break;
                case ItemCondition.Kill:
                    break;
            }
        }
    }
}
