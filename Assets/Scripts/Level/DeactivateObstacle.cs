using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObstacle : MonoBehaviour
{
    private Vector3 worldPosition;

    #region properties
    public Vector3 WorldPosition { get { return worldPosition; } private set { worldPosition = value; } }
    #endregion

    public DeactivateObstacle(Vector3 position)
    {
        worldPosition = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackObstacle"))
        {
            print("충돌 -> 비활성화");
        }
    }
}
