using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IEntity
{
    [Header("Component references")]
    private IPlayerInput input;
    private IMovement movement;
    private IDamageable damageable;
    private IJump jump;
    private StateMachine stateMachine;
    
    // Property
    public IPlayerInput Input => input;
    public IMovement Movement => movement;
    public IJump Jump => jump;
    public IDamageable Damageable => damageable;
    public StateMachine StateMachine => stateMachine;


    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJump>();

        stateMachine = new StateMachine(this);
        if (input == null || movement == null)
        {
            Debug.LogError("Missing component requiment PlayerController!");
            enabled = false;
            return;
        }
        stateMachine.ChangeState(new PlayerIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        Debug.Log($"Current state {stateMachine.CurrentState.GetType().Name}");
    }
     void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
