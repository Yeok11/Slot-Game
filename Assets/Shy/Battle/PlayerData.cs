using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public UnitDataSO playerData;
    public Inventory playerInven { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Already GameData SingleTon exist. So Remove To {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Instance = this;
        playerInven = new();

        if (playerData != null) PlayerSetting();
    }

    [ContextMenu("Player Setting")]
    public void PlayerSetting()
    {
        for (int i = 0; i < playerData.defaultItems.Count; i++)
        {
            playerInven.AddItem(playerData.defaultItems[i]);
        }
    }
}
