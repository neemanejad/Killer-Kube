using Godot;
using System;
using System.Transactions;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	public PackedScene WeaponScene { get; set; }
	public BaseWeapon Weapon { get; set; }
	public Marker2D WeaponLocation { get; set; }

	public Vector2 ScreenSize; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WeaponLocation = GetNode<Marker2D>("WeaponLocation");
		WeaponLocation.Position = new Vector2(Position.X, Position.Y);
		ScreenSize = GetViewportRect().Size;
		WeaponScene = (PackedScene)ResourceLoader.Load("res://BaseWeapon.tscn");
		Weapon = WeaponScene.Instantiate<BaseWeapon>();
		Weapon.Position = GetNode<Marker2D>("WeaponLocation").Position;
		Weapon.Scale = new Vector2((float)0.2, (float)0.2);
		AddChild(Weapon);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ProcessMovementInput(delta);
		ProcessMouseMovement(delta);
		ProcessMouseInput(delta);
	}

    private void ProcessMovementInput(double delta)
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

        var camera = GetNode<Camera2D>("Camera2D");
        camera.LookAt(Position);
    }

    private void ProcessMouseMovement(double delta)
    {
		Weapon.PointWeaponAtLocation(GlobalPosition.DirectionTo(GetGlobalMousePosition()) + Weapon.GlobalPosition);
		WeaponLocation.Position = Position.DirectionTo(GetGlobalMousePosition()) * 40;
		Weapon.Position = WeaponLocation.Position;

		//// Check if weapon needs to be flipped based on the mouse location
		var relativeMouseLocation = Position.DirectionTo(GetGlobalMousePosition());
		if (relativeMouseLocation.X >= 0 && relativeMouseLocation.Y > 0 && Weapon.Direction == BaseWeapon.WeaponDirection.Left)
		{
            // Mouse moving from left to right below the player
            Weapon.Flip();
        }
		else if (relativeMouseLocation.X >= 0 && relativeMouseLocation.Y <= 0 && Weapon.Direction == BaseWeapon.WeaponDirection.Left)
		{
            // Mouse moving from left to right above the player
            Weapon.Flip();
        }
		else if (relativeMouseLocation.X < 0 && relativeMouseLocation.Y > 0 && Weapon.Direction == BaseWeapon.WeaponDirection.Right)
		{
            // Mouse moving from right to left below the player
            Weapon.Flip();
        }
		else if (relativeMouseLocation.X < 0 && relativeMouseLocation.Y < 0 && Weapon.Direction == BaseWeapon.WeaponDirection.Right)
		{
			// Mouse moving from right to left above the player
			Weapon.Flip();
		}
	}

	public void ProcessMouseInput(double delta)
	{
		Weapon.HandleWeaponAction();
	}
}
