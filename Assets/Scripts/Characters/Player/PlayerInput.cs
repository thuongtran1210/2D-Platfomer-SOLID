using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private float horizontalInput;
    private bool jumpPressed;
    private bool attackPressed;
    private bool skill1Pressed;
    private bool skill2Pressed;
    private bool skill3Pressed;

    // Property
    public float HorizontalInput => horizontalInput;
    public bool JumpPressed => jumpPressed;

    public bool AttackPressed => attackPressed;

    public bool Skill1Pressed => skill1Pressed;

    public bool Skill2Pressed => skill2Pressed;

    public bool Skill3Pressed => skill3Pressed;

    private void Update()
    {
        horizontalInput =   Input.GetAxisRaw("Horizontal");
        jumpPressed     =   Input.GetKeyDown(KeyCode.Space);
        attackPressed   =   Input.GetKeyDown(KeyCode.J); 
        skill1Pressed   =   Input.GetKeyDown(KeyCode.K); 
        skill2Pressed   =   Input.GetKeyDown(KeyCode.L); 
        skill3Pressed   =   Input.GetKeyDown(KeyCode.I); 
    }
}
