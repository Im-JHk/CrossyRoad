using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBlock : MonoBehaviour, IMovable
{
    private BoxCollider boxCollider = null;
    
    private static readonly float maxMoveTime = 0.1f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(LinearLineGenerator.lineWidth, 1f, 5f);
    }

    private void Start()
    {
        transform.position = new Vector3(0f, 1f, 0f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver(true);
            GameManager.Instance.OnDieFromDeadBlock();
        }
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * GameManager.Instance.MoveSpeed);
    }

    public void FollowTargetMove(Vector3 moveDistance)
    {
        StartCoroutine(FollowMove(moveDistance));
    }

    private IEnumerator FollowMove(Vector3 moveDistance)
    {
        float elapsedMoveTime = 0.0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + moveDistance;

        while (elapsedMoveTime < maxMoveTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedMoveTime / maxMoveTime);
            elapsedMoveTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        yield break;
    }

    private void OnDrawGizmos()
    {
        
    }
}
