using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public enum ObstacleType
    {
        Car = 0,
        Train,
        Floater,
        Log
    }
    public GameObject respawnPosition = null;
    public GameObject obstacle = null;

    private ObstacleType obstacleType;
    private float respawnDelay = 0f;

    void Start()
    {
        //obstacleType = 
        respawnDelay = Random.Range(0.5f, 3f);

    }

    void Update()
    {
        
    }

    private IEnumerator RespawnObstacle()
    {
        //Instantiate()
        yield return new WaitForSeconds(respawnDelay);
    }

    private ObstacleType AllocateObstacleType()
    {
        
        //if(this.GetComponentInParent<LinearLine>().CompareTag(""))
        return ObstacleType.Car;
    }
}
