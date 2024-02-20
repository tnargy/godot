using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer animPlayerNode;
    [Export] public Sprite3D sprite3DNode;
    [Export] public StateMachine stateMachineNode;

    public Vector2 direction = new();

    public override void _Ready() 
    {
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, 
            GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD    
        );
    }

    public void Flip()
    {
        bool isNotMovingHoriz = Velocity.X == 0;
        if (isNotMovingHoriz) { return; }

        bool isMovingLeft = Velocity.X < 0;
        sprite3DNode.FlipH = isMovingLeft;
    }
}