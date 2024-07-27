using Godot;
using System;

public partial class Shotgun : Weapon
{
    private PackedScene bulletScene;

    public override void _Ready()
    {
        bulletScene = ResourceLoader.Load<PackedScene>("res://Weapons/Bullet.tscn");
        WeaponHitRate = 0.5;
        WeaponDamage = 30;
        HitTimer = GetNode<Timer>("HitTimer");
        HitTimer.WaitTime = WeaponHitRate;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public override void OnHitTimerTimeout()
    {
        var projectile = bulletScene.Instantiate<Bullet>();
        projectile.Speed = 1250;
        projectile.GlobalPosition = GetNode<Marker2D>("ProjectileSpawnPoint").GlobalPosition;
        projectile.Scale = new Vector2(1, 1);
        projectile.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition() + Position);
        projectile.Rotation = Rotation;
        projectile.WeaponOfOrigin = this;

        GetTree().Root.AddChild(projectile);
    }
}
