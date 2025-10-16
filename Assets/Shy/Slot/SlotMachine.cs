using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    public const int Width = 5, Height = 4;

    [SerializeField] private Line[] lines;
    private int checkedCnt = 0;

    private List<Item[]> slotResult = new();
    private List<Vector2Int> emptyTiles = new(), usedTiles = new();

    private Inventory inventory;
    private List<Item> items, usedItems = new();
    private Dictionary<ItemCategory, List<Item>> usedItemDic = new();

    private bool inventoryUpdate;

    public UnityAction<ActionValues> OnUseItems;

    private void Start()
    {
        foreach (var _line in lines) _line.animeFin += AnimeFinCheck;
        
        int _categoryLength = Enum.GetNames(typeof(ItemCategory)).Length;
        for (int i = 0; i < _categoryLength; i++) usedItemDic.Add((ItemCategory)i, new());

        for (int x = 0; x < Width; x++)
        {
            slotResult.Add(new Item[4]);
            for (int y = 0; y < Height; y++) emptyTiles.Add(new(x, y));
        }
    }

    private void OnDestroy()
    {
        foreach (var _line in lines) _line.animeFin -= AnimeFinCheck;
        if (inventory != null) inventory.OnInventoryUpdate -= InventoryUpdate;
    }

    public void SetInventory(Inventory _inven)
    {
        inventory = _inven;
        inventory.OnInventoryUpdate += InventoryUpdate;
        inventoryUpdate = true;
    }

    private void InventoryUpdate() => inventoryUpdate = true;

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
        if (inventoryUpdate)
        {
            items = inventory.items.ToList();
            usedItems.Clear();
            inventoryUpdate = false;
        }

        // Check Tiles
        while (usedTiles.Count != 0)
        {
            Vector2Int _pos = usedTiles[0];
            lines[_pos.x].SlotInit(null);

            emptyTiles.Add(_pos);
            usedTiles.RemoveAt(0);
        }

        // Check Items
        while (usedItems.Count != 0)
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

        SequnceTool.Instance.Delay(ExcuteItems, 0.8f);
    }

    #region Excute Item

    private void ExcuteItems()
    {
        int _useItemCnt = usedItems.Count;

        // OnEffect
        for (int i = 0; i < _useItemCnt; i++)
        {
            var _item = usedItems[i];
            var _effects = _item.GetEffects();

            foreach (var _effect in _effects)
            {
                switch (_effect.condition)
                {
                    case ItemCondition.None:
                        AddValueItem(_item, _effect, _item);
                        break;

                    case ItemCondition.Use:
                        if (_item.delay == 0)
                        {
                            AddValueItem(_item, _effect, _item);
                        }
                        break;

                    case ItemCondition.ItemCheck:
                    case ItemCondition.CategoryCheck:
                    case ItemCondition.ItemCheckOneTime:
                    case ItemCondition.CategoryCheckOneTime:
                        CheckItemCondition(usedTiles[i], _item, _effect);
                        break;
                }
            }
        }

        foreach (var item in usedItems)
        {
            item.UseAddValueDatas();
        }

        ActionValues _actionValues = new();

        foreach (var _item in usedItems)
        {
            _actionValues.AddValue(_item.itemSO.actionType, _item.GetValue());
        }

        OnUseItems?.Invoke(_actionValues);
    }

    private void CheckItemCondition(Vector2Int _tilePos, Item _item, ItemEffect _effect)
    {
        SlotChecker.GetSlotsToRange(slotResult, _effect.Range, _tilePos, _effect.selfCheck);
        bool _targetRemove = _effect.RemoveCheckItem;
        Item[] _validItems;

        if (_effect.condition == ItemCondition.ItemCheck || _effect.condition == ItemCondition.ItemCheckOneTime)
        {
            _validItems = SlotChecker.GetSOCheck(_effect.checkSO, _effect.condition == ItemCondition.ItemCheckOneTime);
        }
        else
        {
            _validItems = SlotChecker.GetCategoryCheck(_effect.checkCategory, _effect.condition == ItemCondition.CategoryCheckOneTime);
        }

        int _validLength = _validItems.Length;

        switch (_effect.targetType)
        {
            case TargetType.Self:
                for (int i = 0; i < _validLength; i++)
                {
                    AddValueItem(_item, _effect, _item);
                    if (_targetRemove) inventory.RemoveItem(_validItems[i]);
                }
                break;

            case TargetType.CheckItems:
                for (int i = 0; i < _validLength; i++)
                {
                    AddValueItem(_item, _effect, _validItems[i]);
                    if (_targetRemove) inventory.RemoveItem(_validItems[i]);
                }
                break;
        }
    }

    private void AddValueItem(Item _castItem, ItemEffect _effectData, Item _targetItem)
    {
        _castItem.effectDatas.Add(new(_targetItem, _effectData.valueType, _effectData.value));
    }

    #endregion
}