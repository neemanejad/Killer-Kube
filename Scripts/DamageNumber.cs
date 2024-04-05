using Godot;
using System;

public partial class DamageNumber : Node2D
{
	private Timer VisibilityTimer;
	private Label DamageNumberLabel;
	public double Number = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DamageNumberLabel = GetNode<Label>("DamageNumberLabel");
		DamageNumberLabel.Text = Number.ToString();

        VisibilityTimer = GetNode<Timer>("VisibilityTimer");
		VisibilityTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void VisibilityTimerTimeout()
	{
		QueueFree();
	}
}
