using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonUtility
{
    public static List<int> RandomInt(int min, int max, int count)
    {
        List<int> randomList = new List<int>();
        bool[] selected = new bool[max + 1 - min];
        selected.Initialize();

        for(int i = 0; i < count;)
        {
            int random = Random.Range(min, max + 1);
            if(!selected[random])
            {
                selected[random] = true;
                randomList.Add(random);
                ++i;
            }
        }

        return randomList;
    }

    public static List<int> RandomIntExceptNumber(int min, int max, int count, int except)
    {
        List<int> randomList = new List<int>();
        bool[] selected = new bool[max + 1 - min];
        selected.Initialize();

        for (int i = 0; i < count;)
        {
            int random = Random.Range(min, max + 1);
            if (!selected[random] && except != random)
            {
                selected[random] = true;
                randomList.Add(random);
                ++i;
            }
        }

        return randomList;
    }
}
