using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacle, IMovable
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
        this.transform.position = position - new Vector3(0f, 0.5f, 0f);
        this.moveDirectionAngle = rotateAngle;
        this.moveDirection = direction;
        this.transform.Rotate(Vector3.up * rotateAngle);

        if (moveDirection == DirectionType.Left)
        {
            this.rigidbody.velocity = Vector3.left * moveSpeed;
        }
        else if (moveDirection == DirectionType.Right)
        {
            this.rigidbody.velocity = Vector3.right * moveSpeed;
        }
    }

    public void Move()
    {
        if(moveDirection == DirectionType.Left)
        {
            this.rigidbody.velocity = Vector3.left * moveSpeed;
        }
        else if(moveDirection == DirectionType.Right)
        {
            this.rigidbody.velocity = Vector3.right * moveSpeed;
        }

        this.transform.Translate(Vector3.forward * moveSpeed);
    }
}
