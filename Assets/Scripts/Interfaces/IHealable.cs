
public interface IHealable
{
    void Heal(float amount);
    float CurrentHealth { get; }
    float MaxHealth { get; }
}
