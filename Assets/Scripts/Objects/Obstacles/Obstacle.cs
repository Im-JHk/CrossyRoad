using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject obstacleObject = null;
    private LevelManager.ObstacleType obstacleType;
    private LevelManager.ObjectPoolTypeList objectType;

    #region properties
    public GameObject ObstacleObject { get { return obstacleObject; } private set { obstacleObject = value; } }
    public LevelManager.ObstacleType ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }
    public LevelManager.ObjectPoolTypeList ObjectType { get { return objectType; } private set { objectType = value; } }
    #endregion

    public Obstacle()
    {

    }

    public Obstacle(GameObject obj, LevelManager.ObstacleType obstacleType, LevelManager.ObjectPoolTypeList objectType, Vector3 position)
    {
        this.obstacleObject = obj;
        this.obstacleType = obstacleType;
        this.objectType = objectType;
        this.transform.position = position;
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
}
