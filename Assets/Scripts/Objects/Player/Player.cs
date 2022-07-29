using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject = null;
    private Animator playerAnimator = null;
    private PlayerMovement playerMovement = null;
    private bool isAlive = true;

    public GameObject PlayerObject { get { return playerObject; } private set { playerObject = value; } }
    public Animator PlayerAnimator { get { return playerAnimator; } private set { playerAnimator = value; } }
    public bool IsAlive { get { return isAlive; } private set { isAlive = value; } }

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void InitializePlayer()
    {
        isAlive = true;
        transform.position = LevelManager.Instance.LinearLineList[playerMovement.CurrentTile.x].TileList[playerMovement.CurrentTile.y].TilePosition + new Vector3(0f, 0.2f, 0f);
    }

    public void OnChangeAliveState(bool b)
    {
        isAlive = b;
        GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackObstacle"))
        {
            GameManager.Instance.CameraShake(1f, 5f);
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.CrashCollisionSound);
            isAlive = false;
            playerAnimator.SetTrigger("OnDie");
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadFloor"))
        {
            GameManager.Instance.CameraShake(1f, 5f);
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.CrashCollisionSound);
            isAlive = false;
            GameManager.Instance.GameOver();
        }
    }
}
