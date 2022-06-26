using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller = null;

    private Vector3 direction;
    private bool left = false;
    private bool right = false;
    private bool up = false;
    private bool down = false;
    private bool isReady = false;
    private bool isJump = false;

    public Vector3 Direction { get { return direction; } private set { direction = value; } }
    public float Horizontal { get { return horizontal; } private set { horizontal = value; } }
    public float Vertical { get { return vertical; } private set { vertical = value; } }
    public bool IsJump { get { return isJump; } private set { isJump = value; } }

    void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        InputControl();
    }

    public void InputControl()
    {
        left = Input.GetButtonUp("Left");
        right = Input.GetButtonUp("Right");
        up = Input.GetButtonUp("Up");
        down = Input.GetButtonUp("Down");
        isJump = Input.GetButton("Jump");
        if(Input.GetButtonDown("Left") || Input.GetButtonDown("Right") || Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
        {
            isReady = true;
        }
        else
        {
            isReady = false;
        }

        //direction = new Vector3(horizontal, 0, vertical);
    }
}
