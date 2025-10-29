using UnityEngine;

public class TypeStackUI : MonoBehaviour, IClickEvent
{
    [SerializeField] private ItemType stackType;
    public int value { get; private set; }
    
    public void AddValue()
    {

    }

    public void OnClickDown()
    {
    }

    public void OnClickUp()
    {
    }
}
