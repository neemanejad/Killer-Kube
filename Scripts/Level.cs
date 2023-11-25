using Godot;
using Nova.Scripts;
using Nova.Scripts.Character;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;

public partial class Level : Node, ILevel
{
    public PackedScene PlayerScene { get; set; }
    public PlayableCharacter Player { get; set; }
    public PackedScene[] EnemyScenes { get; set; } = new PackedScene[] { ResourceLoader.Load<PackedScene>("res://Scenes/Enemies/Enemy.tscn") };
    public Path2D EnemySpawnPath { get; set; }
    public PathFollow2D EnemyPathFollow { get; set; }
    public Timer EnemySpawnRate { get; set; }
    public Timer SurvivalTimer { get; set; }
    public TimeSpan SurvivalTime { get; set; }
    public DateTime GameStartTime { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = PlayerScene.Instantiate<PlayableCharacter>();
        AddChild(Player);

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

        // Recalculate enemy position if the position is outside the level ground
        while (IsOutsideLevel(EnemyPathFollow.GlobalPosition))
        {
            EnemyPathFollow.ProgressRatio = GD.Randf();
        }

        var enemy = EnemyScenes[0].Instantiate<Enemy>();
        enemy.Speed = 250;
        enemy.HealthPoints = 25;
        enemy.GlobalPosition = EnemyPathFollow.GlobalPosition;
        enemy.Connect(Enemy.SignalName.EnemyLostAllHealth, new Callable(this, MethodName.IncreaseScore));

        AddChild(enemy);
    }

    public bool IsOutsideLevel(Vector2 position)
    {
        var levelGround = GetNode<ReferenceRect>("Area2D/ReferenceRect");
        return !levelGround.GetGlobalRect().HasPoint(position);
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
