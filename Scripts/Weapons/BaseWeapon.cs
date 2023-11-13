using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class BaseWeapon : Node2D, IWeapon
{
    public enum WeaponDirection
    {
        Left,
        Right
    }

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
        projectile.CollisionLayer = 1;
        projectile.Scale = new Vector2(1, 1);
        projectile.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition() + Position);
        projectile.Rotation = Rotation;
        
        GetTree().Root.AddChild(projectile);
    }

    public void PointWeaponAtLocation(Vector2 location)
    {
        LookAt(location);
    }

    public void Flip()
    {
        Scale = new Vector2(Scale.X, -Scale.Y);
        Direction = Direction == WeaponDirection.Left ? WeaponDirection.Right : WeaponDirection.Left;
    }

    public void HandleWeaponAction()
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
}
