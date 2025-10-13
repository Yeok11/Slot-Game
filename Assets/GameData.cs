using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public Inventory playerInven;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Already GameData SingleTon exist. Plz Remove To {gameObject.name}");
            return;
        }

        Instance = this;
        playerInven = new();
    }
}
