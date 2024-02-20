using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(5001);
    }

    public void SwitchState<T>()
    {
        Node newState = null;

        foreach (Node state in states)
        {
            if (state is T)
            {
                newState = state;
            }
        }

        // Not a valid state
        if (newState == null) { return; }

        // Disable Current State
        currentState.Notification(5002);

        // Set and enable Current State
        currentState = newState;
        currentState.Notification(5001);
    }
}
