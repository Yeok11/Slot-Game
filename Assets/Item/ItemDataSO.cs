using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    [TextArea()] public string explain;
    public Sprite sprite;
}
