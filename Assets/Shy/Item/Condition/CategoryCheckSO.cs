using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/CategoryCheck", order = 1002)]
public class CategoryCheckSO : BaseConditionSO
{
    [field: SerializeField] public ItemCategory checkCategory { get; private set; }

    public override Item[] GetItems(bool _oneTimeCheck) => ConditionChecker.GetCategoryCheck(checkCategory, _oneTimeCheck);
}