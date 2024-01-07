using Godot;
using System;

public interface IWeapon
{
	public enum WeaponDirection
	{
		Left,
		Right
	}
	public double WeaponDamage { get; set; }
	public double WeaponHitRate { get; set; }
	public double WeaponDamageMultiplier { get; set; }
	public WeaponDirection Direction { get; set; }
	public Timer HitTimer { get; set; }
	public void PointWeaponAtLocation(Vector2 location);

    public void FlipWeaponRight();
	public void FlipWeaponLeft();
	public void EnableWeaponAction();
	public void DisableWeaponAction();
	public string GetWeaponName();
}
