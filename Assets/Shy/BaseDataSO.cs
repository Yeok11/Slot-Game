using UnityEngine;

public class BaseDataSO : ScriptableObject
{
    [field: SerializeField] public string dataName { get; private set; }
    [field: SerializeField] public Sprite dataSprite { get; private set; }
    [field: SerializeField, TextArea()] public string dataExplain { get; private set; }
}