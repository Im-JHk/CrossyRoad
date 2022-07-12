using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearLineGenerator : MonoBehaviour
{
    public GameObject[] linePrefabs;

    public static readonly float lineWidth = 10.0f;
    public static readonly float lineDepth = 1.0f;
    public static readonly float lineHeight = 1.0f;
    public static readonly float halfLineWidth = 5.0f;
    public static readonly float halfLineDepth = 0.5f;
    public static readonly float halfLineHeight = 0.5f;
    public static readonly float moveOnePoint = 1.0f;
    public static readonly float moveHalfPoint = 0.5f;
    public static readonly float tileSizeX = 1.0f;
    public static readonly int maxTile = (int)(lineWidth * tileSizeX);
    public static readonly int maxHalfTile = (int)(lineWidth * 0.5f);

    public LinearLine GenerateLine()
    {
        int randomNumber = 0;

        Vector3 position = Vector3.zero;

        LinearLine newLine = new LinearLine(linePrefabs[randomNumber], position, (LinearLineType)randomNumber, 0);

        newLine.SetTile();

        return newLine;
    }

    public LinearLine GenerateLine(LinearLine lastLine)
    {
        int randomNumber = Random.Range(0, linePrefabs.Length);

        Vector3 position = lastLine.lineObject.transform.position + new Vector3(0, 0, lineDepth);

        LinearLine newLine = new LinearLine(linePrefabs[randomNumber], position, (LinearLineType)randomNumber, lastLine.LineIndex);

        newLine.SetTile();

        return newLine;
    }
}
