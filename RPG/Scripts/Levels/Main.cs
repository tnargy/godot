using Godot;
using System;

public partial class Main : Node3D
{
    [Export] public int levelCounter { get; private set; } = 1;
    private PackedScene level;

    public override void _Ready()
    {
        GetTree().Paused = true;
        LoadLevel(levelCounter);
    }

    public void LoadLevel(int levelCounter)
    {
        level = (PackedScene)ResourceLoader.Load("res://Scenes/Levels/level_" + levelCounter + ".tscn");
        Node3D levelNode = level.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(levelNode);
    }
}
