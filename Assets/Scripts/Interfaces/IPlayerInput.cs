

using UnityEngine;

public interface IPlayerInput 
{
    float HorizontalInput { get; }
    bool JumpPressed { get; }
    bool AttackPressed { get; }
    bool Skill1Pressed { get; }
    bool Skill2Pressed { get; }
    bool Skill3Pressed { get; }
}
