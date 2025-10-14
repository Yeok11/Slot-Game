using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Line : MonoBehaviour
{
    public UnityAction animeFin;
    private Transform items;
    private SlotItem[] slotItems;

    private void Awake()
    {
        items = transform.Find("Item Group");

        if (items == null)
        {
            Debug.LogError("Can not Found Item Group");
            return;
        }

        slotItems = items.GetComponentsInChildren<SlotItem>();
    }

    private void ChangeSlot(int addY = 0)
    {
        for (int y = 0; y < 4; y++)
        {
            slotItems[y + addY].ChangeVisual();
        }
    }

    public void SlotInit(ItemDataSO[] _datas)
    {
        bool _isNull = (_datas == null);

        for (int y = 0; y < 4; y++)
        {
            slotItems[y].InitData(_isNull ? null : _datas[y]);
            slotItems[y + 4].InitData(_isNull ? null : _datas[y]);
        }
    }

    public void RollAnime(float _delay)
    {
        StartCoroutine(Anime(_delay));
    }

    private IEnumerator Anime(float _beginDelay)
    {
        yield return new WaitForSeconds(_beginDelay);

        ChangeSlot();

        bool _slotChange = false;

        for (int i = 0; i < 20; i++)
        {
            while (items.localPosition.y > -700)
            {
                yield return new WaitForSeconds(0.01f);
                items.localPosition += Vector3.down * 50;
            }

            if (!_slotChange)
            {
                _slotChange = true;
                ChangeSlot(4);
            }

            Vector3 _localPos = items.localPosition;
            _localPos.y = 0;
            items.localPosition = _localPos;
        }

        animeFin?.Invoke();
    }
}
