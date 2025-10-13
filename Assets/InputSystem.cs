using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    private List<RaycastResult> results;

    private void Awake()
    {
        results = new();
        pointerEventData = new(eventSystem);

        eventSystem = FindAnyObjectByType<EventSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var _clickObject = GetClickAbleUi();

            Debug.Log(_clickObject);

            if (_clickObject != null) _clickObject.OnClickDown();

        }
    }

    private IClickEvent GetClickAbleUi()
    {
        pointerEventData.position = Input.mousePosition;

        results.Clear();
        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            foreach (var _result in results)
            {
                if (_result.gameObject.TryGetComponent(out IClickEvent _clickItem)) return _clickItem;
            }
        }

        return null;
    }
}
