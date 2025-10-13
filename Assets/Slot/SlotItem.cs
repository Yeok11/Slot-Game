using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IClickEvent
{
    private Image itemImage;

    

    public void OnClickDown()
    {
        Debug.Log("Click Item : " + gameObject.name);
    }

    public void OnClickUp()
    {
    }
}
