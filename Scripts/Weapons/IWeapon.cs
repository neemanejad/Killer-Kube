using Godot;
using System;

public interface IWeapon
{
	public string WeaponName { get; set; }
	public double WeaponDamage { get; set; }
	public double WeaponHitRate { get; set; }
	public double WeaponDamageMultiplier { get; set; }
	public PackedScene Projectile { get; set; }

	public void HandleWeaponAction();
}
