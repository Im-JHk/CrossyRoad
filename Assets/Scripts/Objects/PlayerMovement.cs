using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum DirectionType
    {
        None = 0,
        Left,
        Right,
        Up,
        Down
    }

    public LevelManager levelManager = null;
    public PlayerController playerController = null;
    public Player player = null;

    private Vector3 direction;
    private Vector2Int currentTile;
    private DirectionType directionType;
    private bool canMove = false;
    private bool isMove = false;
    private bool isJumpReady = false;

    private static readonly float maxMoveTime = 0.2f;

    public Vector3 Direction { get { return direction; } private set { direction = value; } }
    public bool IsMove { get { return isMove; } private set { isMove = value; } }
    public bool IsJumpReady { get { return isJumpReady; } private set { isJumpReady = value; } }

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerController = GetComponent<PlayerController>();
        player = GetComponentInParent<Player>();

        currentTile = new Vector2Int(0, LinearLineGenerator.maxHalfTile);
    }

    private void Start()
    {
        player.transform.position = levelManager.linearLineList[currentTile.x].TileList[currentTile.y].TileTransform;
        directionType = DirectionType.Down;
    }

    void Update()
    {
        if (!isMove)
        {
            SetMovementInfomation();
            if (canMove)
            {
                StartCoroutine(Move());
            }
        }
    }

    private IEnumerator Move()
    {
        float elapsedMoveTime = 0.0f;
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = player.transform.position + direction;

        isMove = true;

        while (elapsedMoveTime < maxMoveTime)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedMoveTime / maxMoveTime);
            elapsedMoveTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosition;

        switch (directionType)
        {
            case DirectionType.None:
                break;
            case DirectionType.Left:
                currentTile += new Vector2Int(0, -1);
                break;
            case DirectionType.Right:
                currentTile += new Vector2Int(0, 1);
                break;
            case DirectionType.Up:
                currentTile += new Vector2Int(1, 0);
                break;
            case DirectionType.Down:
                currentTile += new Vector2Int(-1, 0);
                break;
        }

        canMove = false;
        isMove = false;

        yield break;
    }

    void Rotate()
    {

    }

    private void SetMovementInfomation()
    {
        if (playerController.LeftKeyDown || playerController.RightKeyDown || playerController.UpKeyDown || playerController.DownKeyDown)
        {
            isJumpReady = true;
        }
        else
        {
            isJumpReady = false;
        }

        if (playerController.LeftKeyUp)
        {
            direction = Vector3.left;
            directionType = DirectionType.Left;
            OnMoveTargetPosition();
        }
        else if (playerController.RightKeyUp)
        {
            direction = Vector3.right;
            directionType = DirectionType.Right;
            OnMoveTargetPosition();
        }
        else if (playerController.UpKeyUp)
        {
            direction = Vector3.forward;
            directionType = DirectionType.Up;
            OnMoveTargetPosition();
        }
        else if (playerController.DownKeyUp)
        {
            direction = Vector3.back;
            directionType = DirectionType.Down;
            OnMoveTargetPosition();
        }
    }

    private void OnMoveTargetPosition()
    {
        switch (directionType)
        {
            case DirectionType.Left:
                if(currentTile.y > 0)
                {
                    if(levelManager.linearLineList[currentTile.x].TileList[currentTile.y - 1].CanMove)
                    {
                        canMove = true;
                        break;
                    }
                }
                isMove = false;
                break;
            case DirectionType.Right:
                if (currentTile.y < LinearLineGenerator.maxTile - 1)
                {
                    if(levelManager.linearLineList[currentTile.x].TileList[currentTile.y + 1].CanMove)
                    {
                        canMove = true;
                        break;
                    }
                }
                isMove = false;
                break;
            case DirectionType.Up:
                if (currentTile.x < levelManager.linearLineList.Count - 1)
                {
                    if(levelManager.linearLineList[currentTile.x + 1].TileList[currentTile.y].CanMove)
                    {
                        canMove = true;
                        break;
                    }
                }
                isMove = false;
                break;
            case DirectionType.Down:
                if (currentTile.x > 0)
                {
                    if(levelManager.linearLineList[currentTile.x - 1].TileList[currentTile.y].CanMove)
                    {
                        canMove = true;
                        break;
                    }
                }
                isMove = false;
                break;
        }
    }
}
