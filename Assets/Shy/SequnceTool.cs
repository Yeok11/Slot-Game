using System;
using System.Collections;
using UnityEngine;

public class SequnceTool : MonoBehaviour
{
    public static SequnceTool Instance;

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
    }

    public void Delay(Action _endAciton, float _delay) => StartCoroutine(DelayEvent(_endAciton, _delay));

    private IEnumerator DelayEvent(Action _endAction, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _endAction?.Invoke();
    }
}
