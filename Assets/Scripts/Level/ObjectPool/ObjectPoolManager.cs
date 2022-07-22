using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    [SerializeField]
    private List<GameObject> prefabList = new List<GameObject>(System.Enum.GetValues(typeof(ObjectPrefabType)).Length);
    private Dictionary<ObjectPrefabType, ObjectPool> objectPoolDictionary;
    private Dictionary<ObjectPrefabType, GameObject> poolDictionary;

    private static readonly int poolInitialDefaultSize = 20;
    private static readonly int poolInitialOneSize = 1;

    public Dictionary<ObjectPrefabType, ObjectPool> ObjectPoolDictionary { get { return objectPoolDictionary; } }

    void Awake()
    {
        objectPoolDictionary = new Dictionary<ObjectPrefabType, ObjectPool>();
        poolDictionary = new Dictionary<ObjectPrefabType, GameObject>();
    }

    void Start()
    {
        foreach (ObjectPrefabType type in System.Enum.GetValues(typeof(ObjectPrefabType)))
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
        if(type == ObjectPrefabType.AttackBird)
        {
            newObjectPool.InitalizePool(prefab, type, poolInitialOneSize);
        }
        else
        {
            newObjectPool.InitalizePool(prefab, type, poolInitialDefaultSize);
        }
        objectPoolDictionary.Add(type, newObjectPool);
    }
}
