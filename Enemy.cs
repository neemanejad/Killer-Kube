using Godot;
using Nova.Scripts.Character;
using System;

public partial class Enemy : Area2D, ICharacter
{
    public int Speed { get; set; }
    public Node Target { get; set; }
    public PackedScene WeaponScene { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public BaseWeapon Weapon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Marker2D WeaponLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public HealthBar HealthBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Marker2D HealthBarLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public double HealthPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Timer HitTimer { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        HitTimer = GetNode<Timer>("HitTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //GlobalPosition += (GlobalPosition.DirectionTo(Target.Position) * (float)delta * Speed);

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
}
