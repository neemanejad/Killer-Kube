using Godot;
using Nova.Scripts;
using Nova.Scripts.Character;
using System;

public partial class Enemy : Area2D, ICharacter
{
    [Export]
    public int Speed { get; set; }
    public Node Target { get; set; }
    [Export]
    public PackedScene WeaponScene { get; set; }
    public BaseWeapon Weapon { get; set; }
    public Marker2D WeaponLocation { get; set; }
    public HealthBar HealthBar { get; set; }
    public Marker2D HealthBarLocation { get; set; }
    [Export]
    public double HealthPoints { get; set; }
    public Timer HitTimer { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        HitTimer = GetNode<Timer>("HitTimer");
        HealthBar = GetNode<HealthBar>("HealthBar");
        HealthBar.Health = HealthPoints;
        HealthBar.MaxHealth = HealthPoints;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Globals.Target == null) return;

        GlobalPosition += (GlobalPosition.DirectionTo(Globals.Target.GlobalPosition) * (float)delta * Speed);
    }

    public void EnableAttack()
    {
        throw new NotImplementedException();
    }

    public void DisableAttack()
    {
        throw new NotImplementedException();
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area.IsInGroup("PlayerProjectile"))
        {
            var bullet = area.GetNode<Bullet>(".");
            HealthBar.RemoveHealthPoints(bullet.WeaponOfOrigin.WeaponDamage);
            area.QueueFree();
        }
    }

    public void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("Player") && HitTimer.IsStopped())
        {
            body.GetNode<HealthBar>("HealthBar").RemoveHealthPoints(5);
            Target = body;
            HitTimer.Start();
        }
    }

    public void OnBodyExited(Node body)
    {
        HitTimer.Stop();
    }

    public void OnHitTimerTimeout()
    {
        Target.GetNode<HealthBar>("HealthBar").RemoveHealthPoints(5);
    }

    public void PlayerLostAllHealth()
    {
        QueueFree();
    }
}
