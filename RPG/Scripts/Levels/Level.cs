using Godot;
using System;

public partial class Level : Node3D
{
    [Export] private Node3D spawn;

    public override void _Ready()
    {
        GetTree().CurrentScene.GetNode<CharacterBody3D>("Player").Position = spawn.Position;
    }
}
