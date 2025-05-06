public interface IMovement 
{
    void Move(float direction, float speed);

}
public interface IJump
{
    void Jump(float jumpForce);
}
public interface IDamageable
{
    void TakeDamage(float damage);
    bool IsAlive {  get; }
}
