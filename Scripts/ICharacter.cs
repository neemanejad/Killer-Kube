using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts
{
    public interface ICharacter
    {
        public float Speed { get; set; }
        public PackedScene WeaponScene { get; set; }
        public Weapon Weapon { get; set; }
        public Marker2D WeaponLocation { get; set; }
        public HealthBar HealthBar { get; set; }
        public Marker2D HealthBarLocation { get; set; }
        public double HealthPoints { get; set; }
        public void EnableAttack();
        public void DisableAttack();
        public void LostAllHealth();
    }
}
