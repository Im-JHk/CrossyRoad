using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerObject = null;
    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } private set { isAlive = value; } }

    void Awake()
    {

    }

    void Update()
    {
        
    }

    public void InitializePlayer()
    {
        isAlive = true;
        transform.position = new Vector3(0f, 0f, 0f);
    }

    public void OnChangeAliveState(bool b)
    {
        isAlive = b;
        GameManager.Instance.GameOver(true);
    }
}
