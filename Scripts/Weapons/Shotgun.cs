using Godot;
using Nova.Scripts.Weapons;
using System;
using System.Collections.Generic;

public partial class Shotgun : Weapon
{
    private PackedScene bulletScene;

    public override void _Ready()
    {        
        bulletScene = ResourceLoader.Load<PackedScene>("res://Scenes/Bullet.tscn");
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
        List<Bullet> bullets = new List<Bullet>();
        for (int i = 0; i < 3; i++)
        {
            var projectile = bulletScene.Instantiate<Bullet>();
            projectile.Speed = 1250;
            projectile.GlobalPosition = GetNode<Marker2D>("ProjectileSpawnPoint").GlobalPosition;
            projectile.Scale = new Vector2(1, 1);

            // Randomize the projectil direction to emulate shotgun pellet spread
            var projectileDirection = GlobalPosition.DirectionTo(GetGlobalMousePosition() + Position);
            projectileDirection.X += (i % 2 == 0 ? GD.Randf() : -GD.Randf()) * 0.2f;
            projectileDirection.Y += (i % 2 == 0 ? GD.Randf() : -GD.Randf()) * 0.2f;
            projectile.Direction = projectileDirection.Normalized();
            projectile.Rotation = Rotation;
            projectile.WeaponOfOrigin = this;
            bullets.Add(projectile);
        }

        foreach (var bullet in bullets)
        {
            GetTree().Root.AddChild(bullet);
        }
        
    }

    public override string GetWeaponName()
    {
        return "Shotgun";
    }
}
