using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Obstacle, IMovable
{
    private Rigidbody rigidbody;
    private DirectionType moveDirection;
    private float moveSpeed;
    private float moveDirectionAngle;

    private void FixedUpdate()
    {
        Move();
    }

    public override void InitializeState(Vector3 position, DirectionType direction, float rotateAngle, float moveSpeed)
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.moveSpeed = moveSpeed;
        this.transform.position = position - new Vector3(0f, 0.7f, 0f);
        this.moveDirectionAngle = rotateAngle;
        this.moveDirection = direction;
        this.transform.Rotate(Vector3.up * rotateAngle);
    }

    public void Move()
    {
        this.transform.Translate(Vector3.forward * moveSpeed);
    }
}
