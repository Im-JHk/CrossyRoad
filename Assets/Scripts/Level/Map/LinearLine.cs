using System.Collections.Generic;
using UnityEngine;

public class LinearLine : MonoBehaviour
{
    public GameObject lineObject = null;
    public Transform centerTransform = null;

    #region private variable
    private List<Tile> tileList = new List<Tile>();
    private Material lineMaterial;
    private Transform lineTransform;
    private LevelManager.LinearLineType lineType;
    private int lineIndex;
    #endregion

    #region properties
    public List<Tile> TileList { get { return tileList; } set { tileList = value; } }
    public Material LineMaterial { get { return lineMaterial; } private set { lineMaterial = value; } }
    public Transform LineTransform { get { return lineTransform; } private set { lineTransform = value; } }
    public LevelManager.LinearLineType LineType { get { return lineType; } private set { lineType = value; } }
    public int LineIndex { get { return lineIndex; } private set { lineIndex = value; } }
    #endregion

    public LinearLine(GameObject prefab, Vector3 position, LevelManager.LinearLineType type, int index)
    {
        lineObject = prefab;
        lineObject.transform.position = position;
        centerTransform = lineObject.transform.GetChild(0);
        centerTransform.transform.position = position;
        lineTransform = lineObject.transform;
        lineType = type;
        lineIndex = index;
    }

    public Tile GetTile(int index) { return tileList[index]; }
    public void SetTile()
    {
        float horizontalLength = centerTransform.transform.lossyScale.z;
        Vector3 zeroPosition = centerTransform.transform.position + 
            new Vector3(-LinearLineGenerator.halfLineCenterWidth, LinearLineGenerator.halfLineHeight, LinearLineGenerator.halfLineDepth);
        for (int i = 0; i < (int)horizontalLength; ++i)
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
}
