using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private Timer cooldownTimerNode;
    [Export] private float speed = 10;
    [Export] private PackedScene bombScene;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;
        CanTransition = () => cooldownTimerNode.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        
        if (characterNode.Velocity == Vector3.Zero)
        {
            characterNode.Velocity = characterNode.Sprite3DNode.FlipH ?
                Vector3.Left :
                Vector3.Right;
        }
        
        characterNode.Velocity *= speed;
        dashTimerNode.Start();

        Node3D bomb = bombScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = characterNode.GlobalPosition;
    }

    private void HandleDashTimeout()
    {
        cooldownTimerNode.Start();
        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}
