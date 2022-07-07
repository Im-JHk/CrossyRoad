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
    Car = 0,
    Train,
    Floater,
    Log
}

public class LevelManager : SingletonBase<LevelManager>
{
    public LinearLineGenerator lineGenerator = null;
    public RespawnerGenerator respawnerGenerator = null;
    public List<LinearLine> linearLineList;
    public List<Respawner> respawnerList;

    private void Awake()
    {
        lineGenerator = GetComponentInChildren<LinearLineGenerator>();
        respawnerGenerator = GetComponentInChildren<RespawnerGenerator>();
        linearLineList = new List<LinearLine>();
        respawnerList = new List<Respawner>();
    }

    void Start()
    {
        // Test용으로 10개 생성
        for (int i = 0; i < 10; i++)
        {
            AddLinearLine();
        }
        for(int i = 0; i < linearLineList.Count; ++i)
        {
            print("i: " + i);
            AddRespawner(i);
        }
    }

    void Update()
    {
        // Test용
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddLinearLine();
        }
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

    public void AddRespawner(int lineIndex)
    {
        Vector3 position;
        ObstacleType obstacleType;
        int randNumber = Random.Range(0, 2);

        print(linearLineList[lineIndex].LineType);

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
            position = linearLineList[lineIndex].LineTransform.position + new Vector3(-LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
        }
        else
        {
            position = linearLineList[lineIndex].LineTransform.position + new Vector3(LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, 0f);
        }

        respawnerList.Add(respawnerGenerator.GenerateRespawner(position, obstacleType));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(25f, 25f, 25f, 255f);
        for (int i = 0; i < linearLineList.Count; ++i)
        {
            for (int j = 0; j < linearLineList[i].TileList.Count; ++j)
            {
                Gizmos.DrawWireCube(linearLineList[i].TileList[j].TileTransform, new Vector3(LinearLineGenerator.moveOnePoint, 0f, LinearLineGenerator.lineDepth));
            }
        }
        for(int i = 0; i < respawnerList.Count; ++i)
        {
            print(i + ": " + respawnerList[i].RespawnPosition.position);
            Gizmos.DrawWireCube(respawnerList[i].RespawnPosition.position, new Vector3(1.0f, 1f, 1f));
        }
    }
}
