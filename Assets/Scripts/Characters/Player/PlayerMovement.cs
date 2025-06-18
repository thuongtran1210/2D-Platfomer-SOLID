using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement, IJump
{
    [Header("Movement Settings")]
    [SerializeField] private float defaultJumpForce = 10f;
    
    private Rigidbody2D rb;
    private bool isGrounded;

    #region IJump Implementation
    bool IJump.IsGrounded => isGrounded;
    
    public float JumpForce => defaultJumpForce;
    
    public void Jump(float jumpForce)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    
    public void UpdateJumpState()
    {
        // Update jump state logic if needed
        // For now, this is handled by collision detection
    }
    #endregion

    #region IMovement Implementation
    public void Move(float direction, float speed)
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
