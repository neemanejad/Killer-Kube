using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts.Character
{
    internal interface ICharacter
    {
        public int Speed { get; set; }
        public PackedScene WeaponScene { get; set; }
        public BaseWeapon Weapon { get; set; }
        public Marker2D WeaponLocation { get; set; }
        public PackedScene HealthBar { get; set; }
        public Marker2D HealthBarLocation { get; set; }
        public double HealthPoints { get; set; }
        public void EnableAttack();
        public void DisableAttack();
    }
}
