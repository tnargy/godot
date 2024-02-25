using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;

        Node3D target = characterNode.AttackAreaNode
            .GetOverlappingBodies()
            .First();
        targetPosition = target.GlobalPosition;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitBox(false);
        Node3D target = characterNode.AttackAreaNode
            .GetOverlappingBodies()
            .FirstOrDefault();
        if (target == null)
        {
            target = characterNode.ChaseAreaNode
                .GetOverlappingBodies()
                .FirstOrDefault();
            if (target == null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;    
            }
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.Sprite3DNode.FlipH = direction.X < 0;
    }

    public void PerformHit()
    {
        characterNode.ToggleHitBox(true);
        characterNode.HitBoxNode.GlobalPosition = targetPosition;
    }
}
