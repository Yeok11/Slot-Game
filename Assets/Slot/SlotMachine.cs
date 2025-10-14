using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Line[] lines;
    private int checkedCnt = 0;

    private List<ItemDataSO[]> slotResult;

    private List<Vector2Int> emptyTiles, usedTiles;

    private Inventory inventory;
    private List<ItemDataSO> items, selectedItems;

    private void Start()
    {
        foreach (var _line in lines) _line.animeFin += AnimeFinCheck;

        usedTiles = new();
        emptyTiles = new();
        selectedItems = new();
        slotResult = new();

        for (int x = 0; x < 5; x++)
        {
            slotResult.Add(new ItemDataSO[4]);

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
        selectedItems.Clear();
    }

    public void Roll()
    {
        checkedCnt = 0;

        SetResult();

        for (int x = 0; x < lines.Length; x++)
        {
            lines[x].SlotInit(slotResult[x]);
            lines[x].RollAnime(checkedCnt++ * 0.5f);
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
        while (selectedItems.Count != 0)
        {
            items.Add(selectedItems[0]);
            selectedItems.RemoveAt(0);
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
            slotResult[_pos.x][_pos.y] = items[_itemNum];

            selectedItems.Add(items[_itemNum]);
            usedTiles.Add(_pos);

            items.RemoveAt(_itemNum);
            emptyTiles.RemoveAt(_tileNum);
        }
    }



    private void AnimeFinCheck()
    {
        if (--checkedCnt == 0)
        {
            Debug.Log("All Lines Anime Finish");
        }
    }
}
