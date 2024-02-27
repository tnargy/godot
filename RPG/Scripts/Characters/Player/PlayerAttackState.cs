using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] Timer comboTimerNode;
    [Export] PackedScene lightningScene;

    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;

        characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitBoxNode.BodyEntered -= HandleBodyEntered;

        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        characterNode.ToggleHitBox(false);
    }
    
    private void HandleBodyEntered(Node3D body)
    {
        if (comboCounter != maxComboCount) { return; }
        Node3D lighting = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lighting);
        lighting.GlobalPosition = body.GlobalPosition;
    }

    public void PerformHit()
    {
        Vector3 newPosition = characterNode.Sprite3DNode.FlipH ?
            Vector3.Left :
            Vector3.Right;
        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;
        characterNode.HitBoxNode.Position = newPosition;

        characterNode.ToggleHitBox(true);
    }
}
