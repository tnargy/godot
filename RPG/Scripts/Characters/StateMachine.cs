using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        CharacterState newState = states.Where(
            (state) => state is T)
            .FirstOrDefault();

        // Not a valid state or repeating transition
        if (newState == null || currentState is T) { return; }
        if (!newState.CanTransition()) { return; }

        // Disable Current State
        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);

        // Set and enable Current State
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
