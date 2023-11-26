using Godot;
using Nova.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Level : Node, ILevel
{
    public LevelConfig LevelConfig { get; set; }
    public int CurrentMilestoneIndex { get; set; } = 0;
    public PackedScene PlayerScene { get; set; }
    public PlayableCharacter Player { get; set; }
    public Path2D EnemySpawnPath { get; set; }
    public PathFollow2D EnemyPathFollow { get; set; }
    public Timer SurvivalTimer { get; set; }
    public TimeSpan SurvivalTime { get; set; }

    private List<Timer> enemyTimers { get; set; } = new List<Timer>();
    private Dictionary<string, EnemyConfig> EnemyMap { get; set; } = new Dictionary<string, EnemyConfig>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = PlayerScene.Instantiate<PlayableCharacter>();
        AddChild(Player);

        EnemySpawnPath = GetNode<Path2D>("PlayableCharacter/EnemySpawnPath");
        EnemyPathFollow = GetNode<PathFollow2D>("PlayableCharacter/EnemySpawnPath/PathFollow2D");
        SurvivalTimer = GetNode<Timer>("SurvivalTimer");

        CreateEnemyMap();
        CreateEnemyTimers();
        StartEnemyTimers();

        var milestoneTimer = GetNode<Timer>("MilestoneTimer");
        milestoneTimer.WaitTime = LevelConfig.Milestones[CurrentMilestoneIndex].MilestoneDuration.TotalSeconds;
        milestoneTimer.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public bool IsOutsideLevel(Vector2 position)
    {
        var levelGround = GetNode<ReferenceRect>("Area2D/ReferenceRect");
        return !levelGround.GetGlobalRect().HasPoint(position);
    }

    public void CreateEnemyMap()
    {
        var configs = LevelConfig.Milestones[CurrentMilestoneIndex].Enemies;
        foreach (var config in configs)
        {
            EnemyMap[config.Name] = config;
        }
    }

    private void CreateEnemyTimers()
    {
        var configs = LevelConfig.Milestones[CurrentMilestoneIndex].Enemies;
 
        foreach (var config in configs)
        {
            Timer timer = new EnemyTimer(config.Name);
            timer.WaitTime = config.SpawnRate.TotalSeconds;
            timer.Connect(EnemyTimer.SignalName.EnemyTimerTimeout, new Callable(this, MethodName.EnemySpawnTimerTimeout));
            enemyTimers.Add(timer);
            AddChild(timer);
        }
    }

    public void EnemySpawnTimerTimeout(string enemyName) {
        EnemyPathFollow.ProgressRatio = GD.Randf();

        // Recalculate enemy position if the position is outside the level ground
        while (IsOutsideLevel(EnemyPathFollow.GlobalPosition))
        {
            EnemyPathFollow.ProgressRatio = GD.Randf();
        }

        var enemyConfig = EnemyMap[enemyName];
        var enemy = enemyConfig.EnemyScene.Instantiate<Enemy>();
        enemy.Speed = enemyConfig.Speed;
        enemy.HealthPoints = enemyConfig.HealthPoints;
        enemy.GlobalPosition = EnemyPathFollow.GlobalPosition;
        enemy.Connect(Enemy.SignalName.EnemyLostAllHealth, new Callable(this, MethodName.IncreaseScore));

        AddChild(enemy);
    }

    private void QueueFreeEnemyTimers()
    {
        foreach (var timer in enemyTimers)
        {
            timer.QueueFree();
        }

        enemyTimers.Clear();
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

    public void MilestoneTimerTimeout()
    { 
        if (CurrentMilestoneIndex == LevelConfig.Milestones.Length)
        {
            StopEnemyTimers();

            // TODO: show a different screen/menu here after surviving all milestones
            return;
        }

        // Cleaning
        QueueFreeEnemyTimers();
        EnemyMap.Clear();

        CurrentMilestoneIndex += 1;

        CreateEnemyMap();
        CreateEnemyTimers();
        StartEnemyTimers();

        GetNode<Timer>("MilestoneTimer").WaitTime = LevelConfig.Milestones[CurrentMilestoneIndex].MilestoneDuration.TotalSeconds;
        GetNode<Timer>("MilestoneTimer").Start(0);
    }

    private void StartEnemyTimers()
    {
        foreach (var timer in enemyTimers)
        {
            timer.Start();
        }
    }

    private void StopEnemyTimers()
    {
        foreach (var timer in enemyTimers)
        {
            timer.Stop();
        }
    }
}
