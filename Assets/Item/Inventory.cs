using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory
{
    public List<ItemDataSO> items { get; private set; } = new();

    public UnityAction OnInventoryUpdate;

    public void AddItem(ItemDataSO _item)
    {
        if (_item == null) return;

        items.Add(_item);

        OnInventoryUpdate?.Invoke();
    }

    public void RemoveItem()
    {

    }
}
