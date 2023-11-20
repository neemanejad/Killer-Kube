using Godot;

namespace Nova.Scripts.Weapons
{
    public partial class Projectile : Area2D, IProjectile
    {
        public Vector2 Direction { get; set; }
        public double Speed { get; set; }
        public Weapon WeaponOfOrigin { get; set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            GlobalPosition = new Vector2(
                x: (float)(GlobalPosition.X + (Speed * delta * Direction.Normalized().X)),
                y: (float)(GlobalPosition.Y + (Speed * delta * Direction.Normalized().Y))
            );

        }

        public void OnScreenExited()
        {
            QueueFree();
        }
    }
}
