using UnityEngine;

public class DeadBlock : MonoBehaviour, ICyclicMovable
{
    private BoxCollider boxCollider = null;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(LinearLineGenerator.lineCenterWidth, 3f, 5f);
    }

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * GameManager.Instance.MoveSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CameraShake(1f, 10f);
            GameManager.Instance.OnDieFromDeadBlock();
        }
    }
}
