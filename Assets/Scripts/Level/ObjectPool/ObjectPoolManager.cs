using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    [SerializeField]
    private List<GameObject> prefabList = new List<GameObject>(sizeof(ObjectPrefabType));
    private Dictionary<ObjectPrefabType, ObjectPool> objectPoolDictionary;
    private Dictionary<ObjectPrefabType, GameObject> poolDictionary;

    private static readonly int poolInitailSize = 10;

    void Awake()
    {
        objectPoolDictionary = new Dictionary<ObjectPrefabType, ObjectPool>();
        poolDictionary = new Dictionary<ObjectPrefabType, GameObject>();

        foreach(ObjectPrefabType type in System.Enum.GetValues(typeof(ObjectPrefabType)))
        {
            InitializePoolDictionary(prefabList[(int)type], type);
            InitializeObjectPool(prefabList[(int)type], type);
        }
    }

    private void InitializePoolDictionary(GameObject prefab, ObjectPrefabType type)
    {
        poolDictionary.Add(type, prefab);
    }

    private void InitializeObjectPool(GameObject prefab, ObjectPrefabType type)
    {
        ObjectPool newObjectPool = new ObjectPool();
        newObjectPool.InitalizePool(prefab, type, poolInitailSize);
        objectPoolDictionary.Add(type, newObjectPool);
    }
}
