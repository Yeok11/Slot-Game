using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;

    private void Start()
    {
        slotMachine.SetInventory(GameData.Instance.playerInven);
    }

    private void OnDestroy()
    {
        
    }

    public void Roll()
    {
        slotMachine.Roll();
    }
}
