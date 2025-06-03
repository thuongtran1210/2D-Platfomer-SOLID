using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement),typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour, IEntity
{
    [Header("Component references")]
    private IPlayerInput input;
    private IMovement movement;
    private IDamageable damageable;
    private IJump jump;
    private IHealable healable;
    private StateMachine stateMachine;
    private Transform _transform;
    
    // Property
    public IPlayerInput Input => input;
    public IMovement Movement => movement;
    public IJump Jump => jump;
    public IDamageable Damageable => damageable;
    public IHealable Healable => healable;
    public StateMachine StateMachine => stateMachine;
    public Transform EntityTransform => _transform;

  


    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        damageable = GetComponent<IDamageable>();
        jump = GetComponent<IJump>();
        healable = GetComponent<IHealable>();
        _transform = transform;

        stateMachine = new StateMachine(this);
        if (input == null || movement == null || damageable == null || healable == null)
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
