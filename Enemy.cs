using Godot;
using Nova.Scripts.Character;
using System;

public partial class Enemy : RigidBody2D, ICharacter
{
    public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public PackedScene WeaponScene { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public BaseWeapon Weapon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Marker2D WeaponLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public HealthBar HealthBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Marker2D HealthBarLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public double HealthPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void EnableAttack()
    {
        throw new NotImplementedException();
    }

    public void DisableAttack()
    {
        throw new NotImplementedException();
    }
}
