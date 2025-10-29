using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/TypeCheck", order = 1002)]
public class TypeCheckCSO : BaseConditionSO
{
    [field: SerializeField] public ItemType checkType { get; private set; }

    public override Item[] GetItems(bool _oneTimeCheck) => SlotChecker.GetTypeCheck(checkType, _oneTimeCheck);
}