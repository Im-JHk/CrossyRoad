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
    private float moveSpeed = 10f;
    private float height = 2f;
    private float activePositionZ = 10f;
    private bool isTakePlayer = false;

    private readonly Vector3 riseDirection = new Vector3(0f, 1f, -2f);

    public bool IsTakePlayer { get { return isTakePlayer; } private set { isTakePlayer = value; } }

    private void Awake()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetInitialize()
    {
        if (targetObject != null)
        {
            transform.position = targetObject.transform.position + new Vector3(0f, height, activePositionZ);
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
            print("rise: " + riseDirection.normalized);
            rigidbody.velocity = riseDirection.normalized * moveSpeed;
            collision.rigidbody.useGravity = false;
            collision.rigidbody.velocity = rigidbody.velocity;
        }
    }
}
