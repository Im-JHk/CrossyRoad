using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    [SerializeField]
    private List<GameObject> prefabList = new List<GameObject>(System.Enum.GetValues(typeof(LevelManager.ObjectPoolTypeList)).Length);
    private Dictionary<LevelManager.ObjectPoolTypeList, ObjectPool> objectPoolDictionary;
    private Dictionary<LevelManager.ObjectPoolTypeList, GameObject> poolDictionary;

    private static readonly int poolInitialDefaultSize = 80;
    private static readonly int poolInitialLineSize = 30;
    private static readonly int poolInitialOneSize = 1;

    public Dictionary<LevelManager.ObjectPoolTypeList, ObjectPool> ObjectPoolDictionary { get { return objectPoolDictionary; } }

    void Awake()
    {
        objectPoolDictionary = new Dictionary<LevelManager.ObjectPoolTypeList, ObjectPool>();
        poolDictionary = new Dictionary<LevelManager.ObjectPoolTypeList, GameObject>();
    }

    public void InitializeObjectPoolManager()
    {
        foreach (LevelManager.ObjectPoolTypeList type in System.Enum.GetValues(typeof(LevelManager.ObjectPoolTypeList)))
        {
            InitializePoolDictionary(prefabList[(int)type], type);
            InitializeObjectPool(prefabList[(int)type], type);
        }
    }

    private void InitializePoolDictionary(GameObject prefab, LevelManager.ObjectPoolTypeList type)
    {
        poolDictionary.Add(type, prefab);
    }

    private void InitializeObjectPool(GameObject prefab, LevelManager.ObjectPoolTypeList type)
    {
        ObjectPool newObjectPool = new ObjectPool();
        if(type == LevelManager.ObjectPoolTypeList.AttackBird)
        {
            newObjectPool.InitalizePool(prefab, type, poolInitialOneSize);
        }
        else if (type == LevelManager.ObjectPoolTypeList.Grass || type == LevelManager.ObjectPoolTypeList.Road || type == LevelManager.ObjectPoolTypeList.Water
            || type == LevelManager.ObjectPoolTypeList.Respawner || type == LevelManager.ObjectPoolTypeList.Deactivater)
        {
            newObjectPool.InitalizePool(prefab, type, poolInitialLineSize);
        }
        else
        {
            newObjectPool.InitalizePool(prefab, type, poolInitialDefaultSize);
        }
        objectPoolDictionary.Add(type, newObjectPool);
    }
}
