using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBirdBlock : MonoBehaviour
{
    private readonly float maxMoveTime = 0.1f;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackBird"))
        {
            print("bird coll block");
            GameManager.Instance.GameOver();
        }
    }
}
