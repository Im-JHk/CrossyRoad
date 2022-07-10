using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinearLineType
{
    Grass = 0,
    Road,
    Rail,
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

public class LevelManager : SingletonBase<LevelManager>
{
    public ObjectPool objectPool = null;
    public LinearLineGenerator lineGenerator = null;
    public RespawnerGenerator respawnerGenerator = null;
    public List<LinearLine> linearLineList;
    public List<Respawner> respawnerList;
    public List<DeactivateObstacle> deactivaterList;

    void Awake()
    {
        lineGenerator = GetComponentInChildren<LinearLineGenerator>();
        respawnerGenerator = GetComponentInChildren<RespawnerGenerator>();
        linearLineList = new List<LinearLine>();
        respawnerList = new List<Respawner>();
        deactivaterList = new List<DeactivateObstacle>();

        // Test용으로 10개 생성
        for (int i = 0; i < 10; ++i)
        {
            AddLinearLine();
        }
        for (int i = 0; i < linearLineList.Count; ++i)
        {
            AddRespawnerAndDeactivater(i);
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
        ObstacleType obstacleType;
        int randNumber = Random.Range(0, 2);

        if(linearLineList[lineIndex].LineType == LinearLineType.Water)
        {
            obstacleType = (ObstacleType)Random.Range(2, 4);
        }
        else
        {
            obstacleType = (ObstacleType)linearLineList[lineIndex].LineType;
        }

        if (randNumber == 0)
        {
            respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
            deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
        }
        else
        {
            respawnerPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
            deactivaterPosition = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
        }

        respawnerList.Add(respawnerGenerator.GenerateRespawner(respawnerPosition, obstacleType));
        deactivaterList.Add(respawnerGenerator.GenerateDeactivater(deactivaterPosition));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 255f, 255f, 255f);
        for (int i = 0; i < linearLineList.Count; ++i)
        {
            for (int j = 0; j < linearLineList[i].TileList.Count; ++j)
            {
                Gizmos.DrawWireCube(linearLineList[i].TileList[j].TileTransform, new Vector3(LinearLineGenerator.moveOnePoint - 0.01f, 0f, LinearLineGenerator.lineDepth - 0.01f));
            }
        }

        Gizmos.color = new Color(0f, 0f, 255f, 255f);
        for (int i = 0; i < respawnerList.Count; ++i)
        {
            Gizmos.DrawWireCube(respawnerList[i].WorldPosition, new Vector3(1f, 1f, 0.99f));
        }

        Gizmos.color = new Color(255f, 0f, 0f, 255f);
        for (int i = 0; i < deactivaterList.Count; ++i)
        {
            Gizmos.DrawWireCube(deactivaterList[i].WorldPosition, new Vector3(1f, 1f, 0.99f));
        }
    }
}
