using UnityEngine;
using UnityEngine.SceneManagement;

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
    private DeadBlock deadblock = null;
    [SerializeField]
    private GameObject attackBird = null;

    private int gameScore;
    private int coinScore;

    private bool isGameover = false;
    private bool isMoveStart = false;

    private static readonly float moveSpeed = 0.005f;

    #region properties
    public Player GetPlayer { get { return player; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public int GameScore { get { return gameScore; } }
    public int CoinScore { get { return coinScore; } }
    public bool IsGameover { get { return isGameover; } private set { isGameover = value; } }
    public bool IsMoveStart { get { return isMoveStart; } private set { isMoveStart = value; } }
    #endregion

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraMovement = GetComponentInChildren<CameraMovement>();
        deadblock = GetComponentInChildren<DeadBlock>();
        gameScore = 0;
        coinScore = 0;
        UIManager.Instance.UpdateTextGameScore(gameScore);
        UIManager.Instance.UpdateTextCoinScore(coinScore);
    }

    private void FixedUpdate()
    {
        if(isMoveStart && !isGameover)
        {
            deadblock.Move();
            cameraMovement.Move();
        }
    }

    public void GameOver()
    {
        isGameover = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        UIManager.Instance.SetActiveGameoverUI(true);
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
                if (deadblock.transform.position.z <= playerPosition.z)
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
        deadblock.FollowTargetMove(moveDistance);
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

    public void UpdateGameScore(int plus)
    {
        gameScore += plus;
    }

    public void UpdateCoinScore(int plus)
    {
        coinScore += plus;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
