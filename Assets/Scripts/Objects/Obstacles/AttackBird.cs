using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBird : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject = null;
    private Rigidbody rigidbody = null;
    private Vector3 direction = Vector3.zero;
    private ObjectPrefabType objectType = ObjectPrefabType.AttackBird;

    private float moveSpeed = 0.01f;
    private float height = 2f;
    private float activePositionZ = 5f;

    private bool isTakePlayer = false;

    private static readonly Vector3 riseDirection = new Vector3(0f, 1f, 2f);

    public bool IsTakePlayer { get { return isTakePlayer; } private set { isTakePlayer = value; } }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetInitialize()
    {
        transform.position = GameManager.Instance.GetPlayer.transform.position + new Vector3(0f, height, activePositionZ);
        if (targetObject != null)
        {
            direction = (targetObject.transform.position - transform.position).normalized;
        }
        rigidbody.velocity = direction * moveSpeed;
    }

    public void SetIsTakePlayerState(bool b)
    {
        isTakePlayer = b;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidbody.velocity = riseDirection * moveSpeed;
            collision.rigidbody.velocity = rigidbody.velocity;
        }
    }
}
