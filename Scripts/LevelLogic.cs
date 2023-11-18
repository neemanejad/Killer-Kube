using Godot;
using Nova.Scripts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;

public partial class LevelLogic : Node, ILevel
{
    public PathFollow2D EnemySpawnPath { get; set; }
    public Timer EnemySpawnRate { get; set; }
    [Export, Required, MinLength(1)]
    public PackedScene[] EnemyScenes { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        EnemySpawnPath = GetNode<PathFollow2D>("PlayableCharacter/EnemySpawnPath/PathFollow2D");
        EnemySpawnRate = GetNode<Timer>("EnemySpawnRate");

        EnemySpawnRate.WaitTime = 3;

        StartSpawningEnemies();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public void SpawnEnemy()
    {
        EnemySpawnPath.ProgressRatio = GD.Randf();

        var enemy = EnemyScenes[0].Instantiate<Enemy>();
        enemy.Speed = 250;
        enemy.HealthPoints = 25;
        enemy.GlobalPosition = EnemySpawnPath.GlobalPosition;

        AddChild(enemy);
    }

    public void StartSpawningEnemies()
    {
        EnemySpawnRate.Start();
    }

    public void StopSpawningEnemies()
    {
        EnemySpawnRate.Stop();
    }
}
