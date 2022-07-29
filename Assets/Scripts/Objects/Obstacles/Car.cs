using UnityEngine;

public class Car : Obstacle
{
    private Rigidbody rigidbody;
    private DirectionType moveDirection;
    private float moveSpeed;
    private float moveDirectionAngle;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    public override void InitializeState(Vector3 position, DirectionType direction, float rotateAngle, float moveSpeed)
    {
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
}
