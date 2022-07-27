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
    public enum GameState
    {
        Play = 0,
        Pause,
        Delay,
        Gameover
    }

    [SerializeField]
    private Player player = null;
    [SerializeField]
    private CameraMovement cameraMovement = null;
    [SerializeField]
    private DeadBlock deadblock = null;
    [SerializeField]
    private AttackBirdBlock attackBirdBlock = null;
    [SerializeField]
    private GameObject attackBird = null;

    private GameState gameState;

    private int gameScore;
    private int coinScore;

    //private bool isPause = false;
    //private bool isGameover = false;
    private bool isMoveStart = false;

    private static readonly float moveSpeed = 0.005f;

    #region properties
    public Player GetPlayer { get { return player; } }
    public GameState GetGameState { get { return gameState; } private set { gameState = value; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public int GameScore { get { return gameScore; } }
    public int CoinScore { get { return coinScore; } }
   // public bool IsPause { get { return isPause; } private set { isPause = value; } }
    //public bool IsGameover { get { return isGameover; } private set { isGameover = value; } }
    public bool IsMoveStart { get { return isMoveStart; } private set { isMoveStart = value; } }
    #endregion

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        cameraMovement = GetComponentInChildren<CameraMovement>();
        deadblock = GetComponentInChildren<DeadBlock>();
        attackBirdBlock = GetComponentInChildren<AttackBirdBlock>();
    }

    private void Start()
    {
        gameState = GameState.Play;
        gameScore = 0;
        coinScore = 0;
        UIManager.Instance.UpdateTextGameScore(gameScore);
        UIManager.Instance.UpdateTextCoinScore(coinScore);
    }

    private void FixedUpdate()
    {
        if(isMoveStart && gameState == GameState.Play)
        {
            deadblock.Move();
            cameraMovement.Move();
        }
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
        gameState = GameState.Pause;
        UIManager.Instance.SetActivePauseButtonUI(false);
        UIManager.Instance.SetActivePauseUI(true);
    }

    public void GameResume()
    {
        gameState = GameState.Play;
        UIManager.Instance.SetActivePauseUI(false);
        UIManager.Instance.SetActivePauseButtonUI(true);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameState = GameState.Gameover;
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
                    FollowMoveAll(moveDistance);
                }
                break;
            case DirectionType.Left: case DirectionType.Right:
                CameraMove(moveDistance, CameraFollowTarget.Player);
                break;
            default:
                break;
        }
    }

    public void FollowMoveAll(Vector3 moveDistance)
    {
        CameraMove(moveDistance, CameraFollowTarget.Player);
        deadblock.FollowTargetMove(moveDistance);
        attackBirdBlock.FollowTargetMove(moveDistance);
    }

    public void CameraMove(Vector3 moveDistance, CameraFollowTarget followTarget)
    {
        cameraMovement.FollowTarget(moveDistance, followTarget);
    }

    public void OnDieFromDeadBlock()
    {
        gameState = GameState.Delay;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
