
using UnityEngine;

public interface IEntity
{
    Transform EntityTransform { get; }
    IMovement Movement { get; }
    IJump Jump { get; }
    IDamageable Damageable { get; }
    IHealable Healable { get; }
}
