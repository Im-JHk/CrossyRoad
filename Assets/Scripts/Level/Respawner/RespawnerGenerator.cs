using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerGenerator : MonoBehaviour
{
    public GameObject respawner = null;

    public Respawner GenerateRespawner(Vector3 position, ObstacleType type)
    {
        Respawner newRespawner = new Respawner(position, type);

        return newRespawner;
    }

    public DeactivateObstacle GenerateDeactivater(Vector3 position)
    {
        DeactivateObstacle newDeactivater = new DeactivateObstacle(position);

        return newDeactivater;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
