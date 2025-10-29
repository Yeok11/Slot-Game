using System;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    private ActionValues userValues;

    private void Awake()
    {
        userValues = new();
        userValues.Init();
    }

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

#if UNITY_EDITOR
        for (int i = 1; i < Enum.GetNames(typeof(ItemType)).Length; i++)
        {
            Debug.Log($"{(ItemType)i} Value : {userValues.GetValue((ItemType)i)}");
        }
#endif
    }
}