using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Normal Item", order = 100)]
public class NormalItemSO : BaseDataSO
{
    [Header("Effect")]
    [field: SerializeField] public ItemValue defaultValue { get; private set; }
    public ItemType itemType;
    [field: SerializeField] public ItemEffect[] itemEffects { get; private set; }
    public ItemValue[] addValues;
}