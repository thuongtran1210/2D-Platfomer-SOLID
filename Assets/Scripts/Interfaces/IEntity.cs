
public interface IEntity
{
    IMovement Movement { get; }
    IJump Jump { get; }
    IDamageable Damageable { get; }
}
