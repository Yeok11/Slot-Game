using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IClickEvent
{
    private Image itemImage;
    private ItemDataSO itemData;

    private void Awake()
    {
        itemImage = transform.Find("Icon").GetComponent<Image>();
    }

    public void InitData(ItemDataSO _data)
    {
        itemData = _data;
    }

    public void ChangeVisual()
    {
        bool _isNull = itemData == null;

        itemImage.sprite = _isNull ? null : itemData.sprite;
        itemImage.gameObject.SetActive(!_isNull);
    }

    public void OnClickDown()
    {
        Debug.Log("Click Item : " + gameObject.name);
    }

    public void OnClickUp()
    {
    }
}
