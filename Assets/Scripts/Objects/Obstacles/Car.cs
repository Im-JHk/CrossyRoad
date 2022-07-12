using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacle, IMovable
{
    private float moveSpeed;

    private void FixedUpdate()
    {
        Move();
    }

    public override void InitializeState(Vector3 position)
    {
        moveSpeed = Random.Range(1f, 3f);
        transform.position = position;
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed);
    }
}
