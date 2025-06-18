using UnityEngine;

public interface IJump
{
    bool IsGrounded { get; }
    float JumpForce { get; }
    void Jump(float jumpForce);
    void UpdateJumpState();
} 