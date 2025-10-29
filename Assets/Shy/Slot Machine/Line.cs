using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Line : MonoBehaviour
{
    public UnityAction animeFin;
    
    private Transform itemsGroup;
    private List<SlotItem> slotItems = new();

    private void Awake()
    {
        itemsGroup = transform.Find("Item Group");
    }

    public void InitBySlotMachine(SlotItem _slotPrefab, UnityAction _animeFin)
    {
        animeFin += _animeFin;

        if (itemsGroup == null)
        {
            Debug.LogError("Can not Found Items Group");
            return;
        }

        if (itemsGroup.childCount != 0 )
        {
            int _loop = itemsGroup.childCount;
            for (int i = 0; i < _loop; i++)
            {
                Destroy(itemsGroup.GetChild(0).gameObject);
            }
        }

        slotItems.Clear();

        Vector2 _size = new Vector2(700, 700 / SlotMachine.Height);

        for (int i = 0; i < SlotMachine.Height * 2; i++)
        {
            SlotItem _item = Instantiate(_slotPrefab, itemsGroup);
            _item.rectTrm.sizeDelta = _size;
            _item.ChangeVisual();
            slotItems.Add(_item);
        }
    }

    public void ChangeSlot(int addY = 0)
    {
        for (int y = 0; y < SlotMachine.Height; y++)
        {
            slotItems[y + addY].ChangeVisual();
        }
    }

    public void SlotInit(Item[] _datas)
    {
        bool _isNull = (_datas == null);

        for (int y = 0; y < SlotMachine.Height; y++)
        {
            slotItems[y].InitData(_isNull ? null : _datas[y]);
            slotItems[y + SlotMachine.Height].InitData(_isNull ? null : _datas[y]);
        }
    }

    #region Roll Anime

    public void RollAnime(float _delay)
    {
        StartCoroutine(Anime(_delay));
    }

    private IEnumerator Anime(float _beginDelay)
    {
        yield return new WaitForSeconds(_beginDelay);

        for (int i = 0; i < SlotMachine.Height; i++)
        {
            itemsGroup.localPosition += Vector3.up * 15;
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 2; i++)
        {
            itemsGroup.localPosition -= Vector3.up * 15 * 2;
            yield return new WaitForSeconds(0.05f);
        }

        ChangeSlot();

        bool _slotChange = false;

        for (int i = 0; i < 15; i++)
        {
            while (itemsGroup.localPosition.y > -700)
            {
                yield return new WaitForSeconds(0.01f);
                itemsGroup.localPosition += Vector3.down * 75;
            }

            if (!_slotChange)
            {
                _slotChange = true;
                ChangeSlot(SlotMachine.Height);
            }

            Vector3 _localPos = itemsGroup.localPosition;
            _localPos.y = 0;
            itemsGroup.localPosition = _localPos;
        }

        animeFin?.Invoke();
    }
    #endregion
}
