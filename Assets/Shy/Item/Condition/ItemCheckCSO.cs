using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/ItemCheck", order = 1001)]
public class ItemCheckCSO : BaseConditionSO
{
    [field: SerializeField] public NormalItemSO checkItem { get; private set; }

    public override Item[] GetItems(bool _oneTimeCheck) => SlotChecker.GetItemSOCheck(checkItem, _oneTimeCheck);
}
