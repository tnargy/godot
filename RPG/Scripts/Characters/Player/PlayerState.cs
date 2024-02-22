using Godot;

public abstract partial class PlayerState: CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        characterNode = GetOwner<Player>();
    }
}