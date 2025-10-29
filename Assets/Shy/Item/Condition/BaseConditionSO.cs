using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/BaseCondition", order = 1000)]
public class BaseConditionSO : ScriptableObject
{
    [field: SerializeField] public ItemCondition itemCondition { get; protected set; }

    public virtual Item[] GetItems(bool _oneTimeCheck) => null;
}