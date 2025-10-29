using UnityEngine;

public enum ValueWay
{
    Attack = 0,
    Hp,
    Def,
}

[System.Serializable]
public class ItemValue
{
    // key is used in the explain
    [field: SerializeField] public string key { get; private set; }
    [field: SerializeField] public int value { get; private set; }
    [field: SerializeField] public ValueWay valueTarget { get; private set; }
    
}
