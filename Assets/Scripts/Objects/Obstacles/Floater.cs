using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : Obstacle
{
    private int positionIndex;
    public int PositionIndex { get { return positionIndex; } private set { positionIndex = value; } }

    public override void InitializeState(Vector3 position)
    {
        transform.position = position + new Vector3(0f, 0.15f, 0f);
    }

    public void SetPositionAsTileIndex(int index)
    {
        positionIndex = index;
    }
}
