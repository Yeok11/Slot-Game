using UnityEngine;

public enum ItemCategory
{
    None, Skill, Human, Building, Nature, Disease
}

public enum ItemTheme
{
    None, Food, Wood, Rock, Gold, Human
}

[System.Serializable]
public class ValueInfo
{
    [field: SerializeField] public string key { get; private set; }
    [field: SerializeField] public ResourceType resourceType { get; private set; }
    
    public int value;
    [HideInInspector] public int tempValue;
}
