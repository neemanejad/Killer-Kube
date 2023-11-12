using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	[Export]
	public Vector2 Direction { get; set; }
	[Export]
	public double Speed { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Vector2.Right;
		Position = new Vector2(
			x: (float)(Position.X + (Speed * delta)),
			y: (float)(Position.Y + (Speed * delta))
		);

	}

	public void OnBodyEntered()
	{
		QueueFree();
	}
}
