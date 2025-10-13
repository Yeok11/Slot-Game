using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Line : MonoBehaviour
{
    public UnityAction animeFin;
    private Transform items;


    private void Awake()
    {
        items = transform.Find("Item Group");
    }

    public void ChangeImages(Sprite[] _sprites)
    {

    }

    public void RollAnime(float _delay)
    {
        StartCoroutine(Anime(_delay));
    }

    private IEnumerator Anime(float _beginDelay)
    {
        yield return new WaitForSeconds(_beginDelay);

        for (int i = 0; i < 20; i++)
        {
            while (items.localPosition.y > -700)
            {
                yield return new WaitForSeconds(0.01f);
                items.localPosition += Vector3.down * 50;
            }

            Vector3 _localPos = items.localPosition;
            _localPos.y = 0;
            items.localPosition = _localPos;
        }

        animeFin?.Invoke();
    }
}
