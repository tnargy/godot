using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer animPlayerNode;
    [Export] public Sprite3D sprite3DNode;

    private Vector2 direction = new();
    
    public override void _Ready() 
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= 5;

        MoveAndSlide();

        Flip();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, 
            GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD    
        );
    }

    private void Flip()
    {
        bool isNotMovingHoriz = Velocity.X == 0;
        if (isNotMovingHoriz) { return; }

        bool isMovingLeft = Velocity.X < 0;
        sprite3DNode.FlipH = isMovingLeft;
    }
}