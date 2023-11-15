using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using static IWeapon;

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
    public WeaponDirection Direction { get; set; } = WeaponDirection.Right;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetNode<Timer>("HitTimer").WaitTime = WeaponHitRate;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
	}

    private void OnHitTimerTimeout()
    {
        var projectile = Projectile.Instantiate<Bullet>();
        projectile.Speed = 500;
        projectile.GlobalPosition = GetNode<Marker2D>("ProjectileSpawnPoint").GlobalPosition;
        projectile.Scale = new Vector2(1, 1);
        projectile.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition() + Position);
        projectile.Rotation = Rotation;
        projectile.WeaponOfOrigin = this;
        
        GetTree().Root.AddChild(projectile);
    }

    public void PointWeaponAtLocation(Vector2 location)
    {
        LookAt(location);
    }

    public void FlipWeaponRight()
    {
        Scale = Scale.Y <= 0 ? new Vector2(Scale.X, -Scale.Y) : Scale;
        Direction = WeaponDirection.Right;
    }

    public void FlipWeaponLeft()
    {
        Scale = Scale.Y >= 0 ? new Vector2(Scale.X, -Scale.Y) : Scale;
        Direction = WeaponDirection.Left;
    }

    public void EnableWeaponAction()
    {
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("shoot");
        GetNode<Timer>("HitTimer").Start();
    }

    public void DisableWeaponAction()
    {
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
        GetNode<Timer>("HitTimer").Stop();
    }
}
