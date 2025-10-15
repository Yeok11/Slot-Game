using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;

    private void Start()
    {
        slotMachine.SetInventory(PlayerData.Instance.playerInven);
    }

    public void Roll()
    {
        slotMachine.Roll();
    }
}
