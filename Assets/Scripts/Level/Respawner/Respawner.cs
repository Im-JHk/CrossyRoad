using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Queue<GameObject> obstacleQueue;

    private Vector3 worldPosition;
    [SerializeField]
    private ObstacleType obstacleType;
    [SerializeField]
    private ObjectPrefabType objectType;
    private DirectionType respawnDirection;
    private float respawnDelay = 0f;
    private float respawnRotationAngle = 0f;
    private float ObstacleMoveSpeed = 0f;
    private int lineIndex;

    private readonly int randomCountMax = 4;

    #region properties
    public Vector3 WorldPosition { get { return worldPosition; } private set { worldPosition = value; } }
    public ObstacleType ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    public ObjectPrefabType ObjectType { get { return objectType; } private set { objectType = value; } }
    #endregion

    void Awake()
    {
        obstacleQueue = new Queue<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(RespawnObstacle());
    }

    void Update()
    {
    }

    public void InitializeState(Vector3 position, ObstacleType obstacleType, ObjectPrefabType objectType, int lineIndex)
    {
        transform.position = position;
        worldPosition = position;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        this.lineIndex = lineIndex;
    }

    public void InitializeState(Vector3 position, ObstacleType obstacleType, ObjectPrefabType objectType, DirectionType direction, float rotateAngle, int lineIndex)
    {
        transform.position = position;
        worldPosition = position;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        this.respawnDirection = direction;
        this.respawnRotationAngle = rotateAngle;
        this.lineIndex = lineIndex;
        respawnDelay = Random.Range(3f, 6f);

        if (obstacleType == ObstacleType.Car)
        {
            ObstacleMoveSpeed = Random.Range(1.5f, 3.5f);
        }
        else if(obstacleType == ObstacleType.Log)
        {
            ObstacleMoveSpeed = Random.Range(1f, 2f);
        }
    }

    public IEnumerator RespawnObstacle()
    {
        while (GameManager.Instance.GetGameState == GameManager.GameState.Play && lineIndex != 0)
        {
            switch (obstacleType)
            {
                case ObstacleType.Car:
                    SetCarObstacle();
                    break;
                case ObstacleType.Log:
                    SetLogObstacle();
                    break;
                case ObstacleType.Tree:
                    SetTreeObstacle();
                    yield break;
                case ObstacleType.Floater:
                    SetFloaterObstacle();
                    yield break;
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    public void SetTreeObstacle()
    {
        int randCount = Random.Range(1, randomCountMax);
        List<int> randomNumber = CommonUtility.RandomInt(0, LinearLineGenerator.maxTile - 1, randCount);

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
