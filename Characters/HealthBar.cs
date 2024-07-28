using Godot;
using System;

public partial class HealthBar : Node
{
	[Signal]
	public delegate void LostAllHealthEventHandler();
    [Signal]
    public delegate void HealthChangedEventHandler(int healthLost);

    public double Health { get; set; }
	public double MaxHealth { get; set; }
	private ProgressBar ProgressBar { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProgressBar = GetNode<ProgressBar>("ProgressBar");
		ProgressBar.Hide();
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

		ProgressBar.Show();

		EmitSignal(SignalName.HealthChanged);
	}

	public void AddHealthPoints(double amount)
	{
		Health += amount;

		if (Health >= MaxHealth)
		{
			ProgressBar.Hide();
		}

		EmitSignal(SignalName.HealthChanged);
	}

    public void OnHealthChanged(float value)
	{
		if (Health <= 0)
		{
			EmitSignal(SignalName.LostAllHealth);
		}
	}

	public void Show()
	{
		ProgressBar.Show();
	}

	public void Hide()
	{
		ProgressBar.Hide();
	}
}
