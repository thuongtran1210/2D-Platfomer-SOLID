using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Refactored PlayerInput using the new Input System.
/// Extends PlayerInputHandler for better abstraction.
/// </summary>
public class PlayerInputSystemRefactored : PlayerInputHandler
{
    private PlayerControls playerControls;

    protected override void InitializeInput()
    {
        playerControls = new PlayerControls();
        LogInput("Input System Initialized");
    }

    protected override void EnableInput()
    {
        playerControls.Enable();
        SubscribeToEvents();
        LogInput("Input System Enabled");
    }

    protected override void DisableInput()
    {
        UnsubscribeFromEvents();
        playerControls.Disable();
        LogInput("Input System Disabled");
    }

    private void SubscribeToEvents()
    {
        // Movement
        playerControls.Player.Move.performed += OnMovePerformed;
        playerControls.Player.Move.canceled += OnMoveCanceled;
        
        // Actions
        playerControls.Player.Jump.performed += OnJumpPerformed;
        playerControls.Player.Attack.performed += OnAttackPerformed;
        playerControls.Player.Skill1.performed += OnSkill1Performed;
        playerControls.Player.Skill2.performed += OnSkill2Performed;
        playerControls.Player.Skill3.performed += OnSkill3Performed;
    }

    private void UnsubscribeFromEvents()
    {
        // Movement
        playerControls.Player.Move.performed -= OnMovePerformed;
        playerControls.Player.Move.canceled -= OnMoveCanceled;
        
        // Actions
        playerControls.Player.Jump.performed -= OnJumpPerformed;
        playerControls.Player.Attack.performed -= OnAttackPerformed;
        playerControls.Player.Skill1.performed -= OnSkill1Performed;
        playerControls.Player.Skill2.performed -= OnSkill2Performed;
        playerControls.Player.Skill3.performed -= OnSkill3Performed;
    }

    #region Event Handlers
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        SetHorizontalInput(moveVector.x);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        SetHorizontalInput(0f);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        SetJumpPressed(true);
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        SetAttackPressed(true);
    }

    private void OnSkill1Performed(InputAction.CallbackContext context)
    {
        SetSkillPressed(0, true);
    }

    private void OnSkill2Performed(InputAction.CallbackContext context)
    {
        SetSkillPressed(1, true);
    }

    private void OnSkill3Performed(InputAction.CallbackContext context)
    {
        SetSkillPressed(2, true);
    }
    #endregion

    private void OnDestroy()
    {
        playerControls?.Dispose();
    }
} 