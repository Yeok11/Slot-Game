using UnityEngine;

public enum ItemCategory
{
    Weapon,
    Magic,
    Shield,
    Supply
}

[CreateAssetMenu(menuName = "SO/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemCategory category;
    [TextArea()] public string explain;
    public Sprite sprite;

    public int defaultValue;
    public ItemEffect[] itemEffects;
}
