using Godot;
using System;

public abstract partial class PlayerState: CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }
}