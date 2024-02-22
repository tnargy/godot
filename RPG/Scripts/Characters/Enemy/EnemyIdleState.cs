using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
