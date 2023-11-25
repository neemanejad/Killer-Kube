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
        public PackedScene PlayerScene { get; set; }
        public PlayableCharacter Player { get; set; }
        public Path2D EnemySpawnPath { get; set; }
        public PathFollow2D EnemyPathFollow { get; set; }
        public Timer EnemySpawnRate { get; set; }
        public Timer SurvivalTimer { get; set; }
        public TimeSpan SurvivalTime { get; set; }
        public DateTime GameStartTime { get; set; }
        public void SpawnEnemy();
        public void StartSpawningEnemies();
        public void StopSpawningEnemies();
        public void SurvivalTimerTimeout();
    }
}
