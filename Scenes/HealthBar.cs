using Godot;
using System;

public partial class HealthBar : Node
{
	[Signal]
	public delegate void LostAllHealthEventHandler();

	public double Health { get; set; }
	public double MaxHealth { get; set; }
	private ProgressBar ProgressBar { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProgressBar = GetNode<ProgressBar>("ProgressBar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ProgressBar.Value = Health;
		ProgressBar.MaxValue = MaxHealth;
	}

	public void RemoveHealthPoints(double amount)
	{
		Health -= amount;
	}

	public void AddHealthPoints(double amount)
	{
		Health += amount;
	}

    public void OnHealthChanged()
	{
		if (Health <= 0)
		{
			EmitSignal(SignalName.LostAllHealth);
			QueueFree();
		}
	}
}
