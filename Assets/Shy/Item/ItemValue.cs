using System.Collections;
using TMPro;
using UnityEngine;

public class ItemValue : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI valueTmp { get; private set; }

    [SerializeField] private float spawnRange = 0.25f;
    Vector2 des;




    public void Spawn()
    {
        float _dir = Random.Range(0f, 360f);
        _dir *= Mathf.Deg2Rad;
        des = new Vector2(Mathf.Cos(_dir), Mathf.Sin(_dir));

        StartCoroutine(SpawnAnime());
    }


    private IEnumerator SpawnAnime()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.position = (Vector2)transform.position + Vector2.Lerp(Vector2.zero, des, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        

    }
}
