using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject respawner = null;
    [SerializeField]
    private GameObject deactivater = null;

    public GameObject GenerateRespawner(Vector3 position, ObstacleType obstacleType, ObjectPrefabType objectType, int lineIndex)
    {
        GameObject newRespawner = Instantiate(respawner);
        newRespawner.AddComponent<Respawner>();
        newRespawner.GetComponent<Respawner>().InitializeState(position, obstacleType, objectType, lineIndex);

        //Respawner newRespawner = new Respawner(prefeb, position, obstacleType, objectType);

        newRespawner.transform.position = position;

        return newRespawner;
    }

    public GameObject GenerateDeactivater(Vector3 position)
    {
        GameObject newDeactivater = Instantiate(deactivater);
        newDeactivater.AddComponent<Deactivater>();
        newDeactivater.GetComponent<Deactivater>().InitializeState(position);

        //Deactivater newDeactivater = new Deactivater(prefeb, position);

        newDeactivater.transform.position = position;

        return newDeactivater;
    }
}
