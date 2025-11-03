using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;

    private void Start()
    {
        slotMachine.SetInventory(PlayerData.Instance.playerInven);
    }
}
