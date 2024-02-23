using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    private int pointIndex;

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;
        GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
    }

    private void HandleNavigationFinished()
    {
        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);
        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }

}
