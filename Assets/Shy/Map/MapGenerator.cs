using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int maxSize = new(4, 15);

    private Node[,] arr;

    public void MakeMap()
    {
        arr = new Node[maxSize.y, maxSize.x];

        int x = Random.Range(0, maxSize.x);

        int xMin = Mathf.Max(x - 1, 0), xMax = Mathf.Min(x + 1, maxSize.x - 1);
        int rand = Random.Range(-1, 2);


    }
}
