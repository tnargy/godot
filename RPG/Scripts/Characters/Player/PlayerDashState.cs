using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player characterNode;
    [Export] private Timer dashTimerNode;
    [Export] private float speed = 10;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        dashTimerNode.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            characterNode.animPlayerNode.Play(GameConstants.ANIM_DASH);
            characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
            
            if (characterNode.Velocity == Vector3.Zero)
            {
                characterNode.Velocity = characterNode.sprite3DNode.FlipH ?
                    Vector3.Left :
                    Vector3.Right;
            }
            
            characterNode.Velocity *= speed;
            dashTimerNode.Start();
            SetPhysicsProcess(true);
        }
    }

    private void HandleDashTimeout()
    {
        SetPhysicsProcess(false);
        characterNode.Velocity = Vector3.Zero;
        characterNode.stateMachineNode.SwitchState<PlayerIdleState>();
    }
}
