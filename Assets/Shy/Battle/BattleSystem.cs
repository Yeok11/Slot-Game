using System.Collections;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    private ActionValues userValues;

    private void Start()
    {
        slotMachine.SetInventory(PlayerData.Instance.playerInven);
        slotMachine.OnUseItems += SetUserValues;
    }

    public void Roll()
    {
        slotMachine.Roll();
    }

    private void SetUserValues(ActionValues _values)
    {
        userValues = _values;
        Debug.Log(userValues.physicalValue + " / " + userValues.magicalValue + " / " + userValues.shieldValue);
    }
}
