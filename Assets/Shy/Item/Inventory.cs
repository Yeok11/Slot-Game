using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory
{
    public List<Item> items { get; private set; } = new();

    public UnityAction OnInventoryUpdate;

    public void AddItem(ItemSO _itemSO)
    {
        if (_itemSO == null) return;

        Item _item = new(_itemSO);

        items.Add(_item);

        OnInventoryUpdate?.Invoke();
    }

    public void RemoveItem()
    {

    }
}
