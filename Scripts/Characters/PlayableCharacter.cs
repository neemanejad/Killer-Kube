using Godot;
using Killer_Kube.Scripts;
using Nova.Scripts;
using Nova.Scripts.Character;
using System;
using static IWeapon;

public partial class PlayableCharacter : Character, IPlayableCharacter
{
    public int Score { get; set; }
    public TimeSpan SurvivalTime { get; set; }
    public HUD HUD { get; set; }
    public Inventory Inventory { get; set; }
    public ItemPopupHUD ItemPopupHUD { get; set; }

    public void UpdateScoreString()
    {
        HUD.GetNode<Label>("ScoreLabel").Text = string.Format("Score: {0}", Score);
    }

    public void UpdateSurvivalTimeString()
    {
       HUD.GetNode<Label>("SurvivalTimeLabel").Text = SurvivalTime.ToString(@"mm\:ss");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Setup health related signals
        Connect(SignalName.OnLostAllHealth, new Callable(this, MethodName.ShowGameOverScreen));

        HUD = GetNode<HUD>("HUD");
        Inventory = GetNode<Inventory>("Inventory");
        ItemPopupHUD = GetNode<ItemPopupHUD>("ItemPopupHUD");

        // Initialize and spawn weapon
        WeaponLocation = GetNode<Marker2D>("WeaponLocation");
        WeaponLocation.Position = new Vector2(Position.X, Position.Y);
        Weapon = WeaponScene.Instantiate<Weapon>();
        Weapon.Position = GetNode<Marker2D>("WeaponLocation").Position;
        Weapon.Scale = new Vector2((float)0.2, (float)0.2);
        AddChild(Weapon);
        EnableAttack();

        // Initialize health bar
        HealthBar = GetNode<HealthBar>("HealthBar");
        HealthBar.Health = HealthPoints;
        HealthBar.MaxHealth = HealthPoints;

        GetNode<Timer>("SurvivalTimer").Start();

        // Set global variable so all enemies can track the player's position
        Globals.Target = this;
    }

    public void SurvivalTimerTimeout()
    {
        SurvivalTime += TimeSpan.FromSeconds(1);
        UpdateSurvivalTimeString();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        ProcessMovementInput(delta);
        ProcessMouseMovement(delta);
    }

    public void ProcessMovementInput(double delta)
    {
        var velocity = Vector2.Zero;

        if (Input.IsActionJustPressed("pause"))
        {
            var pauseMenuScene = ResourceLoader.Load<PackedScene>("res://Scenes/Menus/PauseMenu.tscn");
            var pauseMenu = pauseMenuScene.Instantiate<PauseMenu>();
            var hud = GetNode<Control>("HUD");

            pauseMenu.SetDeferred(Control.PropertyName.Size, hud.Size);
            pauseMenu.SetDeferred(Control.PropertyName.GlobalPosition, hud.GlobalPosition);

            AddChild(pauseMenu);
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }

        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public void ProcessMouseMovement(double delta)
    {
        Weapon.PointWeaponAtLocation(GlobalPosition.DirectionTo(GetGlobalMousePosition()) + Weapon.GlobalPosition);
        WeaponLocation.Position = Position.DirectionTo(GetGlobalMousePosition()) * 40;
        Weapon.Position = WeaponLocation.Position;

        //// Check if weapon needs to be flipped based on the mouse location
        var relativeMouseLocation = Position.DirectionTo(GetGlobalMousePosition());
        if (relativeMouseLocation.X >= 0 && relativeMouseLocation.Y > 0 && Weapon.Direction == WeaponDirection.Left)
        {
            // Mouse moving from left to right below the player
            Weapon.FlipWeaponRight();
        }
        else if (relativeMouseLocation.X >= 0 && relativeMouseLocation.Y <= 0 && Weapon.Direction == WeaponDirection.Left)
        {
            // Mouse moving from left to right above the player
            Weapon.FlipWeaponRight();
        }
        else if (relativeMouseLocation.X < 0 && relativeMouseLocation.Y > 0 && Weapon.Direction == WeaponDirection.Right)
        {
            // Mouse moving from right to left below the player
            Weapon.FlipWeaponLeft();
        }
        else if (relativeMouseLocation.X < 0 && relativeMouseLocation.Y < 0 && Weapon.Direction == WeaponDirection.Right)
        {
            // Mouse moving from right to left above the player
            Weapon.FlipWeaponLeft();
        }
    }

    public void KilledEnemy(Enemy enemy)
    {
        Score += enemy.PointsForDeath;
        UpdateScoreString();
    }

    public void ShowGameOverScreen()
    {
        var gameOverMenuScene = ResourceLoader.Load<PackedScene>("res://Scenes/Menus/GameOverMenu.tscn");
        var gameOverMenu = gameOverMenuScene.Instantiate<GameOverMenu>();
        var hud = GetNode<Control>("HUD");

        gameOverMenu.SetDeferred(Control.PropertyName.Size, GetViewportRect().Size);
        gameOverMenu.SetDeferred(Control.PropertyName.GlobalPosition, GetViewportRect().Position);

        // Add as child of root node so the menu doesn't
        // get deleted alongside the PlayableCharacter
        GetTree().Root.AddChild(gameOverMenu);
    }

    public void AddItem(Item item)
    {
        ItemPopupHUD.AddItem(item);
    }
}
