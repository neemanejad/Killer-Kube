using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts
{
    internal interface ILevel
    {
        public LevelConfig LevelConfig { get; set; }
        public int CurrentMilestoneIndex { get; set; }
        public PackedScene PlayerScene { get; set; }
        public PlayableCharacter Player { get; set; }
        public Path2D EnemySpawnPath { get; set; }
        public PathFollow2D EnemyPathFollow { get; set; }
        public void MilestoneTimerTimeout();
    }
}
