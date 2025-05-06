public interface IState 
{

    void Enter(IEntity entity);
    void Update(IEntity entity);
    void FixedUpdate(IEntity entity);
    void Exit(IEntity entity);
}
public interface IEntity
{
    IMovement Movement { get; }
    IJump Jump { get; }
    IDamageable Damageable { get; }
}