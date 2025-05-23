public class StateMachine 
{
    private IState currentState;
    private IEntity entity; 
    //Entity la cac thuc the co State
    // + Character
    // + Enemy
    public StateMachine(IEntity entity)
    {
        this.entity = entity;
    }
    public void ChangeState(IState newState)
    {
        currentState?.Exit(entity);
        currentState = newState;
        currentState?.Enter(entity);
    }
    public void Update() => currentState?.Update(entity);
    public void FixedUpdate() => currentState?.FixedUpdate(entity);
    public IState CurrentState => currentState;

}
