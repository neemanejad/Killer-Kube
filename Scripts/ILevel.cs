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
        public PathFollow2D EnemySpawnPath { get; set; }
        public Timer EnemySpawnRate { get; set; }
        public void SpawnEnemy();
        public void StartSpawningEnemies();
        public void StopSpawningEnemies();
    }
}
