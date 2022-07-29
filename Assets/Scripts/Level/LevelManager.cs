using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonBase<LevelManager>
{
    public enum ObjectPoolTypeList
    {
        Grass = 0,
        Road,
        Water,
        Respawner,
        Deactivater,
        Tree1,
        Tree2,
        Car1,
        Car2,
        Floater,
        Log,
        AttackBird
    }
    public enum LinearLineType
    {
        Grass = 0,
        Road,
        Water
    }
    public enum ObstacleType
    {
        None = 0,
        Tree,
        Car,
        Floater,
        Log
    }

    private ObjectPool objectPool = null;
    private LinearLineGenerator lineGenerator = null;
    private RespawnerGenerator respawnerGenerator = null;
    private List<LinearLine> linearLineList = null;
    private List<GameObject> respawnerList = null;
    private List<GameObject> deactivaterList = null;

    private readonly int defaultFrontNumbers = 30;

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
    }

    public void InitializeLevelManager()
    {
        for (int i = 0; i < defaultFrontNumbers; ++i)
        {
            AddLinearLine();
        }
        for (int i = 0; i < linearLineList.Count; ++i)
        {
            SetRespawner(i);
        }
    }

    public void AddLinearLine()
    {
        if (linearLineList.Count == 0)
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
        ObjectPoolTypeList objectType;
        DirectionType direction;
        float rotateAngle = 0f;
        int randNumberForType = 0;
        int randNumberForDirection = Random.Range(0, 2);

        switch (linearLineList[lineIndex].LineType)
        {
            case LinearLineType.Grass:
                randNumberForType = Random.Range(5, 7);
                obstacleType = ObstacleType.Tree;
                break;
            case LinearLineType.Road:
                randNumberForType = Random.Range(7, 9);
                obstacleType = ObstacleType.Car;
                break;
            case LinearLineType.Water:
                randNumberForType = Random.Range(9, 11);
                if (randNumberForType == 9)
                {
                    obstacleType = ObstacleType.Floater;
                    if (lineIndex >= 2)
                    {
                        if(respawnerList[lineIndex - 2].GetComponent<Respawner>().ObstacleType == ObstacleType.Floater)
                        {
                            obstacleType = ObstacleType.Log;
                            randNumberForType += 1;
                        }
                    }
                }
                else
                {
                    obstacleType = ObstacleType.Log;
                }
                break;
            default:
                print("default");
                break;
        }
        objectType = (ObjectPoolTypeList)randNumberForType;

        if(lineIndex == 0)
        {
            obstacleType = ObstacleType.None;
            objectType = ObjectPoolTypeList.Respawner;
        }

        if(obstacleType == ObstacleType.Tree || obstacleType == ObstacleType.Floater)
        {
            respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(0f, LinearLineGenerator.lineHeight * 3f, 0f);
            deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(0f, LinearLineGenerator.lineHeight * 3f, 0f);

            respawnerList.Add(respawnerGenerator.GenerateRespawner(respawnerPosition, obstacleType, objectType, lineIndex));
            deactivaterList.Add(respawnerGenerator.GenerateDeactivater(deactivaterPosition, objectType));
        }
        else if (obstacleType == ObstacleType.None)
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
                respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineTotalWidth, LinearLineGenerator.lineHeight, 0f);
                deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineTotalWidth, LinearLineGenerator.lineHeight, 0f);
                direction = DirectionType.Right;
                rotateAngle = 90f;
            }
            else
            {
                respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineTotalWidth, LinearLineGenerator.lineHeight, 0f);
                deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineTotalWidth, LinearLineGenerator.lineHeight, 0f);
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
