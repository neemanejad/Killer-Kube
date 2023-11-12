using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class BaseWeapon : Node2D, IWeapon
{

    public string WeaponName { get; set; }
    public double WeaponDamage { get; set; }
    public double WeaponHitRate { get; set; }
    public double WeaponDamageMultiplier { get; set; }



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public void GetDamage()
    {
        throw new NotImplementedException();
    }

    public double CalculateDamage()
    {
        throw new NotImplementedException();
    }
}
