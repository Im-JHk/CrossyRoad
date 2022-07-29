using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour, ICyclicMovable, IFollowMovable
{
    public enum CameraFollowTarget
    {
        Player = 0,
        DeadBlock,
        AttackBird
    }

    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private Transform playerTransform = null;
    [SerializeField]
    private Transform deadBlockTransform = null;
    [SerializeField]
    private Transform attackBirdTransform = null;

    private Vector3 cameraBaseOffset = new Vector3(2.5f, 10f, -2.5f);
    private Vector3 cameraBaseRotation = new Vector3(60f, -30f, 0f);
    private CameraFollowTarget cameraFollowTarget;

    private readonly float maxMoveTime = 0.2f;

    public Camera MainCamera { get { return mainCamera; } private set { mainCamera = value; } }

    void Start()
    {
        cameraFollowTarget = CameraFollowTarget.Player;
        mainCamera.transform.eulerAngles = cameraBaseRotation;
        mainCamera.transform.position = playerTransform.position + cameraBaseOffset;
    }

    public void Move()
    {
        mainCamera.transform.position = mainCamera.transform.position + Vector3.forward * GameManager.Instance.MoveSpeed;
    }

    public void FollowTarget(Vector3 moveDistance, CameraFollowTarget targetType)
    {
        switch (targetType)
        {
            case CameraFollowTarget.Player:
                StartCoroutine(FollowMove(playerTransform, moveDistance));
                break;
            case CameraFollowTarget.DeadBlock:
                StartCoroutine(FollowMove(deadBlockTransform, moveDistance));
                break;
        }
    }

    private IEnumerator FollowMove(Transform target, Vector3 moveDistance)
    {
        float elapsedMoveTime = 0.0f;
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = mainCamera.transform.position + moveDistance;

        while (elapsedMoveTime < maxMoveTime)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedMoveTime / maxMoveTime);
            elapsedMoveTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = targetPosition;

        yield break;
    }
}
