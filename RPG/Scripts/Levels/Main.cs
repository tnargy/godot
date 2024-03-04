using Godot;
using System;

public partial class Main : Node3D
{
    [Export] public int currentLevel { get; private set; }
    [Export] public int maxLevel { get; private set; }
    private PackedScene level;

    public override void _Ready()
    {
        GetTree().Paused = true;
        LoadLevel();
        GameEvents.OnNextLevel += HandleNextLevel;
        GameEvents.OnRestartLevel += HandleRestart;
    }

    private void HandleRestart()
    {
        GetTree().Quit();
    }


    private void HandleNextLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            LoadLevel();
        }
        else GetTree().Quit();
    }


    public void LoadLevel()
    {
        level = (PackedScene)ResourceLoader.Load("res://Scenes/Levels/level_" + currentLevel + ".tscn");
        Node3D levelNode = level.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(levelNode);
    }
}
