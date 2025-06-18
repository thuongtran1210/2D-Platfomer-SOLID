using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Abstract input handler that can be extended for different input systems.
/// Follows Strategy Pattern and Open/Closed Principle.
/// </summary>
public abstract class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    [Header("Input Configuration")]
    [SerializeField] protected bool enableInputLogging = false;
    
    protected float horizontalInput;
    protected bool jumpPressedThisFrame;
    protected bool attackPressedThisFrame;
    protected bool skill1PressedThisFrame;
    protected bool skill2PressedThisFrame;
    protected bool skill3PressedThisFrame;

    #region IPlayerInput Implementation
    public float HorizontalInput => horizontalInput;
    public bool JumpPressed => jumpPressedThisFrame;
    public bool AttackPressed => attackPressedThisFrame;
    public bool Skill1Pressed => skill1PressedThisFrame;
    public bool Skill2Pressed => skill2PressedThisFrame;
    public bool Skill3Pressed => skill3PressedThisFrame;
    #endregion

    protected virtual void Awake()
    {
        InitializeInput();
    }

    protected virtual void OnEnable()
    {
        EnableInput();
    }

    protected virtual void OnDisable()
    {
        DisableInput();
    }

    protected abstract void InitializeInput();
    protected abstract void EnableInput();
    protected abstract void DisableInput();

    public virtual void ResetPressState()
    {
        jumpPressedThisFrame = false;
        attackPressedThisFrame = false;
        skill1PressedThisFrame = false;
        skill2PressedThisFrame = false;
        skill3PressedThisFrame = false;
    }

    protected void LogInput(string action, object value = null)
    {
        if (enableInputLogging)
        {
            string message = value != null ? $"{action}: {value}" : action;
            Debug.Log($"[InputHandler] {message}");
        }
    }

    protected void SetHorizontalInput(float value)
    {
        horizontalInput = value;
        LogInput("Horizontal Input", value);
    }

    protected void SetJumpPressed(bool pressed)
    {
        jumpPressedThisFrame = pressed;
        if (pressed) LogInput("Jump Pressed");
    }

    protected void SetAttackPressed(bool pressed)
    {
        attackPressedThisFrame = pressed;
        if (pressed) LogInput("Attack Pressed");
    }

    protected void SetSkillPressed(int skillIndex, bool pressed)
    {
        switch (skillIndex)
        {
            case 0:
                skill1PressedThisFrame = pressed;
                if (pressed) LogInput("Skill 1 Pressed");
                break;
            case 1:
                skill2PressedThisFrame = pressed;
                if (pressed) LogInput("Skill 2 Pressed");
                break;
            case 2:
                skill3PressedThisFrame = pressed;
                if (pressed) LogInput("Skill 3 Pressed");
                break;
        }
    }
} 