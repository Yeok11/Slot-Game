using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotItem : MonoBehaviour, IClickEvent
{
    private Image itemImage;
    private Item item;

    [SerializeField] private TextMeshProUGUI fixedValueTmp, tempValueTmp;
    [SerializeField] private TextMeshProUGUI cntTmp, delayTmp;

    private void Awake()
    {
        itemImage = transform.Find("Icon").GetComponent<Image>();
    }

    public void InitData(Item _data)
    {
        item = _data;
    }

    private void UisActive(bool _value)
    {
        itemImage.gameObject.SetActive(_value);
        fixedValueTmp.gameObject.SetActive(_value);
        tempValueTmp.gameObject.SetActive(_value);
        cntTmp.gameObject.SetActive(_value);
        delayTmp.gameObject.SetActive(_value);
    }

    public void ChangeVisual()
    {
        bool _isNull = item == null;

        if (_isNull)
        {
            UisActive(false);
        }
        else
        {
            UisActive(true);
            itemImage.sprite = item.itemSO.sprite;
            cntTmp.SetText(item.count == 0 ? "" : item.count.ToString());
            delayTmp.SetText(item.delay == 0 ? "" : item.delay.ToString());
            fixedValueTmp.SetText(item.fixedValue == 0 ? "" : item.fixedValue.ToString());
            tempValueTmp.SetText(item.tempValue == 0 ? "" : item.tempValue.ToString());
        }
    }

    public void OnClickDown()
    {
        Debug.Log("Click Item : " + gameObject.name);
    }

    public void OnClickUp()
    {
    }
}
