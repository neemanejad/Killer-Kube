using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class BaseWeapon : Node2D, IWeapon
{
    [Export]
    public string WeaponName { get; set; }
    [Export]
    public double WeaponDamage { get; set; }
    [Export]
    public double WeaponHitRate { get; set; }
    [Export]
    public double WeaponDamageMultiplier { get; set; }
    [Export]
    public PackedScene Projectile { get; set; }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetNode<Timer>("HitTimer").WaitTime = WeaponHitRate;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        ProcessFireInput(delta);
	}

    public void GetDamage()
    {
        throw new NotImplementedException();
    }

    public double CalculateDamage()
    {
        throw new NotImplementedException();
    }

    private void ProcessFireInput(double delta)
    {
        var weaponSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (Input.IsActionPressed("fire_weapon"))
        {
            weaponSprite.Play("shoot");
            var hitTimer = GetNode<Timer>("HitTimer");
            if (hitTimer.TimeLeft == 0)
            {
                hitTimer.Start();
            }
        }
        else
        {
            weaponSprite.Play("idle");
            GetNode<Timer>("HitTimer").Stop();
        }
    }

    private void OnHitTimerTimeout()
    {
        var projectile = Projectile.Instantiate<Bullet>();
        projectile.Speed = 600;
        projectile.Direction = new Vector2(5.0f, 0.0f);

        AddChild(projectile);
    }
}
