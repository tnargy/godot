using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{

    [Export] public Timer timerNode;

    private CharacterBody3D target;

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        target = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .First() as CharacterBody3D;

        timerNode.Timeout += HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        timerNode.Timeout -= HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }

    protected void HandleAttackAreaBodyEntered(Node3D body) 
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }
    
    private void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }
}
