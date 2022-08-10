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
    private float remainShakeTime = 0;

    private readonly float maxMoveTime = 0.2f;

    public Camera MainCamera { get { return mainCamera; } private set { mainCamera = value; } }

    void Start()
    {
        cameraFollowTarget = CameraFollowTarget.Player;
        mainCamera.transform.eulerAngles = cameraBaseRotation;
        mainCamera.transform.position = playerTransform.position + cameraBaseOffset;
    }

    public void ShakeCamera(float time, float shakePower)
    {
        remainShakeTime = time;
        StartCoroutine("ShakeByRotation", shakePower);
    }

    private IEnumerator ShakeByRotation(float shakePower)
    {
        Vector3 originRotation = mainCamera.transform.eulerAngles;

        while(remainShakeTime > 0 && GameManager.Instance.GetGameState == GameManager.GameState.Play)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            mainCamera.transform.rotation = Quaternion.Euler(originRotation + new Vector3(x, y, z) * shakePower);

            remainShakeTime -= Time.deltaTime;

            yield return null;
        }
        mainCamera.transform.rotation = Quaternion.Euler(originRotation);
        remainShakeTime = 0f;

        yield break;
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

    public void FollowPlayer()
    {
        mainCamera.transform.position = playerTransform.position + cameraBaseOffset;
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
