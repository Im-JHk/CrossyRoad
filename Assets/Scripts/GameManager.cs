using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionType
{
    None = 0,
    Left,
    Right,
    Up,
    Down
}

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private CameraMovement cameraMovement = null;
    [SerializeField]
    private DeadBlock deadBlock = null;
    [SerializeField]
    private GameObject attackBird = null;

    private bool isGameOver = false;
    private bool isMoveStart = false;

    private static readonly float moveSpeed = 0.005f;

    #region properties
    public Player GetPlayer { get { return player; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public bool IsGameOver { get { return isGameOver; } private set { isGameOver = value; } }
    public bool IsMoveStart { get { return isMoveStart; } private set { isMoveStart = value; } }
    #endregion

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraMovement = GetComponentInChildren<CameraMovement>();
        deadBlock = GetComponentInChildren<DeadBlock>();
    }

    private void FixedUpdate()
    {
        if(isMoveStart && !isGameOver)
        {
            deadBlock.Move();
            cameraMovement.Move();
        }
    }

    public void GameOver(bool b)
    {
        isGameOver = b;
    }

    public void PlayerMoveStart(bool b)
    {
        isMoveStart = b;
    }

    public void PlayerMove(Vector3 playerPosition, Vector3 moveDistance, DirectionType directionType)
    {
        if (!isMoveStart)
        {
            isMoveStart = true;
        }
        switch (directionType)
        {
            case DirectionType.Up:
                if (deadBlock.transform.position.z <= playerPosition.z)
                {
                    DeadBlockMove(moveDistance);
                }
                break;
            case DirectionType.Left: case DirectionType.Right:
                CameraMove(moveDistance, CameraFollowTarget.Player);
                break;
            default:
                break;
        }
        
    }

    public void DeadBlockMove(Vector3 moveDistance)
    {
        CameraMove(moveDistance, CameraFollowTarget.Player);
        deadBlock.FollowTargetMove(moveDistance);
    }

    public void CameraMove(Vector3 moveDistance, CameraFollowTarget followTarget)
    {
        cameraMovement.FollowTarget(moveDistance, followTarget);
    }

    public void OnDieFromDeadBlock()
    {
        attackBird = ObjectPoolManager.Instance.ObjectPoolDictionary[ObjectPrefabType.AttackBird].BorrowObject();
        attackBird.GetComponent<AttackBird>().SetInitialize();

    }
}
