using UnityEngine;

public class LinearLineGenerator : MonoBehaviour
{
    public GameObject[] linePrefabs;

    public static readonly float lineTotalWidth = 50.0f;
    public static readonly float lineCenterWidth = 10.0f;
    public static readonly float lineDepth = 1.0f;
    public static readonly float lineHeight = 1.0f;
    public static readonly float halfLineTotalWidth = lineTotalWidth * 0.5f;
    public static readonly float halfLineCenterWidth = lineCenterWidth * 0.5f;
    public static readonly float halfLineDepth = 0.5f;
    public static readonly float halfLineHeight = 0.5f;
    public static readonly float moveOnePoint = 1.0f;
    public static readonly float moveHalfPoint = 0.5f;
    public static readonly float tileOnePointSizeX = 1.0f;
    public static readonly int maxTile = (int)(lineCenterWidth * tileOnePointSizeX);
    public static readonly int maxHalfTile = (int)(lineCenterWidth * 0.5f);
    public static readonly int tileOneSizeInt = 1;

    public LinearLine GenerateLine()
    {
        int randomNumber = 0;

        Vector3 position = Vector3.zero;

        LinearLine newLine = new LinearLine(ObjectPoolManager.Instance.ObjectPoolDictionary[(LevelManager.ObjectPoolTypeList)randomNumber].BorrowObject(), position, (LevelManager.LinearLineType)randomNumber, 0);

        newLine.SetTile();

        return newLine;
    }

    public LinearLine GenerateLine(LinearLine lastLine)
    {
        int randomNumber = Random.Range(0, linePrefabs.Length);

        Vector3 position = lastLine.lineObject.transform.position + new Vector3(0, 0, lineDepth);

        LinearLine newLine = new LinearLine(ObjectPoolManager.Instance.ObjectPoolDictionary[(LevelManager.ObjectPoolTypeList)randomNumber].BorrowObject(), position, (LevelManager.LinearLineType)randomNumber, lastLine.LineIndex);

        newLine.SetTile();

        return newLine;
    }
}
