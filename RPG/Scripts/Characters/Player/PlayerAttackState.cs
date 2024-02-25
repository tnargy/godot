using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] Timer comboTimerNode;

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
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        characterNode.ToggleHitBox(false);
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
