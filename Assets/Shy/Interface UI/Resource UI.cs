using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI valueTmp;
    [field: SerializeField] public ResourceType resourceType { get; private set; }
}
