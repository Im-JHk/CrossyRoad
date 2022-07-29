using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player = null;
    private PlayerController playerController = null;

    private Vector3 direction;
    private Vector2Int currentTile;
    private DirectionType directionType;
    private float elapsedTime = 0f;
    private bool canMove = false;
    private bool isMove = false;
    private bool isJumpReady = false;

    private static readonly float moveInputDelay = 0.6f;
    private static readonly float maxMoveTime = 0.4f;
    private static readonly float maxHalfMoveTime = 0.1f;
    private static readonly float jumpPower = 10f;
    private static readonly float rotationSpeed = 360f;

    public Vector3 Direction { get { return direction; } private set { direction = value; } }
    public Vector2Int CurrentTile { get { return currentTile; } private set { currentTile = value; } }
    public bool IsMove { get { return isMove; } private set { isMove = value; } }
    public bool IsJumpReady { get { return isJumpReady; } private set { isJumpReady = value; } }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        playerController = GetComponent<PlayerController>();
        currentTile = new Vector2Int(0, LinearLineGenerator.maxHalfTile);
    }

    private void Start()
    {
        directionType = DirectionType.Down;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (GameManager.Instance.GetGameState == GameManager.GameState.Play && !isMove && elapsedTime >= moveInputDelay)
        {
            SetMovementInfomation();
            if (canMove && elapsedTime >= moveInputDelay)
            {
                StartCoroutine(Move());
                elapsedTime = 0f;
            }
        }
    }

    public void SetInitialize()
    {
        player.transform.position = LevelManager.Instance.LinearLineList[currentTile.x].TileList[currentTile.y].TilePosition + new Vector3(0f, 0.2f, 0f);
        directionType = DirectionType.Down;
    }

    private IEnumerator Move()
    {
        float elapsedMoveTime = 0.0f;
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = player.transform.position + direction * LinearLineGenerator.moveOnePoint;
        Quaternion startRotation = player.transform.rotation;

        SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.PlayerMoveSound);
        isMove = true;
        player.PlayerAnimator.SetBool("OnMove", true);

        while (elapsedMoveTime < maxMoveTime)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedMoveTime / maxMoveTime);
            player.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.LookRotation(direction), elapsedMoveTime / maxMoveTime);
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
        player.PlayerAnimator.SetBool("OnMove", false);

        GameManager.Instance.PlayerMove(player.transform.position, targetPosition - startPosition, directionType);

        yield break;
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
                    if(LevelManager.Instance.LinearLineList[currentTile.x].TileList[currentTile.y - 1].CanMove)
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
                    if(LevelManager.Instance.LinearLineList[currentTile.x].TileList[currentTile.y + 1].CanMove)
                    {
                        canMove = true;
                        break;
                    }
                }
                isMove = false;
                break;
            case DirectionType.Up:
                if (currentTile.x < LevelManager.Instance.LinearLineList.Count - 1)
                {
                    if(LevelManager.Instance.LinearLineList[currentTile.x + 1].TileList[currentTile.y].CanMove)
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
                    if(LevelManager.Instance.LinearLineList[currentTile.x - 1].TileList[currentTile.y].CanMove)
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
