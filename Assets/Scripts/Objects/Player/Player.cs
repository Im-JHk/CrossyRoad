using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerStateType
    {
        Idle = 0,
        Move,
        CarriedByLog,
        Die
    }

    [SerializeField]
    private GameObject playerObject = null;
    private Animator playerAnimator = null;
    private PlayerMovement playerMovement = null;
    private PlayerStateType playerState;

    #region properties
    public GameObject PlayerObject { get { return playerObject; } private set { playerObject = value; } }
    public Animator PlayerAnimator { get { return playerAnimator; } private set { playerAnimator = value; } }
    public PlayerStateType PlayerState { get { return playerState; } private set { playerState = value; } }
    #endregion

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void InitializePlayer()
    {
        transform.position = LevelManager.Instance.LinearLineList[playerMovement.CurrentTile.x].TileList[playerMovement.CurrentTile.y].TilePosition + new Vector3(0f, 0.2f, 0f);
    }

    public void OnChangeState(PlayerStateType state)
    {
        playerState = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackObstacle"))
        {
            GameManager.Instance.CameraShake(0.5f, 2f);
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.CrashCollisionSound);
            playerState = PlayerStateType.Die;
            playerAnimator.SetTrigger("OnDie");
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadFloor"))
        {
            GameManager.Instance.CameraShake(0.5f, 2f);
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.CrashCollisionSound);
            playerState = PlayerStateType.Die;
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("CarryObstacle"))
        {
            playerState = PlayerStateType.CarriedByLog;
        }
    }
}
