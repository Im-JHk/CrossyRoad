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
    Log
}

public class LevelManager : SingletonBase<LevelManager>
{
    public ObjectPool objectPool = null;
    public LinearLineGenerator lineGenerator = null;
    public RespawnerGenerator respawnerGenerator = null;
    public List<LinearLine> linearLineList;
    //public List<Respawner> respawnerList;
    //public List<Deactivater> deactivaterList;
    public List<GameObject> respawnerList;
    public List<GameObject> deactivaterList;

    void Awake()
    {
        lineGenerator = GetComponentInChildren<LinearLineGenerator>();
        respawnerGenerator = GetComponentInChildren<RespawnerGenerator>();
        linearLineList = new List<LinearLine>();
        //respawnerList = new List<Respawner>();
        //deactivaterList = new List<Deactivater>();
        respawnerList = new List<GameObject>();
        deactivaterList = new List<GameObject>();

        // Test용으로 10개 생성
        for (int i = 0; i < 10; ++i)
        {
            AddLinearLine();
        }
        for (int i = 0; i < linearLineList.Count; ++i)
        {
            if (linearLineList[i].LineType != LinearLineType.Grass)
            {
                AddRespawnerAndDeactivater(i);
            }
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

    public void AddRespawnerAndDeactivater(int lineIndex)
    {
        Vector3 respawnerPosition;
        Vector3 deactivaterPosition;
        ObstacleType obstacleType = 0;// = (ObstacleType)linearLineList[lineIndex].LineType;
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

        //if (linearLineList[lineIndex].LineType == LinearLineType.Water)
        //{
        //    obstacleType = (ObstacleType)Random.Range(4, 6);
        //    objectType = (ObjectPrefabType)Random.Range(4, 6);
        //}
        //else
        //{
        //    obstacleType = (ObstacleType)linearLineList[lineIndex].LineType;
        //}

        // L = 0, R = 1
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
        deactivaterList.Add(respawnerGenerator.GenerateDeactivater(deactivaterPosition));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 255f, 255f, 255f);
        for (int i = 0; i < linearLineList.Count; ++i)
        {
            for (int j = 0; j < linearLineList[i].TileList.Count; ++j)
            {
                Gizmos.DrawWireCube(linearLineList[i].TileList[j].TilePosition, new Vector3(LinearLineGenerator.moveOnePoint - 0.01f, 0f, LinearLineGenerator.lineDepth - 0.01f));
            }
        }

        Gizmos.color = new Color(0f, 0f, 255f, 255f);
        for (int i = 0; i < respawnerList.Count; ++i)
        {
            Gizmos.DrawWireCube(respawnerList[i].transform.position, new Vector3(1f, 1f, 0.99f));
        }

        Gizmos.color = new Color(255f, 0f, 0f, 255f);
        for (int i = 0; i < deactivaterList.Count; ++i)
        {
            Gizmos.DrawWireCube(deactivaterList[i].transform.position, new Vector3(1f, 1f, 0.99f));
        }
    }
}
