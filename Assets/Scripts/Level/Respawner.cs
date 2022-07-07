using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject obstacle = null;

    private Transform respawnPosition;
    private ObstacleType obstacleType;
    private readonly float respawnDelay = 0f;

    #region properties
    public Transform RespawnPosition { get { return respawnPosition; } private set { respawnPosition = value; } }
    public ObstacleType GetObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    #endregion

    public Respawner(Vector3 position, ObstacleType type)
    {
        respawnPosition.position = position;
        obstacleType = type;
        respawnDelay = Random.Range(1f, 3f);
    }

    void Awake()
    {
        respawnPosition = gameObject.AddComponent<Transform>();
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
