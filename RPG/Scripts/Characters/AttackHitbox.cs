using Godot;
using System;

public partial class AttackHitbox : Area3D, IHitbox
{
    public bool CanStun() => false;

    public float GetDamage()
    {
        return GetOwner<Character>().GetStatResource(Stat.Strength)
            .StatValue;
    }
}
