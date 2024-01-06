using Godot;

namespace Nova.Scripts.Weapons
{
    public partial class Uzi : Weapon
    {
        private PackedScene bulletScene;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            bulletScene = ResourceLoader.Load<PackedScene>("res://Scenes/Bullet.tscn");
            WeaponHitRate = 0.05;
            WeaponDamage = 8;
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

        public override void EnableWeaponAction()
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("shoot");
            HitTimer.Start();
        }

        public override void DisableWeaponAction()
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
            HitTimer.Stop();
        }
    }


}
