using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotItem : MonoBehaviour, IClickEvent
{
    private Image itemImage;
    private Item itemData;

    [HideInInspector] public RectTransform rectTrm;

    [SerializeField] private TextMeshProUGUI fixedValueTmp, tempValueTmp;
    [SerializeField] private TextMeshProUGUI cntTmp, delayTmp;

    //[SerializeField] private ItemValue itemValue;

    private void Awake()
    {
        itemImage = transform.Find("Icon").GetComponent<Image>();
        rectTrm = GetComponent<RectTransform>();
    }

    public void InitData(Item _item) => itemData = _item;

    private void TextUpdate(TextMeshProUGUI _tmp, int _value) => _tmp.SetText(_value == 0 ? "" : _value.ToString());

    public void ChangeVisual()
    {
        bool _isNull = (itemData == null || itemData.isDelete);

        itemImage.gameObject.SetActive(!_isNull);
        //itemValue.gameObject.SetActive(false);

        if (_isNull)
        {
            cntTmp.SetText("");
            delayTmp.SetText("");
            fixedValueTmp.SetText("");
            tempValueTmp.SetText("");
            return;
        }

        itemImage.sprite = itemData.itemSO.dataSprite;

        TextUpdate(cntTmp, itemData.count);
        TextUpdate(delayTmp, itemData.delay);
        //TextUpdate(fixedValueTmp, itemData.fixedValue);
        //TextUpdate(tempValueTmp, itemData.tempValue);
    }

    public void OnClickDown()
    {
        Debug.Log("Click Item : " + gameObject.name);
    }

    public void OnClickUp()
    {
    }
}
