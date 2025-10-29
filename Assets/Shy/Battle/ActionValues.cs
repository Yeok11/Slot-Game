using UnityEngine;

public enum ItemType
{
    None,
    Fire,
    Water,
    Ground,
    Wind,
    Light,
    Dark,
}

public class ActionValues
{
    public int fireValue, waterValue, groundValue, windValue, lightValue, darkValue;

    public void Init()
    {
        fireValue = 0;
        waterValue = 0;
        groundValue = 0;
        windValue = 0;
        lightValue = 0;
        darkValue = 0;
    }

    public void AddValue(ItemType _actionType, int _value)
    {
        switch (_actionType)
        {
            case ItemType.Fire: fireValue += _value; break;
            case ItemType.Water: waterValue += _value; break;
            case ItemType.Ground: groundValue += _value; break;
            case ItemType.Wind: windValue += _value; break;
            case ItemType.Light: lightValue += _value; break;
            case ItemType.Dark: darkValue += _value; break;
        }
    }

    public int GetValue(ItemType _type)
    {
        switch (_type)
        {
            case ItemType.Fire:   return fireValue;
            case ItemType.Water:  return waterValue;
            case ItemType.Ground: return groundValue;
            case ItemType.Wind:   return windValue;
            case ItemType.Light:  return lightValue;
            case ItemType.Dark:   return darkValue;
        }

        Debug.LogError($"Can't found {{ItemType:{_type}}} in ActionValues' switch.");

        return 0;
    }
}