using Godot;
using Nova.Scripts;
using Nova.Scripts.Character;
using Nova.Scripts.Weapons;
using System;
using System.Collections.Generic;
using static IWeapon;

public partial class PlayableCharacter : Character, IPlayableCharacter
{
    public int Score { get; set; }
    public TimeSpan SurvivalTime { get; set; }
    public List<Weapon> Weapons { get; set; }

    public Dictionary<string, Weapon> WeaponsDictionary;

    public void UpdateScoreString()
    {
        GetNode<Label>("HUD/ScoreLabel").Text = string.Format("Score: {0}", Score);
    }

    public void UpdateSurvivalTimeString()
    {
        GetNode<Label>("HUD/SurvivalTimeLabel").Text = SurvivalTime.ToString(@"mm\:ss");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Setup health related signals
        Connect(SignalName.OnLostAllHealth, new Callable(this, MethodName.ShowGameOverScreen));

        // Add all possible weapons
        Weapons = new List<Weapon>() 
        { 
            ResourceLoader.Load<PackedScene>("res://Scenes/Uzi.tscn").Instantiate<Uzi>(), 
            ResourceLoader.Load<PackedScene>("res://Scenes/Shotgun.tscn").Instantiate<Shotgun>()
        };
        WeaponsDictionary = new Dictionary<string, Weapon>();

        foreach (var weapon in Weapons)
        {
            WeaponsDictionary[weapon.GetWeaponName()] = weapon;

            weapon.Position = GetNode<Marker2D>("WeaponLocation").Position;
            weapon.Scale = new Vector2(0.2f, 0.2f);
            weapon.Hide();
            AddChild(weapon);
        }

        // Initialize and spawn weapon
        WeaponLocation = GetNode<Marker2D>("WeaponLocation");
        WeaponLocation.Position = new Vector2(Position.X, Position.Y);
        CurrentWeapon = Weapons[0];
        CurrentWeapon.Show();
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
        ProcessWeaponSelectionInput();
        ProcessMenuInputs();
    }

    private void ProcessMenuInputs()
    {
        if (Input.IsActionJustPressed("pause"))
        {
            var pauseMenuScene = ResourceLoader.Load<PackedScene>("res://Scenes/Menus/PauseMenu.tscn");
            var pauseMenu = pauseMenuScene.Instantiate<PauseMenu>();
            var hud = GetNode<Control>("HUD");

            pauseMenu.SetDeferred(Control.PropertyName.Size, hud.Size);
            pauseMenu.SetDeferred(Control.PropertyName.GlobalPosition, hud.GlobalPosition);

            AddChild(pauseMenu);
        } else if (Input.IsActionJustPressed("inventory"))
        {
            var inventoryMenuScene = ResourceLoader.Load<PackedScene>("res://Scenes/Menus/InventoryMenu.tscn");
            var inventoryMenu = inventoryMenuScene.Instantiate<InventoryMenu>();
            var hud = GetNode<Control>("HUD");

            inventoryMenu.SetDeferred(Control.PropertyName.Size, hud.Size * 0.75f);
            inventoryMenu.SetDeferred(Control.PropertyName.GlobalPosition, hud.GlobalPosition * 0.75f);

            AddChild(inventoryMenu);
        }
    }

    private void ProcessWeaponSelectionInput()
    {
        if (Input.IsActionJustPressed("weapon_1"))
        {
            DisableAttack();
            CurrentWeapon.Hide();
            CurrentWeapon = Weapons[0];
            CurrentWeapon.Show();
            EnableAttack();
        } else if (Input.IsActionJustPressed("weapon_2"))
        {
            DisableAttack();
            CurrentWeapon.Hide();
            CurrentWeapon = Weapons[1];
            CurrentWeapon.Show();
            EnableAttack();
        }
    }

    public void ProcessMovementInput(double delta)
    {
        var velocity = Vector2.Zero;

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
        CurrentWeapon.PointWeaponAtLocation(GlobalPosition.DirectionTo(GetGlobalMousePosition()) + CurrentWeapon.GlobalPosition);
        WeaponLocation.Position = Position.DirectionTo(GetGlobalMousePosition()) * 40;
        CurrentWeapon.Position = WeaponLocation.Position;

        //// Check if weapon needs to be flipped based on the mouse location
        var relativeMouseLocation = Position.DirectionTo(GetGlobalMousePosition());
        if (relativeMouseLocation.X >= 0 && relativeMouseLocation.Y > 0 && CurrentWeapon.Direction == WeaponDirection.Left)
        {
            // Mouse moving from left to right below the player
            CurrentWeapon.FlipWeaponRight();
        }
        else if (relativeMouseLocation.X >= 0 && relativeMouseLocation.Y <= 0 && CurrentWeapon.Direction == WeaponDirection.Left)
        {
            // Mouse moving from left to right above the player
            CurrentWeapon.FlipWeaponRight();
        }
        else if (relativeMouseLocation.X < 0 && relativeMouseLocation.Y > 0 && CurrentWeapon.Direction == WeaponDirection.Right)
        {
            // Mouse moving from right to left below the player
            CurrentWeapon.FlipWeaponLeft();
        }
        else if (relativeMouseLocation.X < 0 && relativeMouseLocation.Y < 0 && CurrentWeapon.Direction == WeaponDirection.Right)
        {
            // Mouse moving from right to left above the player
            CurrentWeapon.FlipWeaponLeft();
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

    public void SelectWeapon(string weaponName)
    {
        var weapon = WeaponsDictionary[weaponName];

        DisableAttack();
        CurrentWeapon.Hide();
        CurrentWeapon = weapon;
        CurrentWeapon.Show();
        EnableAttack();
    }
}
