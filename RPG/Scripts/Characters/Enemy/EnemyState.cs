using Godot;

public abstract partial class EnemyState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        characterNode = GetOwner<Enemy>();
    }
}
