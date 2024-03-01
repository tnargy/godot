using Godot;
using System;

public partial class MiniMap : Sprite3D
{
    [Export] private Node3D target;

    public override void _PhysicsProcess(double delta)
    {
        Vector3 pos = target.GlobalPosition;
        pos.Y += 15;
        GlobalPosition = pos;
    }
}
