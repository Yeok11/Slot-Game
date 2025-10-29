using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Supply Item", order = 101)]
public class SupplyItemSO : NormalItemSO
{
    [field: SerializeField, Header("Supply")] public int defaultLife { get; private set; }
    [field: SerializeField] public int maxLife { get; private set; } = 99; // 100 is Infinity
}
