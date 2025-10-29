using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SlotChecker
{
    private static List<Item> result = new();

    public static Item[] GetItemSOCheck(NormalItemSO _so, bool _oneTimeCheck)
    {
        result = result.Where(item => item.itemSO == _so).ToList();

        if (result.Count > 1 && _oneTimeCheck) result.RemoveRange(1, result.Count);

        return result.ToArray();
    }

    public static Item[] GetTypeCheck(ItemType _itemType, bool _oneTimeCheck)
    {
        result = result.Where(item => item.itemSO.itemType == _itemType).ToList();

        if (result.Count > 1 && _oneTimeCheck) result.RemoveRange(1, result.Count - 1);

        return result.ToArray();
    }

    public static void SetSlotsInRange(List<Item[]> _slot, SlotRange _range, Vector2Int _pos, bool _includeSelf)
    {
        result.Clear();

        switch (_range)
        {
            case SlotRange.Row:
                Row(_slot, 0, SlotMachine.Width, _pos.y);
                break;

            case SlotRange.RowOne:
                Row(_slot, _pos.x - 1, _pos.x + 1, _pos.y);
                break;

            case SlotRange.Column:
                Column(_slot, 0, SlotMachine.Height, _pos.x);
                break;

            case SlotRange.ColumnOne:
                Column(_slot, _pos.y - 1, _pos.y + 1, _pos.x);
                break;

            case SlotRange.Cross:
                Row(_slot, 0, SlotMachine.Width, _pos.y);
                Column(_slot, 0, SlotMachine.Width, _pos.x);
                break;

            case SlotRange.CrossOne:
                Row(_slot, _pos.x - 1, _pos.x + 1, _pos.y);
                Column(_slot, _pos.y - 1, _pos.y + 1, _pos.x);
                result.Remove(_slot[_pos.x][_pos.y]);
                break;

            case SlotRange.Xmark:
                Xmark(_slot, _pos, 3);
                break;

            case SlotRange.XmarkOne:
                Xmark(_slot, _pos, 1);
                break;

            case SlotRange.Round:
                Row(_slot, _pos.x - 1, _pos.x + 1, _pos.y);
                Column(_slot, _pos.y - 1, _pos.y + 1, _pos.x);
                result.Remove(_slot[_pos.x][_pos.y]);
                break;

            case SlotRange.All:
                for (int x = 0; x < _slot.Count; x++)
                {
                    int _loop = _slot[x].Length;

                    for (int y = 0; y < _loop; y++)
                        CheckSlot(_slot[x][y]);
                }
                break;
        }

        if (_includeSelf == false) result.Remove(_slot[_pos.x][_pos.y]);
    }

    private static void CheckSlot(Item _item)
    {
        if (_item != null) result.Add(_item);
    }

    private static void Row(List<Item[]> _slot, int _beginX, int _endX, int _y)
    {
        for (int x = _beginX; x <= _endX; x++)
        {
            if (x < 0 || x >= SlotMachine.Width) continue;
            CheckSlot(_slot[x][_y]);
        }
    }

    private static void Column(List<Item[]> _slot, int _beginY, int _endY, int _x)
    {
        for (int y = _beginY; y <= _endY; y++)
        {
            if (y < 0 || y >= SlotMachine.Height) continue;
            CheckSlot(_slot[_x][y]);
        }
    }

    private static void Xmark(List<Item[]> _slot, Vector2Int _beginPos, int _cnt)
    {
        int minX = _beginPos.x, maxX = _beginPos.x, 
            minY = _beginPos.y, maxY = _beginPos.y;

        for (int n = 0; n < _cnt; n++)
        {
            minX--;
            minY--;
            maxX++;
            maxY++;

            if (minX >= 0 && minY >= 0) CheckSlot(_slot[minX][minY]);
            if (maxX < SlotMachine.Width && minY >= 0) CheckSlot(_slot[maxX][minY]);
            if (minX >= 0 && maxY < SlotMachine.Height) CheckSlot(_slot[minX][maxY]);
            if (maxX < SlotMachine.Width && maxY < SlotMachine.Height) CheckSlot(_slot[maxX][maxY]);
        }
    }
}