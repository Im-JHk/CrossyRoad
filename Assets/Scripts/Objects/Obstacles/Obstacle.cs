using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject obstacleObject = null;
    //private ObstacleMovement obstacleMovement = null;
    private ObstacleType obstacleType;
    private ObjectPrefabType objectType;
    //private bool isMovable;

    #region properties
    public GameObject ObstacleObject { get { return obstacleObject; } private set { obstacleObject = value; } }
    //public ObstacleMovement ObstacleMovement { get { return obstacleMovement; } private set { obstacleMovement = value; } }
    public ObstacleType ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    public ObjectPrefabType ObjectType { get { return objectType; } private set { objectType = value; } }
    //public bool IsMovable { get { return isMovable; } private set { isMovable = value; } }
    #endregion

    public Obstacle()
    {

    }

    public Obstacle(GameObject obj, ObstacleType obstacleType, ObjectPrefabType objectType, Vector3 position, bool move)
    {
        //if (move)
        //{
        //    obstacleMovement = GetComponent<ObstacleMovement>();
        //}
        obstacleObject = obj;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        //isMovable = move;
        transform.position = position;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            ObjectActiveState();
        }
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
