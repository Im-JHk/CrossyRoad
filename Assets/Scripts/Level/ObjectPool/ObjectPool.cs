using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    private GameObject objectPrefab;
    private ObjectPrefabType objectType;

    public void InitalizePool(GameObject prefab, ObjectPrefabType type, int numbers)
    {
        objectPrefab = prefab;
        objectType = type;
        for (int i = 0; i < numbers; ++i)
        {
            objectQueue.Enqueue(CreateNewObject());
        }
    }

    public GameObject CreateNewObject()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        return newObject;
    }

    public GameObject BorrowObject()
    {
        print("brr1");
        GameObject spawnObject;
        if (objectQueue.Count > 0)
        {
            spawnObject = objectQueue.Dequeue();
            print("brr2");
        }
        else
        {
            spawnObject = CreateNewObject();
            print("brr3");
        }
        spawnObject.SetActive(true);
        print("brr4");
        return spawnObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectQueue.Enqueue(obj);
    }
}
