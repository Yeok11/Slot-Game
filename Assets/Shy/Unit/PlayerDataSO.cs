using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit")]
public class PlayerDataSO : ScriptableObject
{
    public string unitName;
    public List<NormalItemSO> defaultItems;
}
