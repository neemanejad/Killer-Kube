using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using static IWeapon;

public partial class Weapon : Node2D, IWeapon
{
    public string WeaponName { get; set; }
    public double WeaponDamage { get; set; }
    public double WeaponHitRate { get; set; }
    public double WeaponDamageMultiplier { get; set; }
    public Timer HitTimer { get; set; }
    public WeaponDirection Direction { get; set; } = WeaponDirection.Right;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
	}

    public virtual void OnHitTimerTimeout()
    {
        throw new NotImplementedException();
    }

    public virtual void PointWeaponAtLocation(Vector2 location)
    {
        LookAt(location);
    }

    public virtual void FlipWeaponRight()
    {
        Scale = Scale.Y <= 0 ? new Vector2(Scale.X, -Scale.Y) : Scale;
        Direction = WeaponDirection.Right;
    }

    public virtual void FlipWeaponLeft()
    {
        Scale = Scale.Y >= 0 ? new Vector2(Scale.X, -Scale.Y) : Scale;
        Direction = WeaponDirection.Left;
    }

    public virtual void EnableWeaponAction()
    {
        HitTimer.Start();
    }

    public virtual void DisableWeaponAction()
    {
        HitTimer.Stop();
    }
}
