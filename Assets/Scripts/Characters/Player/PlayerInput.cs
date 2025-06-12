using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private PlayerControls playerControls;

    private float horizontalInput;
    private bool jumpPressedThisFrame;
    private bool attackPressedThisFrame;
    private bool skill1PressedThisFrame;
    private bool skill2PressedThisFrame;
    private bool skill3PressedThisFrame;

    // Property
    public float HorizontalInput => horizontalInput;
    public bool JumpPressed => jumpPressedThisFrame;

    public bool AttackPressed => attackPressedThisFrame;

    public bool Skill1Pressed => skill1PressedThisFrame;

    public bool Skill2Pressed => skill2PressedThisFrame;

    public bool Skill3Pressed => skill3PressedThisFrame;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();

        //Add 
        playerControls.Player.Move.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>().x;
        playerControls.Player.Move.canceled += ctx => horizontalInput = 0f;
        playerControls.Player.Jump.performed += ctx => jumpPressedThisFrame = true;
        playerControls.Player.Attack.performed += ctx => attackPressedThisFrame = true;
        playerControls.Player.Skill1.performed += ctx => skill1PressedThisFrame = true;
        playerControls.Player.Skill2.performed += ctx => skill2PressedThisFrame = true;
        playerControls.Player.Skill3.performed += ctx => skill3PressedThisFrame = true;


    }
    private void OnDisable()
    {
        playerControls.Disable(); 


        playerControls.Player.Move.performed -= ctx => horizontalInput = ctx.ReadValue<Vector2>().x;
        playerControls.Player.Move.canceled -= ctx => horizontalInput = 0f;

        playerControls.Player.Jump.performed -= ctx => jumpPressedThisFrame = true;
        playerControls.Player.Attack.performed -= ctx => attackPressedThisFrame = true;
        playerControls.Player.Skill1.performed -= ctx => skill1PressedThisFrame = true;
        playerControls.Player.Skill2.performed -= ctx => skill2PressedThisFrame = true;
        playerControls.Player.Skill2.performed -= ctx => skill2PressedThisFrame = true;
    }

    public void ResetPressState()
    {
        jumpPressedThisFrame = false;
        attackPressedThisFrame = false;
        skill1PressedThisFrame = false;
        skill2PressedThisFrame = false;
        skill3PressedThisFrame = false;
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        horizontalInput = moveVector.x;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        horizontalInput = 0f;
    }
}
