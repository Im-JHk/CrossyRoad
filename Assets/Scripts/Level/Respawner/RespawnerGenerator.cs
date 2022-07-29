using UnityEngine;

public class RespawnerGenerator : MonoBehaviour
{
    public GameObject GenerateRespawner(Vector3 position, LevelManager.ObstacleType obstacleType, LevelManager.ObjectPoolTypeList objectType, int lineIndex)
    {
        GameObject newRespawner = ObjectPoolManager.Instance.ObjectPoolDictionary[LevelManager.ObjectPoolTypeList.Respawner].BorrowObject();
        newRespawner.GetComponent<Respawner>().InitializeState(position, obstacleType, objectType, lineIndex);
        newRespawner.transform.position = position;

        return newRespawner;
    }

    public GameObject GenerateRespawner(Vector3 position, LevelManager.ObstacleType obstacleType, LevelManager.ObjectPoolTypeList objectType, DirectionType direction, float rotateAngle, int lineIndex)
    {
        GameObject newRespawner = ObjectPoolManager.Instance.ObjectPoolDictionary[LevelManager.ObjectPoolTypeList.Respawner].BorrowObject();
        newRespawner.GetComponent<Respawner>().InitializeState(position, obstacleType, objectType, direction, rotateAngle, lineIndex);
        newRespawner.transform.position = position;

        return newRespawner;
    }

    public GameObject GenerateDeactivater(Vector3 position, LevelManager.ObjectPoolTypeList objectType)
    {
        GameObject newDeactivater = ObjectPoolManager.Instance.ObjectPoolDictionary[LevelManager.ObjectPoolTypeList.Deactivater].BorrowObject();
        newDeactivater.GetComponent<Deactivater>().InitializeState(position, objectType);
        newDeactivater.transform.position = position;

        return newDeactivater;
    }

    public GameObject GenerateDeactivater(Vector3 position, LevelManager.ObjectPoolTypeList objectType, float rotateAngle)
    {
        GameObject newDeactivater = ObjectPoolManager.Instance.ObjectPoolDictionary[LevelManager.ObjectPoolTypeList.Deactivater].BorrowObject();
        newDeactivater.GetComponent<Deactivater>().InitializeState(position, objectType, rotateAngle);
        newDeactivater.transform.position = position;

        return newDeactivater;
    }
}
