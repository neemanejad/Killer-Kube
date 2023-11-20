using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts
{
    internal struct LevelConfig
    {
        LevelTimerMilestone[] milestones;
    }

    internal struct LevelTimerMilestone
    {
        DateTime start;
        DateTime end;
        EnemyConfig[] enemies;
    }

    internal struct EnemyConfig
    {
        string name;
        PackedScene enemyScene;
        TimeSpan spawnRate;
    }
}
