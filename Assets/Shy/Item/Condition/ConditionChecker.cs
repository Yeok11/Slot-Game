using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ConditionChecker
{
    private static List<Item> result = new();

    #region Condition Check

    public static Item[] GetItemSOCheck(NormalItemSO _so, bool _oneTimeCheck)
    {
        result = result.Where(item => item.itemSO == _so).ToList();

        if (result.Count > 1 && _oneTimeCheck) result.RemoveRange(1, result.Count);

        return result.ToArray();
    }

    public static Item[] GetCategoryCheck(ItemCategory _itemType, bool _oneTimeCheck)
    {
        result = result.Where(item => item.itemSO.itemCategory == _itemType).ToList();

        if (result.Count > 1 && _oneTimeCheck) result.RemoveRange(1, result.Count);

        return result.ToArray();
    }

    #endregion

    public static void SetSlotsInRange(List<Item[]> _slot, SlotRange _rangeType, int _range, Vector2Int _pos, bool _includeSelf)
    {
        result.Clear();

        switch (_rangeType)
        {
            case SlotRange.Row:
                Row(_slot, _pos, _range);
                break;

            case SlotRange.Column:
                Column(_slot, _pos, _range);
                break;

            case SlotRange.Cross:
                Row(_slot, _pos, _range);
                Column(_slot, _pos, _range);

                result.Remove(_slot[_pos.x][_pos.y]);
                break;

            case SlotRange.Xmark:
                Xmark(_slot, _pos, _range);
                break;

            case SlotRange.Round:
                Row(_slot, _pos, _range);
                Column(_slot, _pos, _range);
                Xmark(_slot, _pos, _range);

                result.Remove(_slot[_pos.x][_pos.y]);
                result.Remove(_slot[_pos.x][_pos.y]);
                break;

            case SlotRange.All:
                for (int x = 0; x < SlotMachine.Width; x++)
                {
                    for (int y = 0; y < SlotMachine.Height; y++)
                    {
                        CheckSlot(_slot[x][y]);
                    }
                }
                break;
        }

        if (_includeSelf == false) result.Remove(_slot[_pos.x][_pos.y]);
    }

    private static void CheckSlot(Item _item)
    {
        if (_item != null) result.Add(_item);
    }

    private static void Row(List<Item[]> _slot, Vector2Int _pos, int _range)
    {
        int _beginX = Mathf.Max(0, _pos.x - _range), _endX = Mathf.Min(_pos.x + _range, SlotMachine.Width - 1);

        for (int x = _beginX; x <= _endX; x++)
        {
            CheckSlot(_slot[x][_pos.y]);
        }
    }

    private static void Column(List<Item[]> _slot, Vector2Int _pos, int _range)
    {
        int _beginY = Mathf.Max(0, _pos.y - _range), _endY = Mathf.Min(_pos.y + _range, SlotMachine.Height - 1);

        for (int y = _beginY; y <= _endY; y++)
        {
            CheckSlot(_slot[_pos.x][y]);
        }
    }

    private static void Xmark(List<Item[]> _slot, Vector2Int _beginPos, int _range)
    {
        int minX = _beginPos.x, maxX = _beginPos.x, 
            minY = _beginPos.y, maxY = _beginPos.y;

        for (int n = 0; n < _range; n++)
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