using Godot;
using Killer_Kube.Scripts;
using Nova.Scripts;
using Nova.Scripts.Enemies;
using System;
using System.Collections.Generic;

public partial class Enemy : Area2D, IEnemy
{
    [Signal]
    public delegate void EnemyLostAllHealthEventHandler(Enemy enemy);

    [Export]
    public float Speed { get; set; }
    public Node Target { get; set; }
    public int PointsForDeath { get; set; }
    [Export]
    public PackedScene WeaponScene { get; set; }
    public Weapon Weapon { get; set; }
    public Marker2D WeaponLocation { get; set; }
    public HealthBar HealthBar { get; set; }
    public Marker2D HealthBarLocation { get; set; }
    [Export]
    public double HealthPoints { get; set; }
    public Timer HitTimer { get; set; }
    public Dictionary<string, InventoryItem> InventoryItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

    public virtual void EnableAttack()
    {
        throw new NotImplementedException();
    }

    public virtual void DisableAttack()
    {
        throw new NotImplementedException();
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area.IsInGroup("PlayerProjectile"))
        {
            var bullet = area.GetNode<Bullet>(".");
            HealthBar.RemoveHealthPoints(bullet.Damage);
            var damageNumberScene = ResourceLoader.Load<PackedScene>("res://Scenes/DamageNumber.tscn");
            var damageNumber = damageNumberScene.Instantiate<DamageNumber>();
            damageNumber.IsCriticalDamage = bullet.IsCriticalDamage;
            damageNumber.Number = bullet.Damage;
            GetTree().Root.AddChild(damageNumber);
            damageNumber.GlobalPosition = GlobalPosition;

            bullet.QueueFree();
        }
    }

    public void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            body.GetNode<HealthBar>("HealthBar").RemoveHealthPoints(5);
            Target = body;
            HitTimer.Start();
        }
    }

    public void OnBodyExited(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            HitTimer.Stop();
        }
    }

    public void OnHitTimerTimeout()
    {
        Target.GetNode<HealthBar>("HealthBar").RemoveHealthPoints(5);
    }

    public void LostAllHealth()
    {
        EmitSignal(SignalName.EnemyLostAllHealth, this);
        QueueFree();
    }

    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(Item item)
    {
        throw new NotImplementedException();
    }
}
