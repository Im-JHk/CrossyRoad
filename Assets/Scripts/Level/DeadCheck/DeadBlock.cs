using UnityEngine;
using System.Collections;

public class DeadBlock : MonoBehaviour, ICyclicMovable
{
    private BoxCollider boxCollider = null;
    private readonly float maxMoveTime = 0.1f;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.OutCollisionSound);
            GameManager.Instance.OnDieFromDeadBlock();
        }
    }
}
