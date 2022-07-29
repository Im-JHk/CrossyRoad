using UnityEngine;

public class Tree : Obstacle
{
    private int positionIndex;
    public int PositionIndex { get { return positionIndex; } private set { positionIndex = value; } }

    public override void InitializeState(Vector3 position)
    {
        transform.position = position;
    }

    public void SetPositionAsTileIndex(int index)
    {
        positionIndex = index;
    }
}
