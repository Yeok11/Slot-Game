using System.Collections.Generic;
using UnityEngine;

public class InterfaceContainer : MonoBehaviour
{
    [SerializeField] private ResourceUI[] resourceUis;
    private Dictionary<ResourceType, int> resourceDic = new();

    public ResourceUI GetResource(ResourceType _type) => resourceDic.ContainsKey(_type) ? resourceUis[resourceDic[_type]] : null;

    private void Awake()
    {
        int _loop = resourceUis.Length;
        for (int i = 0; i < _loop; i++)
        {
            ResourceType _resourceType = resourceUis[i].resourceType;

            if (resourceDic.ContainsKey(_resourceType))
            {
                Debug.LogError($"ResourceDictionary에 {_resourceType} 키가 이미 존재하고 있습니다. (Now Value : {i} / Key Value : {resourceUis[i]})");
                continue;
            }
            else
            {
                resourceDic.Add(_resourceType, i);
            }
        }
    }
}