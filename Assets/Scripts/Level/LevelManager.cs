using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinearLineType
{
    Grass = 0,
    Road,
    Water
}

public class LevelManager : SingletonBase<LevelManager>
{
    public LinearLineGenerator lineGenerator = null;
    public List<LinearLine> linearLineList;

    private void Awake()
    {
        lineGenerator = GetComponentInChildren<LinearLineGenerator>();
        linearLineList = new List<LinearLine>();

        for(int i = 0; i < 10; i++)
        {
            AddLinearLine();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
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
}
