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

    public void ChangeSlot(int addY = 0)
    {
        for (int y = 0; y < 4; y++)
        {
            slotItems[y + addY].ChangeVisual();
        }
    }

    public void SlotInit(Item[] _datas)
    {
        bool _isNull = (_datas == null);

        for (int y = 0; y < 4; y++)
        {
            slotItems[y].InitData(_isNull ? null : _datas[y]);
            slotItems[y + 4].InitData(_isNull ? null : _datas[y]);
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

        for (int i = 0; i < 4; i++)
        {
            items.localPosition += Vector3.up * 15;
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 2; i++)
        {
            items.localPosition -= Vector3.up * 15 * 2;
            yield return new WaitForSeconds(0.05f);
        }

        ChangeSlot();

        bool _slotChange = false;

        for (int i = 0; i < 15; i++)
        {
            while (items.localPosition.y > -700)
            {
                yield return new WaitForSeconds(0.01f);
                items.localPosition += Vector3.down * 75;
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
    #endregion
}
