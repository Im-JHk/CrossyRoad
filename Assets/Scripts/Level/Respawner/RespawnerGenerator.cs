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
        newRespawner.GetComponent<Respawner>().InitializeState(position, obstacleType, objectType, lineIndex);
        newRespawner.transform.position = position;

        return newRespawner;
    }

    public GameObject GenerateRespawner(Vector3 position, ObstacleType obstacleType, ObjectPrefabType objectType, DirectionType direction, float rotateAngle, int lineIndex)
    {
        GameObject newRespawner = Instantiate(respawner);
        newRespawner.GetComponent<Respawner>().InitializeState(position, obstacleType, objectType, direction, rotateAngle, lineIndex);
        newRespawner.transform.position = position;

        return newRespawner;
    }

    public GameObject GenerateDeactivater(Vector3 position, ObjectPrefabType objectType)
    {
        GameObject newDeactivater = Instantiate(deactivater);
        newDeactivater.GetComponent<Deactivater>().InitializeState(position, objectType);
        newDeactivater.transform.position = position;

        return newDeactivater;
    }

    public void SetStaticObstacle(Vector3 position, ObjectPrefabType objectType)
    {

    }
}
