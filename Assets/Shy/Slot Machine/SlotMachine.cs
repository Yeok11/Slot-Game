using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    public const int Width = 5, Height = 5;

    [SerializeField] private Line[] lines;
    [SerializeField] private SlotItem slotItemPrefab;
    private int checkedCnt = 0;

    private Inventory inventory;
    private bool inventoryUpdate;

    private readonly List<Item[]> slotResult = new();
    private List<Vector2Int> emptyTiles = new(), usedTiles = new();
    private List<Item> items = new(), usedItems = new();
    private Dictionary<ResourceType, List<Vector2Int>> usedItemDic = new();

    private void Start()
    {
        foreach (var _line in lines)
        {
            _line.SettingSlotMachine(slotItemPrefab);
            _line.AnimeFin += SlotAnimeFinCheck;
        }

        foreach (ResourceType _category in Enum.GetValues(typeof(ResourceType)))
        {
            usedItemDic.Add(_category, new());
        }

        for (int x = 0; x < Width; x++)
        {
            slotResult.Add(new Item[Height]);
            for (int y = 0; y < Height; y++)
            {
                emptyTiles.Add(new(x, y));
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var _line in lines)
        {
            _line.AnimeFin -= SlotAnimeFinCheck;
        }

        if (inventory != null) inventory.OnInventoryUpdate -= InventoryUpdate;
    }

    #region Inventory

    private void InventoryUpdate() => inventoryUpdate = true;

    public void SetInventory(Inventory _inven)
    {
        inventory = _inven;
        inventory.OnInventoryUpdate += InventoryUpdate;
        inventoryUpdate = true;
    }

    #endregion

    // 외부 Button에서 동작
    public void Roll()
    {
        checkedCnt = lines.Length;
        SettingSlots();

        for (int x = 0; x < lines.Length; x++)
        {
            lines[x].InitSlots(slotResult[x]);
            lines[x].RollAnime(0.3f * x);
        }
    }

    private void SettingSlots()
    {
        if (inventoryUpdate) InitInventory();
        InitTilesAndItems();
        InitDictionaryAndArray();
        SettingItemsInSlot();
    }

    #region Setting Slots' Method

    private void InitInventory()
    {
        items = inventory.items.ToList();
        usedItems.Clear();
        inventoryUpdate = false;
    }

    private void InitTilesAndItems()
    {
        // Init Tiles
        emptyTiles.AddRange(usedTiles);
        usedTiles.Clear();

        foreach (Vector2Int _pos in emptyTiles)
        {
            lines[_pos.x].InitSlots(null);
        }

        // Init Items
        usedItems.AddRange(items);
        items.Clear();
    }

    private void InitDictionaryAndArray()
    {
        for (int i = 0; i < usedItemDic.Count; i++)
        {
            if (usedItemDic[(ResourceType)i].Count != 0) usedItemDic[(ResourceType)i].Clear();
        }

        for (int i = 0; i < slotResult.Count; i++)
        {
            Array.Clear(slotResult[i], 0, slotResult[i].Length);
        }
    }

    private void SettingItemsInSlot()
    {
        while (items.Count != 0 && emptyTiles.Count != 0)
        {
            int _itemIdx = Random.Range(0, items.Count), _tileIdx = Random.Range(0, emptyTiles.Count);
            Vector2Int _pos = emptyTiles[_tileIdx];
            Item _item = items[_itemIdx];

            slotResult[_pos.x][_pos.y] = _item;
            usedItems.Add(_item);
            usedTiles.Add(_pos);

            items.RemoveAt(_itemIdx);
            emptyTiles.RemoveAt(_tileIdx);
        }
    }

    #endregion

    private void SlotAnimeFinCheck()
    {
        if (--checkedCnt != 0) return;

        Debug.Log("All Lines Anime Finish");
        SequnceTool.Instance.Delay(ExcuteItems, 1.5f);
    }

    private void ExcuteItems()
    {
        int _useItemCnt = usedItems.Count;

        ApplyItemEffects(_useItemCnt);

        // Visual Update
        foreach (Line _line in lines)
        {
            _line.UpdateSlotVisual(Height);
        }

        DivideToResourceDic(_useItemCnt);

        foreach (var item in usedItemDic)
        {

        }

        Debug.Log("Total End");
    }

    #region Item Effect

    private void ApplyItemEffects(int _useItemCnt)
    {
        // Subscribe Effect
        for (int i = 0; i < _useItemCnt; i++)
        {
            Item _item = usedItems[i];
            ItemEffect[] _effects = _item.GetEffects();

            foreach (ItemEffect _effect in _effects)
            {
                switch (_effect.condition.itemCondition)
                {
                    case ItemCondition.None:
                        SubscribeItemEffect(_item, _effect, _item);
                        break;

                    case ItemCondition.Use:
                        if (_item.delay == 0) SubscribeItemEffect(_item, _effect, _item);
                        break;

                    case ItemCondition.ItemCheck:
                    case ItemCondition.CategoryCheck:
                        CheckItemCondition(usedTiles[i], _item, _effect);
                        break;
                }
            }
        }

        // Apply Effect
        foreach (Item _item in usedItems)
        {
            _item.UseAddValueDatas();
            if (_item.LifeZeroCheck()) inventory.RemoveItem(_item);
        }
    }

    private void CheckItemCondition(Vector2Int _tilePos, Item _item, ItemEffect _effect)
    {
        ConditionChecker.SetSlotsInRange(slotResult, _effect.rangeType, _effect.range, _tilePos, _effect.selfCheck);
        
        Item[] _validItems = _effect.GetItems();
        int _validLength = _validItems.Length;

        for (int i = 0; i < _validLength; i++)
        {
            switch (_effect.removeWay)
            {
                case RemoveWay.None:
                    SubscribeItemEffect(_item, _effect, _item);
                    break;

                case RemoveWay.SelfRemove:
                    SubscribeItemEffect(_validItems[i], _effect, _validItems[i]);
                    inventory.RemoveItem(_item);
                    break;

                case RemoveWay.CheckItemsRemove:
                    SubscribeItemEffect(_item, _effect, _item);
                    inventory.RemoveItem(_validItems[i]);
                    break;

                case RemoveWay.AllRemove:
                    inventory.RemoveItem(_item);
                    inventory.RemoveItem(_validItems[i]);
                    break;
            }
        }
    }

    private void SubscribeItemEffect(Item _castItem, ItemEffect _effect, Item _targetItem)
    {
        _castItem.effectDatas.Add(new ItemEffectData(_targetItem, _effect));
    }

    #endregion

    private void DivideToResourceDic(int _useItemCnt)
    {
        for (int i = 0; i < _useItemCnt; i++)
        {
            if (usedItems[i].delay != 0 || usedItems[i].isDelete) continue;

            ValueInfo[] _values = usedItems[i].GetValueInfos();

            foreach (ValueInfo _valueInfo in _values)
            {
                if (_valueInfo.value + _valueInfo.tempValue == 0) break;
                usedItemDic[_valueInfo.resourceType].Add(usedTiles[i]);
            }
        }
    }
}