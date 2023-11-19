using Godot;
using Nova.Scripts;
using Nova.Scripts.Character;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;

public partial class LevelLogic : Node, ILevel
{
    public Path2D EnemySpawnPath { get; set; }
    public PathFollow2D EnemyPathFollow { get; set; }
    public Timer EnemySpawnRate { get; set; }
    [Export, Required, MinLength(1)]
    public PackedScene[] EnemyScenes { get; set; }
    public Timer SurvivalTimer { get; set; }
    public TimeSpan SurvivalTime { get; set; }
    public DateTime GameStartTime { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EnemySpawnPath = GetNode<Path2D>("PlayableCharacter/EnemySpawnPath");
        EnemyPathFollow = GetNode<PathFollow2D>("PlayableCharacter/EnemySpawnPath/PathFollow2D");
        EnemySpawnRate = GetNode<Timer>("EnemySpawnRate");
        SurvivalTimer = GetNode<Timer>("SurvivalTimer");

        GameStartTime = DateTime.Now;
        EnemySpawnRate.WaitTime = 3;

        StartSpawningEnemies();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void SpawnEnemy()
    {
        EnemyPathFollow.ProgressRatio = GD.Randf();

        var enemy = EnemyScenes[0].Instantiate<Enemy>();
        enemy.Speed = 250;
        enemy.HealthPoints = 25;
        enemy.GlobalPosition = EnemyPathFollow.GlobalPosition;
        enemy.Connect(Enemy.SignalName.EnemyLostAllHealth, new Callable(this, MethodName.IncreaseScore));

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

    public void SurvivalTimerTimeout()
    {
        SurvivalTime = new TimeSpan(SurvivalTime.Ticks + TimeSpan.TicksPerSecond);
        GetNode<Label>("PlayableCharacter/HUD/SurvivalTimeLabel").Text = SurvivalTime.ToString(@"mm\:ss");
    }

    public void IncreaseScore()
    {
        Globals.Target.Score += 1;
        GetNode<Label>("PlayableCharacter/HUD/ScoreLabel").Text = String.Format("Score: {0}", Globals.Target.Score);
    }
}
