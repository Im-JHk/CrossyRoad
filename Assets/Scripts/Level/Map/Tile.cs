using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector3 tileTransform;
    private int tileRowIndex;
    private int tileColumnIndex;
    private bool canMove;

    public Vector3 TileTransform { get { return tileTransform; } private set { tileTransform = value; } }
    public int TileRowIndex { get { return tileRowIndex; } private set { tileRowIndex = value; } }
    public int TileColumnIndex { get { return tileColumnIndex; } private set { tileColumnIndex = value; } }
    public bool CanMove { get { return canMove; } private set { canMove = value; } }

    public Tile(Vector3 position, int column, bool canMove = true)
    {
        tileTransform = position;
        tileColumnIndex = column;
        this.canMove = canMove;
    }
}
