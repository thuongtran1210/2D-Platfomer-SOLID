using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private float horizontalInput;
    private bool jumpPressed;
    public float HorizontalInput => horizontalInput;

    public bool JumpPressed => jumpPressed;
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
}
