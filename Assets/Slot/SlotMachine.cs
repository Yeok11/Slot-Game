using System.Collections;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Line[] lines;
    private int checkedCnt = 0;

    private Sprite[] sprites;
    private Inventory inventory;

    private void Start()
    {
        foreach (var _line in lines)
        {
            _line.animeFin += AnimeFinCheck;
        }
    }

    private void OnDestroy()
    {
        foreach (var _line in lines) _line.animeFin -= AnimeFinCheck;
    }

    public void SetInventory(Inventory _inven)
    {
        inventory = _inven;
    }

    public void Roll()
    {
        checkedCnt = 0;

        // Setting
        

        foreach (var _line in lines)
            _line.RollAnime(checkedCnt++ * 0.5f);
    }



    private void AnimeFinCheck()
    {
        if (--checkedCnt == 0)
        {
            Debug.Log("All Line's Anime Finish");
        }
    }
}
