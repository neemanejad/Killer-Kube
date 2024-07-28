using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts.Character
{
    internal interface IPlayableCharacter : ICharacter
    {
        public int Score { get; set; }
        public TimeSpan SurvivalTime { get; set; }
        public void UpdateScoreString();
        public void UpdateSurvivalTimeString();
        public void ProcessKeyboardInput(double delta);
        public void ProcessMouseMovement(double delta);
        public void KilledEnemy(Enemy enemy);
    }
}
