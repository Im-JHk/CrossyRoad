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

    public override void InitializeState(Vector3 position, DirectionType direction, float rotateAngle)
    {
        rigidbody = GetComponent<Rigidbody>();
        moveSpeed = Random.Range(0.01f, 0.03f);
        transform.position = position;
        moveDirectionAngle = rotateAngle;
        moveDirection = direction;
        transform.Rotate(Vector3.up * rotateAngle);

        if (moveDirection == DirectionType.Left)
        {
            rigidbody.velocity = Vector3.left * moveSpeed;
        }
        else if (moveDirection == DirectionType.Right)
        {
            rigidbody.velocity = Vector3.right * moveSpeed;
        }
    }

    public void Move()
    {
        if(moveDirection == DirectionType.Left)
        {
            rigidbody.velocity = Vector3.left * moveSpeed;
        }
        else if(moveDirection == DirectionType.Right)
        {
            rigidbody.velocity = Vector3.right * moveSpeed;
        }
        
        transform.Translate(Vector3.forward * moveSpeed);
    }
}
