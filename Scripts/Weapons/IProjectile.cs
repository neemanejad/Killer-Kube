using Godot;

namespace Nova.Scripts.Weapons
{
    internal interface IProjectile
    {
        public Vector2 Direction { get; set; }
        public double Speed { get; set; }
        public Weapon WeaponOfOrigin { get; set; }
    }
}
