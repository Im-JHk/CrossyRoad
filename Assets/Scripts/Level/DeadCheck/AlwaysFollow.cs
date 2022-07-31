using System.Collections;
using UnityEngine;

public class AlwaysFollow : MonoBehaviour
{
    public void MatchPlayerPositionZ(float playerPositionZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPositionZ);
    }
}
