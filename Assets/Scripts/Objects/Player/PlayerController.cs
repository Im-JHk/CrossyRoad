using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller = null;

    private bool leftKeyDown = false;
    private bool rightKeyDown = false;
    private bool upKeyDown = false;
    private bool downKeyDown = false;

    private bool leftKeyUp = false;
    private bool rightKeyUp = false;
    private bool upKeyUp = false;
    private bool downKeyUp = false;

    private bool jumpKeyDown = false;

    #region properties
    public bool LeftKeyDown { get { return leftKeyDown; } private set { leftKeyDown = value; } }
    public bool RightKeyDown { get { return rightKeyDown; } private set { rightKeyDown = value; } }
    public bool UpKeyDown { get { return upKeyDown; } private set { upKeyDown = value; } }
    public bool DownKeyDown { get { return downKeyDown; } private set { downKeyDown = value; } }

    public bool LeftKeyUp { get { return leftKeyUp; } private set { leftKeyUp = value; } }
    public bool RightKeyUp { get { return rightKeyUp; } private set { rightKeyUp = value; } }
    public bool UpKeyUp { get { return upKeyUp; } private set { upKeyUp = value; } }
    public bool DownKeyUp { get { return downKeyUp; } private set { downKeyUp = value; } }

    public bool JumpKeyDown { get { return jumpKeyDown; } private set { jumpKeyDown = value; } }
    #endregion

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
        leftKeyDown = Input.GetButtonDown("Left");
        rightKeyDown = Input.GetButtonDown("Right");
        upKeyDown = Input.GetButtonDown("Up");
        downKeyDown = Input.GetButtonDown("Down");

        leftKeyUp = Input.GetButtonUp("Left");
        rightKeyUp = Input.GetButtonUp("Right");
        upKeyUp = Input.GetButtonUp("Up");
        downKeyUp = Input.GetButtonUp("Down");

        jumpKeyDown = Input.GetButtonDown("Jump");
    }
}
