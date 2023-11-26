using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts
{
    public struct LevelConfig
    {
        public LevelMilestone[] Milestones;
    }

    public struct LevelMilestone
    {
        public TimeSpan MilestoneDuration;
        public EnemyConfig[] Enemies;
    }

    public struct EnemyConfig
    {
        public string Name;
        public PackedScene EnemyScene;
        public TimeSpan SpawnRate;
        public float Speed;
        public double HealthPoints;
    }
}
