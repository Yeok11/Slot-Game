using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Normal Item", order = 100)]
public class NormalItemSO : BaseDataSO
{
    [Header("Effect")]
    [field: SerializeField] public ItemCategory itemCategory { get; private set; } = ItemCategory.None;
    [field: SerializeField] public ValueInfo[] itemValues { get; private set; }
    [field: SerializeField] public ItemEffect[] itemEffects { get; private set; }
}