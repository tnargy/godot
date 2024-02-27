using Godot;
using System;
using System.Linq;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D Sprite3DNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area3D HurtBoxNode { get; private set; }
    [Export] public Area3D HitBoxNode { get; private set; }
    [Export] public CollisionShape3D HitBoxShapeNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();

    public override void _Ready()
    {
        base._Ready();
        HurtBoxNode.AreaEntered += HandleHurtBoxBodyEntered;
    }

    public void Flip()
    {
        bool isNotMovingHoriz = Velocity.X == 0;
        if (isNotMovingHoriz) { return; }

        bool isMovingLeft = Velocity.X < 0;
        Sprite3DNode.FlipH = isMovingLeft;
    }

    protected void HandleHurtBoxBodyEntered(Area3D area) 
    {
        if (area is not IHitbox hitbox) { return; }

        StatResource health = GetStatResource(Stat.Health);
        float damage = hitbox.GetDamage();
        health.StatValue -= damage;
        GD.Print(Name + ": " + health.StatValue);
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where(
            (element) => element.StatType == stat)
            .FirstOrDefault();
    }

    public void ToggleHitBox(bool flag)
    {
        HitBoxShapeNode.Disabled = !flag;
    }
}
