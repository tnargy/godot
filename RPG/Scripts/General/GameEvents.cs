using System;

public class GameEvents
{
    public static Action OnStartGame;

    public static void RaiseStartGame() => OnStartGame?.Invoke();
}