using Godot;
using Nova.Scripts;
using System;

public partial class PrisonLevel : Level
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Set level milestones
        LevelMilestone[] milestones = new LevelMilestone[]
        {
            new LevelMilestone
            {
                MilestoneDuration = TimeSpan.FromSeconds(5),
                Enemies = new EnemyConfig[] {
                    new EnemyConfig
                    {
                        Name = "NormalEnemey",
                        EnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/Enemies/Enemy.tscn"),
                        SpawnRate = TimeSpan.FromSeconds(2),
                        Speed = 350,
                        HealthPoints = 50
                    }
                }
            },
            new LevelMilestone
            {
                MilestoneDuration = TimeSpan.FromSeconds(15),
                Enemies = new EnemyConfig[] {
                    new EnemyConfig
                    {
                        Name = "FastEnemy",
                        EnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/Enemies/Enemy.tscn"),
                        SpawnRate = TimeSpan.FromSeconds(1),
                        Speed = 450,
                        HealthPoints = 20
                    }
                }
            }
        };

        LevelConfig = new LevelConfig
        {
            Milestones = milestones
        };

        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
