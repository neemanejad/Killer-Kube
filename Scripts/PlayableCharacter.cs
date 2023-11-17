using Godot;
using static IWeapon;

public partial class PlayableCharacter : Character
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Initialize and spawn weapon
        WeaponLocation = GetNode<Marker2D>("WeaponLocation");
        WeaponLocation.Position = new Vector2(Position.X, Position.Y);
        Weapon = WeaponScene.Instantiate<BaseWeapon>();
        Weapon.Position = GetNode<Marker2D>("WeaponLocation").Position;
        Weapon.Scale = new Vector2((float)0.2, (float)0.2);
        Weapon.WeaponDamage = 2;
        Weapon.WeaponHitRate = 0.05;
        AddChild(Weapon);

        // Initialize health bar
        HealthBar = GetNode<HealthBar>("HealthBar");
        HealthBar.Health = HealthPoints;
        HealthBar.MaxHealth = HealthPoints;

        EnableAttack();
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
}
