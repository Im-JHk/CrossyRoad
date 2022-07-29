using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Queue<GameObject> obstacleQueue;

    [SerializeField]
    private LevelManager.ObstacleType obstacleType;
    [SerializeField]
    private LevelManager.ObjectPoolTypeList objectType;
    private Vector3 worldPosition;
    private DirectionType respawnDirection;
    private float respawnDelay = 0f;
    private float respawnRotationAngle = 0f;
    private float ObstacleMoveSpeed = 0f;
    private int lineIndex;

    private readonly int randomCountMax = 4;

    #region properties
    public Vector3 WorldPosition { get { return worldPosition; } private set { worldPosition = value; } }
    public LevelManager.ObstacleType ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    public LevelManager.ObjectPoolTypeList ObjectType { get { return objectType; } private set { objectType = value; } }
    #endregion

    void Awake()
    {
        obstacleQueue = new Queue<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(RespawnObstacle());
    }

    // Respawn StaticObject
    public void InitializeState(Vector3 position, LevelManager.ObstacleType obstacleType, LevelManager.ObjectPoolTypeList objectType, int lineIndex)
    {
        transform.position = position;
        worldPosition = position;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        this.lineIndex = lineIndex;
    }

    // Respawn MovableObject
    public void InitializeState(Vector3 position, LevelManager.ObstacleType obstacleType, LevelManager.ObjectPoolTypeList objectType, DirectionType direction, float rotateAngle, int lineIndex)
    {
        transform.position = position;
        worldPosition = position;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        this.respawnDirection = direction;
        this.respawnRotationAngle = rotateAngle;
        this.lineIndex = lineIndex;
        respawnDelay = Random.Range(3f, 6f);

        if (obstacleType == LevelManager.ObstacleType.Car)
        {
            ObstacleMoveSpeed = Random.Range(2f, 5f);
        }
        else if(obstacleType == LevelManager.ObstacleType.Log)
        {
            ObstacleMoveSpeed = Random.Range(2f, 4f);
        }
    }

    public IEnumerator RespawnObstacle()
    {
        while (GameManager.Instance.GetGameState == GameManager.GameState.Play && lineIndex != 0)
        {
            switch (obstacleType)
            {
                case LevelManager.ObstacleType.Car:
                    SetCarObstacle();
                    break;
                case LevelManager.ObstacleType.Log:
                    SetLogObstacle();
                    break;
                case LevelManager.ObstacleType.Tree:
                    SetTreeObstacle();
                    yield break;
                case LevelManager.ObstacleType.Floater:
                    SetFloaterObstacle();
                    yield break;
                case LevelManager.ObstacleType.None:
                    yield break;
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    public void SetTreeObstacle()
    {
        int randCount = Random.Range(1, randomCountMax);
        List<int> randomNumber = CommonUtility.RandomInt(0, LinearLineGenerator.maxTile - 1, randCount);
        if (lineIndex > 2)
        {
            Respawner prevRespawner = LevelManager.Instance.RespawnerList[lineIndex - 1].GetComponent<Respawner>();
            if (prevRespawner.obstacleType == LevelManager.ObstacleType.Floater)
            {
                randomNumber = CommonUtility.RandomIntExceptNumber
                (
                    0,
                    LinearLineGenerator.maxTile - 1,
                    randCount,
                    prevRespawner.obstacleQueue.Peek().GetComponent<Floater>().PositionIndex
                );
            }
        }
        
        for (int i = 0; i < randomNumber.Count; ++i)
        {
            GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
            newObject.transform.position = LevelManager.Instance.LinearLineList[lineIndex].TileList[randomNumber[i]].TilePosition;
            newObject.GetComponent<Tree>().SetPositionAsTileIndex(randomNumber[i]);
            LevelManager.Instance.LinearLineList[lineIndex].TileList[randomNumber[i]].SetCanMove(false);
            obstacleQueue.Enqueue(newObject);
        }
    }

    public void SetFloaterObstacle()
    {
        int randCount = Random.Range(1, randomCountMax);
        List<int> randomNumber = CommonUtility.RandomInt(0, LinearLineGenerator.maxTile - 1, randCount);

        for (int i = 0; i < randomNumber.Count; ++i)
        {
            GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
            newObject.transform.position = LevelManager.Instance.LinearLineList[lineIndex].TileList[randomNumber[i]].TilePosition;
            newObject.GetComponent<Floater>().SetPositionAsTileIndex(randomNumber[i]);
            obstacleQueue.Enqueue(newObject);
        }
    }

    public void SetLogObstacle()
    {
        GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        newObject.GetComponent<Log>().InitializeState(transform.position, respawnDirection, respawnRotationAngle, ObstacleMoveSpeed);
        obstacleQueue.Enqueue(newObject);
    }

    public void SetCarObstacle()
    {
        GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        newObject.GetComponent<Car>().InitializeState(transform.position, respawnDirection, respawnRotationAngle, ObstacleMoveSpeed);
        obstacleQueue.Enqueue(newObject);
    }

    public void ReturnAllObstacleToPool()
    {
        foreach(var iter in obstacleQueue)
        {
            ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].ReturnObject(iter);
        }
    }
}
