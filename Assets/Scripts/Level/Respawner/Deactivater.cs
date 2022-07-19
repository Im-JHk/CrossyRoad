using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivater : MonoBehaviour
{
    private Vector3 worldPosition;
    private ObjectPrefabType objectType;
    private float returnRotateAngle = 0f;

    #region properties
    public Vector3 WorldPosition { get { return worldPosition; } private set { worldPosition = value; } }
    #endregion

    public Deactivater(GameObject prefeb, Vector3 position)
    {
        worldPosition = position;
    }

    public void InitializeState(Vector3 position, ObjectPrefabType objectType)
    {
        this.worldPosition = position;
        this.objectType = objectType;
    }

    public void InitializeState(Vector3 position, ObjectPrefabType objectType, float rotateAngle)
    {
        this.worldPosition = position;
        this.objectType = objectType;
        this.returnRotateAngle = rotateAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackObstacle"))
        {
            other.gameObject.transform.Rotate(Vector3.up * returnRotateAngle);
            ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].ReturnObject(other.gameObject);
        }
        else if (other.CompareTag("PropObstacle"))
        {
            print("prop");
            other.gameObject.transform.Rotate(Vector3.up * returnRotateAngle);
            ObjectPoolManager.Instance.ObjectPoolDictionary[objectType].ReturnObject(other.gameObject);
        }
    }
}
