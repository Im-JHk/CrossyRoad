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
    private AlwaysFollow alwaysFollow = null;
    [SerializeField]
    private DeadBlock deadblock = null;
    [SerializeField]
    private GameObject attackBird = null;

    private GameState gameState;
    private int gameScore;
    private int coinScore;
    private bool isMoveStart = false;

    private static readonly float moveSpeed = 0.005f;

    #region properties
    public Player GetPlayer { get { return player; } }
    public GameState GetGameState { get { return gameState; } private set { gameState = value; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public int GameScore { get { return gameScore; } }
    public int CoinScore { get { return coinScore; } }
    public bool IsMoveStart { get { return isMoveStart; } private set { isMoveStart = value; } }
    #endregion

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        cameraMovement = GetComponentInChildren<CameraMovement>();
        alwaysFollow = GetComponentInChildren<AlwaysFollow>();
        deadblock = GetComponentInChildren<DeadBlock>();
    }

    private void Start()
    {
        gameState = GameState.Play;
        gameScore = 0;
        coinScore = 0;
        UIManager.Instance.UpdateTextGameScore(gameScore);
        UIManager.Instance.UpdateTextCoinScore(coinScore);

        ObjectPoolManager.Instance.InitializeObjectPoolManager();
        LevelManager.Instance.InitializeLevelManager();

        player.InitializePlayer();
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
        SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.ButtonClickSound);
        Time.timeScale = 0f;
        gameState = GameState.Pause;
        UIManager.Instance.SetActivePauseButtonUI(false);
        UIManager.Instance.SetActivePauseUI(true);
    }

    public void GameResume()
    {
        SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.ButtonClickSound);
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
                if (deadblock.transform.position.z < playerPosition.z)
                {
                    FollowMoveToLerp(moveDistance);
                }
                alwaysFollow.MatchPlayerPositionZ(playerPosition.z);
                UpdateGameScore(1);
                LevelManager.Instance.AddLinearLine();
                LevelManager.Instance.SetRespawner(LevelManager.Instance.LinearLineList.Count - 1);
                break;
            case DirectionType.Left: case DirectionType.Right:
                CameraMove(moveDistance, CameraMovement.CameraFollowTarget.Player);
                break;
            default:
                break;
        }
    }

    public void FollowMoveToLerp(Vector3 moveDistance)
    {
        CameraMove(moveDistance, CameraMovement.CameraFollowTarget.Player);
        deadblock.FollowTargetMove(moveDistance);
    }

    public void CameraMove(Vector3 moveDistance, CameraMovement.CameraFollowTarget followTarget)
    {
        cameraMovement.FollowTarget(moveDistance, followTarget);
    }

    public void CameraShake(float time, float shakePower)
    {
        cameraMovement.ShakeCamera(time, shakePower);
    }

    public void OnDieFromDeadBlock()
    {
        gameState = GameState.Delay;
        attackBird = ObjectPoolManager.Instance.ObjectPoolDictionary[LevelManager.ObjectPoolTypeList.AttackBird].BorrowObject();
        attackBird.GetComponent<AttackBird>().SetInitialize();
        SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.FlySound);
    }

    public void UpdateGameScore(int plus)
    {
        gameScore += plus;
        UIManager.Instance.UpdateTextGameScore(gameScore);
    }

    public void UpdateCoinScore(int plus)
    {
        coinScore += plus;
        UIManager.Instance.UpdateTextCoinScore(coinScore);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
