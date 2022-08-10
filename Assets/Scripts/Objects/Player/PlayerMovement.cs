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
    private bool isJumpReady = false;

    private static readonly float moveInputDelay = 0.6f;
    private static readonly float maxMoveTime = 0.4f;
    private static readonly float maxHalfMoveTime = 0.1f;
    private static readonly float jumpPower = 10f;
    private static readonly float rotationSpeed = 360f;

    public Vector3 Direction { get { return direction; } private set { direction = value; } }
    public Vector2Int CurrentTile { get { return currentTile; } private set { currentTile = value; } }
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
        if (GameManager.Instance.GetGameState == GameManager.GameState.Play && player.PlayerState != Player.PlayerStateType.Move)
        {
            if(player.PlayerState == Player.PlayerStateType.CarriedByLog)
            {
                currentTile = new Vector2Int(currentTile.x, (int)(player.transform.position.x + LinearLineGenerator.maxHalfTile) / LinearLineGenerator.tileOneSizeInt);
                GameManager.Instance.CameraFollowPlayer();
            }
            if(elapsedTime >= moveInputDelay)
            {
                SetMovementInfomation();
                if (canMove)
                {
                    StartCoroutine(Move(directionType));
                    elapsedTime = 0f;
                }
            }
        }
    }

    public void SetInitialize()
    {
        player.transform.position = LevelManager.Instance.GetTilePositionToIndex(currentTile) + new Vector3(0f, 0.2f, 0f);
        directionType = DirectionType.Down;
    }

    private IEnumerator Move(DirectionType moveDirection)
    {
        float elapsedMoveTime = 0.0f;
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = LevelManager.Instance.GetTilePositionToIndex(currentTile) + direction * LinearLineGenerator.moveOnePoint;
        Quaternion startRotation = player.transform.rotation;

        SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.PlayerMoveSound);
        player.OnChangeState(Player.PlayerStateType.Move);
        player.PlayerAnimator.SetBool("OnMove", true);

        while (elapsedMoveTime < maxMoveTime)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedMoveTime / maxMoveTime);
            player.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.LookRotation(direction), elapsedMoveTime / maxMoveTime);
            elapsedMoveTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosition;

        switch (moveDirection)
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
        player.PlayerAnimator.SetBool("OnMove", false);
        player.OnChangeState(Player.PlayerStateType.Idle);

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
                player.OnChangeState(Player.PlayerStateType.Idle);
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
                player.OnChangeState(Player.PlayerStateType.Idle);
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
                player.OnChangeState(Player.PlayerStateType.Idle);
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
                player.OnChangeState(Player.PlayerStateType.Idle);
                break;
        }
    }
}
