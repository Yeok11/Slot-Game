using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotItem : MonoBehaviour, IClickEvent
{
    private Image itemImage;
    private Item item;

    [HideInInspector] public RectTransform rectTrm;

    [SerializeField] private TextMeshProUGUI fixedValueTmp, tempValueTmp;
    [SerializeField] private TextMeshProUGUI cntTmp, delayTmp;

    private void Awake()
    {
        itemImage = transform.Find("Icon").GetComponent<Image>();
        rectTrm = GetComponent<RectTransform>();
    }

    public void InitData(Item _item) => item = _item;

    private void TextUpdate(TextMeshProUGUI _tmp, int _value) => _tmp.SetText(_value == 0 ? "" : _value.ToString());

    public void ChangeVisual()
    {
        bool _isNull = (item == null || item.isDelete);

        itemImage.gameObject.SetActive(!_isNull);

        if (_isNull)
        {
            cntTmp.SetText("");
            delayTmp.SetText("");
            fixedValueTmp.SetText("");
            tempValueTmp.SetText("");
            return;
        }

        itemImage.sprite = item.itemSO.dataSprite;

        TextUpdate(cntTmp, item.count);
        TextUpdate(delayTmp, item.delay);
        TextUpdate(fixedValueTmp, item.fixedValue);
        TextUpdate(tempValueTmp, item.tempValue);
    }

    public void OnClickDown()
    {
        Debug.Log("Click Item : " + gameObject.name);
    }

    public void OnClickUp()
    {
    }
}
