using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit")]
public class UnitDataSO : ScriptableObject
{
    public string unitName;
    public List<NormalItemSO> defaultItems;
}
