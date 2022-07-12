using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivater : MonoBehaviour
{
    private Vector3 worldPosition;

    #region properties
    public Vector3 WorldPosition { get { return worldPosition; } private set { worldPosition = value; } }
    #endregion

    public Deactivater(GameObject prefeb, Vector3 position)
    {
        worldPosition = position;
    }

    public void InitializeState(Vector3 position)
    {
        worldPosition = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackObstacle"))
        {
            print("충돌 -> 비활성화");
            print(other.gameObject);
            print(other);
        }
    }
}
