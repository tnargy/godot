using Godot;
using System;

public partial class Camera : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionFromTarget;
    [Export] private Area3D areaNode;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
        areaNode.BodyEntered += HandleBodyEntered;
        areaNode.BodyExited += HandleBodyExited;
    }

    private void HandleBodyExited(Node3D body)
    {
        body.Visible = true;
    }


    private void HandleBodyEntered(Node3D body)
    {
        body.Visible = false;
    }


    private void HandleStartGame()
    {
        Reparent(target);
        Position = positionFromTarget;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }
}
