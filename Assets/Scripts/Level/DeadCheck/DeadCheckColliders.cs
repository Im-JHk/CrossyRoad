using System.Collections;
using UnityEngine;

public class DeadCheckColliders : MonoBehaviour, IFollowMovable
{
    [SerializeField]
    private GameObject[] colliders;
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
}
