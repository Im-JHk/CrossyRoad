using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCheckColliders : MonoBehaviour, IFollowMovable
{
    public enum FollowCollider
    {
        Deadblock = 0,
        DeadFloor,
        OutBlock,
        AttackBirdOutBlock
    }

    [SerializeField]
    private Dictionary<FollowCollider, GameObject> dictionaryColliders;
    private GameObject alwaysFollowColliders;
    private readonly float maxMoveTime = 0.1f;

    private void Awake()
    {
        dictionaryColliders = new Dictionary<FollowCollider, GameObject>();
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
}
