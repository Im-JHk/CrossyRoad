using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject = null;
    private Animator playerAnimator = null;
    private bool isAlive = true;

    public GameObject PlayerObject { get { return playerObject; } private set { playerObject = value; } }
    public Animator PlayerAnimator { get { return playerAnimator; } private set { playerAnimator = value; } }
    public bool IsAlive { get { return isAlive; } private set { isAlive = value; } }

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void InitializePlayer()
    {
        isAlive = true;
        transform.position = new Vector3(0f, 0f, 0f);
    }

    public void OnChangeAliveState(bool b)
    {
        isAlive = b;
        GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackObstacle") || other.CompareTag("Floor"))
        {
            isAlive = false;
            playerAnimator.SetTrigger("OnDie");
            GameManager.Instance.GameOver();
        }
    }
}
