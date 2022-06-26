using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController playerController = null;
    public Player player = null;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if(playerController.Horizontal != 0 || playerController.Vertical != 0)
        {
            Move();
        }
    }

    void Move()
    {

    }

    void Rotate()
    {

    }
}
