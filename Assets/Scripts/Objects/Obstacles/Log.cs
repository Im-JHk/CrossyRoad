using UnityEngine;

public class Log : Obstacle
{
    private Rigidbody rigidbody = null;
    private DirectionType moveDirection;
    private float moveSpeed;
    private float moveDirectionAngle;
    private bool isSetCollision = false;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    public override void InitializeState(Vector3 position, DirectionType direction, float rotateAngle, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.transform.position = position - new Vector3(0f, 0.6f, 0f);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.rigidbody.useGravity = false;
            isSetCollision = true;
            if (moveDirection == DirectionType.Left)
            {
                this.rigidbody.velocity = Vector3.left * moveSpeed;
            }
            else if (moveDirection == DirectionType.Right)
            {
                this.rigidbody.velocity = Vector3.right * moveSpeed;
            }
            collision.rigidbody.velocity = this.rigidbody.velocity;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.rigidbody.useGravity = true;
            collision.rigidbody.velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
