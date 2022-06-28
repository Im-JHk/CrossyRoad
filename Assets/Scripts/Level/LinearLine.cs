using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearLine : MonoBehaviour
{
    public GameObject lineObject = null;

    private List<Tile> tileList = new List<Tile>();
    private Material lineMaterial;
    private Transform lineTransform;
    private int lineIndex;

    public List<Tile> TileList { get { return tileList; } set { tileList = value; } }
    public Material LineMaterial { get { return lineMaterial; } private set { lineMaterial = value; } }
    public Transform LineTransform { get { return lineTransform; } private set { lineTransform = value; } }
    public int LineIndex { get { return lineIndex; } private set { lineIndex = value; } }

    public Tile GetTile(int index) { return tileList[index]; }
    public void SetTile()
    {
        float horizontalLength = lineObject.transform.lossyScale.z;
        Vector3 zeroPosition = lineObject.transform.position + new Vector3(-LinearLineGenerator.halfLineWidth, LinearLineGenerator.lineHeight, LinearLineGenerator.halfLineDepth);
        for (int i = 0; i < horizontalLength; i++)
        {
            Tile tile = new Tile
            (
                zeroPosition + 
                new Vector3
                (
                    i * LinearLineGenerator.moveOnePoint + LinearLineGenerator.moveHalfPoint, 
                    0f, 
                    -LinearLineGenerator.moveHalfPoint
                ),
                i
            );

            tileList.Add(tile);
        }
    }

    public LinearLine(GameObject prefab, Vector3 position, int index)
    {
        lineObject = Instantiate(prefab);
        lineObject.transform.position = position;
        lineIndex = index;
    }
}
