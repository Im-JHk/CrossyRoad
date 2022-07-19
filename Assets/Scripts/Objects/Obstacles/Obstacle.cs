using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject obstacleObject = null;
    private ObstacleType obstacleType;
    private ObjectPrefabType objectType;

    #region properties
    public GameObject ObstacleObject { get { return obstacleObject; } private set { obstacleObject = value; } }
    public ObstacleType ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    public ObjectPrefabType ObjectType { get { return objectType; } private set { objectType = value; } }
    #endregion

    public Obstacle()
    {

    }

    public Obstacle(GameObject obj, ObstacleType obstacleType, ObjectPrefabType objectType, Vector3 position)
    {
        this.obstacleObject = obj;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        this.transform.position = position;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            ObjectActiveState();
        }
    }

    public virtual void InitializeState()
    {
        print("InitializeState0 Parent");
    }

    public virtual void InitializeState(Vector3 position)
    {
        print("InitializeState1 Parent");
    }

    public virtual void InitializeState(Vector3 position, DirectionType direction, float rotateAngle, float moveSpeed)
    {
        print("InitializeState4 Parent");
    }

    public void ObjectActiveState()
    {
        switch (obstacleType)
        {
            case ObstacleType.Car:
                break;
            case ObstacleType.Train:
                break;
            case ObstacleType.Floater:
                break;
            case ObstacleType.Log:
                break;
            case ObstacleType.Tree:
                break;
            case ObstacleType.Rock:
                break;
        }
    }
}
