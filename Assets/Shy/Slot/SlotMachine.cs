using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Line[] lines;
    private int checkedCnt = 0;

    private List<Item[]> slotResult;
    private List<Vector2Int> emptyTiles, usedTiles;

    private Inventory inventory;
    private List<Item> items, usedItems;
    private Dictionary<ItemCategory, List<Item>> usedItemDic;

    private Item[] validItems;


    private void Start()
    {
        foreach (var _line in lines) _line.animeFin += AnimeFinCheck;

        usedTiles = new();
        usedItems = new();
        emptyTiles = new();
        
        usedItemDic = new();
        int _length = Enum.GetNames(typeof(ItemCategory)).Length;
        for (int i = 0; i < _length; i++)
        {
            usedItemDic.Add((ItemCategory)i, new());
        }

        slotResult = new();
        for (int x = 0; x < 5; x++)
        {
            slotResult.Add(new Item[4]);

            for (int y = 0; y < 4; y++)
            {
                emptyTiles.Add(new(x, y));
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var _line in lines) _line.animeFin -= AnimeFinCheck;

        if (inventory != null) inventory.OnInventoryUpdate -= ItemsUpdate;
    }

    public void SetInventory(Inventory _inven)
    {
        inventory = _inven;
        inventory.OnInventoryUpdate += ItemsUpdate;
        ItemsUpdate();
    }

    private void ItemsUpdate()
    {
        items = inventory.items.ToList();
        usedItemDic.Clear();
    }

    public void Roll()
    {
        checkedCnt = 0;

        SetResult();

        for (int x = 0; x < lines.Length; x++)
        {
            lines[x].SlotInit(slotResult[x]);
            lines[x].RollAnime(checkedCnt++ * 0.3f);
        }
    }

    private void SetResult()
    {
        // Check Tiles
        while (usedTiles.Count != 0)
        {
            Vector2Int _pos = usedTiles[0];
            lines[_pos.x].SlotInit(null);

            emptyTiles.Add(_pos);
            usedTiles.RemoveAt(0);
        }

        // Check Items
        while (usedItemDic.Count != 0)
        {
            items.Add(usedItems[0]);
            usedItems.RemoveAt(0);
        }

        for (int i = 0; i < usedItemDic.Count; i++)
        {
            if (usedItemDic[(ItemCategory)i].Count != 0) usedItemDic[(ItemCategory)i].Clear();
        }

        for (int i = 0; i < slotResult.Count; i++)
        {
            Array.Clear(slotResult[i], 0, slotResult[i].Length);
        }

        // Add
        while (items.Count != 0 && emptyTiles.Count != 0)
        {
            int _itemNum = Random.Range(0, items.Count), _tileNum = Random.Range(0, emptyTiles.Count);

            Vector2Int _pos = emptyTiles[_tileNum];
            var _item = items[_itemNum];

            slotResult[_pos.x][_pos.y] = _item;
            usedItemDic[_item.itemSO.category].Add(_item);
            usedItems.Add(_item);
            usedTiles.Add(_pos);

            items.RemoveAt(_itemNum);
            emptyTiles.RemoveAt(_tileNum);
        }
    }

    private void AnimeFinCheck()
    {
        if (--checkedCnt != 0) return;

        Debug.Log("All Lines Anime Finish");
        ExecuteItems();
    }

    private void AddValueItem(Item _item)
    {

    }

    private void ExecuteItems()
    {
        // OnEffect
        for (int i = 0; i < usedItems.Count; i++)
        {
            var _item = usedItems[i];
            var _effects = _item.GetEffects();

            foreach (var _effect in _effects)
            {
                switch (_effect.condition)
                {
                    case ItemCondition.None:
                        AddValueItem(_item);
                        break;

                    case ItemCondition.Use:
                        if (_item.delay == 0)
                        {
                            AddValueItem(_item);
                        }
                        break;

                    case ItemCondition.ItemCheck:
                    case ItemCondition.CategoryCheck:
                    case ItemCondition.ItemCheckOneTime:
                    case ItemCondition.CategoryCheckOneTime:
                        SlotChecker.GetSlotsToRange(slotResult, _effect.Range, usedTiles[i], _effect.selfCheck);

                        if (_effect.condition == ItemCondition.ItemCheck || _effect.condition == ItemCondition.ItemCheckOneTime)
                        {
                            validItems = SlotChecker.GetSOCheck(_effect.checkSO, _effect.condition == ItemCondition.ItemCheckOneTime);
                        }
                        else
                        {
                            validItems = SlotChecker.GetCategoryCheck(_effect.checkCategory, _effect.condition == ItemCondition.CategoryCheckOneTime);
                        }

                        if (validItems.Length == 0) continue;


                        break;
                }
            }

            usedItems[i].OnEffect();
        }

        // GetValues
        for (int i = 0; i < usedItemDic.Count; i++)
        {
        }
    }
}