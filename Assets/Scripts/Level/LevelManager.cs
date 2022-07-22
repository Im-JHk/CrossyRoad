using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinearLineType
{
    Grass = 0,
    Road,
    Water
}
public enum ObstacleType
{
    Tree = 0,
    Rock,
    Car,
    Train,
    Floater,
    Log
}
public enum ObjectPrefabType
{
    Tree1 = 0,
    Tree2,
    Car1,
    Car2,
    Floater,
    Log,
    AttackBird
}

public class LevelManager : SingletonBase<LevelManager>
{
    private ObjectPool objectPool = null;
    private LinearLineGenerator lineGenerator = null;
    private RespawnerGenerator respawnerGenerator = null;
    private List<LinearLine> linearLineList = null;
    private List<GameObject> respawnerList = null;
    private List<GameObject> deactivaterList = null;

    #region properties
    public LinearLineGenerator LineGenerator { get { return lineGenerator; } private set { lineGenerator = value; } }
    public RespawnerGenerator RespawnerGenerator { get { return respawnerGenerator; } private set { respawnerGenerator = value; } }
    public List<LinearLine> LinearLineList { get { return linearLineList; } private set { linearLineList = value; } }
    public List<GameObject> RespawnerList { get { return respawnerList; } private set { respawnerList = value; } }
    public List<GameObject> DeactivaterList { get { return deactivaterList; } private set { deactivaterList = value; } }
    #endregion

    void Awake()
    {
        lineGenerator = GetComponentInChildren<LinearLineGenerator>();
        respawnerGenerator = GetComponentInChildren<RespawnerGenerator>();
        linearLineList = new List<LinearLine>();
        respawnerList = new List<GameObject>();
        deactivaterList = new List<GameObject>();

        // Test용으로 10개 생성
        for (int i = 0; i < 10; ++i)
        {
            AddLinearLine();
        }
        for (int i = 1; i < linearLineList.Count; ++i)
        {
            SetRespawner(i);
        }
    }

    void Start()
    {

    }

    void Update()
    {
    }

    public void AddLinearLine()
    {
        if(linearLineList.Count == 0)
        {
            linearLineList.Add(lineGenerator.GenerateLine());
        }
        else
        {
            linearLineList.Add(lineGenerator.GenerateLine(linearLineList[linearLineList.Count - 1]));
        }
        
    }

    public void SetRespawner(int lineIndex)
    {
        Vector3 respawnerPosition;
        Vector3 deactivaterPosition;
        ObstacleType obstacleType = 0;
        ObjectPrefabType objectType;
        DirectionType direction;
        float rotateAngle = 0f;
        int randNumberForType = 0;
        int randNumberForDirection = Random.Range(0, 2);

        switch (linearLineList[lineIndex].LineType)
        {
            case LinearLineType.Grass:
                randNumberForType = Random.Range(0, 2);
                obstacleType = ObstacleType.Tree;
                break;
            case LinearLineType.Road:
                randNumberForType = Random.Range(2, 4);
                obstacleType = ObstacleType.Car;
                break;
            case LinearLineType.Water:
                randNumberForType = Random.Range(4, 6);
                obstacleType = (ObstacleType)randNumberForType;
                break;
            default:
                print("default");
                break;
        }
        objectType = (ObjectPrefabType)randNumberForType;

        if(obstacleType == ObstacleType.Tree || obstacleType == ObstacleType.Floater)
        {
            respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(0f, LinearLineGenerator.lineHeight * 3f, 0f);
            deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(0f, LinearLineGenerator.lineHeight * 3f, 0f);

            respawnerList.Add(respawnerGenerator.GenerateRespawner(respawnerPosition, obstacleType, objectType, lineIndex));
            deactivaterList.Add(respawnerGenerator.GenerateDeactivater(deactivaterPosition, objectType));
        }
        else
        {
            // Direction: L = 0, R = 1
            if (randNumberForDirection == 0)
            {
                respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
                deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
                direction = DirectionType.Right;
                rotateAngle = 90f;
            }
            else
            {
                respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
                deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
                direction = DirectionType.Left;
                rotateAngle = -90f;
            }

            respawnerList.Add(respawnerGenerator.GenerateRespawner(respawnerPosition, obstacleType, objectType, direction, rotateAngle, lineIndex));
            deactivaterList.Add(respawnerGenerator.GenerateDeactivater(deactivaterPosition, objectType, -rotateAngle));
        }
    }

    private void OnDrawGizmos()
    {
        if (linearLineList != null)
        {
            Gizmos.color = new Color(255f, 255f, 255f, 255f);
            for (int i = 0; i < linearLineList.Count; ++i)
            {
                for (int j = 0; j < linearLineList[i].TileList.Count; ++j)
                {
                    Gizmos.DrawWireCube(linearLineList[i].TileList[j].TilePosition, new Vector3(LinearLineGenerator.moveOnePoint - 0.01f, 0f, LinearLineGenerator.lineDepth - 0.01f));
                }
            }
        }
        
        if(respawnerList != null)
        {
            Gizmos.color = new Color(0f, 0f, 255f, 255f);
            for (int i = 0; i < respawnerList.Count; ++i)
            {
                Gizmos.DrawWireCube(respawnerList[i].transform.position, new Vector3(1f, 1f, 0.99f));
            }
        }

        if(deactivaterList != null)
        {
            Gizmos.color = new Color(255f, 0f, 0f, 255f);
            for (int i = 0; i < deactivaterList.Count; ++i)
            {
                Gizmos.DrawWireCube(deactivaterList[i].transform.position, new Vector3(1f, 1f, 0.99f));
            }
        }
    }
}
