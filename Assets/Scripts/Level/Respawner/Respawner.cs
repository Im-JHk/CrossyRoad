using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Queue<GameObject> obstacleQueue;

    private Vector3 worldPosition;
    private ObstacleType obstacleType;
    private ObjectPrefabType objectType;
    private DirectionType respawnDirection;
    private float elapsedTime = 0f;
    private float respawnRotationAngle = 0f;
    private int lineIndex;

    private float respawnDelay = 0f;

    #region properties
    public Vector3 WorldPosition { get { return worldPosition; } private set { worldPosition = value; } }
    public ObstacleType ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    public ObjectPrefabType ObjectType { get { return objectType; } private set { objectType = value; } }
    #endregion

    //public Respawner(Vector3 position, ObstacleType obstacleType, ObjectPrefabType objectType)
    //{
    //    //respawnPosition.position = position;
    //    //transform.position = position;
    //    worldPosition = position;
    //    this.obstacleType = obstacleType;
    //    this.objectType = objectType;
    //    respawnDelay = Random.Range(1f, 3f);
    //}

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
        //elapsedTime += Time.deltaTime;
        //if (elapsedTime > respawnDelay)
        //{
        //    print("go?");
        //    StartCoroutine(RespawnObstacle());
        //    elapsedTime = 0f;
        //}
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
        respawnDelay = Random.Range(2.5f, 5f);
    }

    public IEnumerator RespawnObstacle()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            print("coroutine");
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
                    break;
                case ObstacleType.Floater:
                    SetFloaterObstacle();
                    break;
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    public void SetTreeObstacle()
    {
        int randIndex = 0; //Random.Range(0, LinearLineGenerator.maxTile);
        GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        List<int> existNumber = new List<int>();

        foreach (var value in obstacleQueue)
        {
            existNumber.Add(value.GetComponent<Tree>().PositionIndex);
        }

        for(int i = 0; i < LinearLineGenerator.maxTile; ++i)
        {
            if (existNumber.Contains(i))
            {

            }
        }

        foreach(var value in existNumber)
        {
            //if()
        }

        newObject.transform.position = LevelManager.Instance.linearLineList[lineIndex].TileList[randIndex].TilePosition;
        newObject.GetComponent<Tree>().SetPositionAsTileIndex(randIndex);
        
        obstacleQueue.Enqueue(newObject);
    }

    public void SetFloaterObstacle()
    {
        int randIndex = Random.Range(0, LinearLineGenerator.maxTile);
        GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        //GameObject prefab = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        newObject.GetComponent<Floater>().SetPositionAsTileIndex(randIndex);
        //newObject.GetComponent

        newObject.transform.position = LevelManager.Instance.linearLineList[lineIndex].TileList[randIndex].TilePosition;
        //newObject.transform.rotation = Quaternion.LookRotation()

        obstacleQueue.Enqueue(newObject);
    }


    public void SetLogObstacle()
    {
        GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        //newObject.GetComponent<Log>().InitializeState();
        //newObject.transform.Rotate(Vector3.up * respawnRotationAngle);
        obstacleQueue.Enqueue(newObject);
    }

    public void SetCarObstacle()
    {
        GameObject newObject = ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].BorrowObject();
        print(newObject);
        newObject.GetComponent<Car>().InitializeState(transform.position, respawnDirection, respawnRotationAngle);
        //newObject.transform.Rotate(Vector3.up * respawnRotationAngle);
        obstacleQueue.Enqueue(newObject);
    }
}
