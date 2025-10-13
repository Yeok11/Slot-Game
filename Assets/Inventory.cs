using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    private List<Item> itemDatas;
    public IReadOnlyList<Item> items => itemDatas;

    public UnityAction OnInventoryUpdate;

    public void AddItem(Item _item)
    {

    }

    public void RemoveItem()
    {

    }
}
