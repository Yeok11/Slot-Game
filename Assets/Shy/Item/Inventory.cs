using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory
{
    public List<Item> items { get; private set; } = new();

    public UnityAction OnInventoryUpdate;

    public void AddItem(NormalItemSO _itemSO)
    {
        if (_itemSO == null) return;

        Item _item = new(_itemSO);

        items.Add(_item);

        OnInventoryUpdate?.Invoke();
    }

    public void RemoveItem(Item _item)
    {
        if (!items.Contains(_item)) return;

        items.Remove(_item);
        _item.isDelete = true;
        OnInventoryUpdate?.Invoke();
    }
}
