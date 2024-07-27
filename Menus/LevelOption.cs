using Godot;
using System;

public partial class LevelOption : Button
{
	[Export]
	public PackedScene Level { get; set; }
	[Signal]
	public delegate void LevelOptionSelectedEventHandler(PackedScene level);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void LevelOptionButtonUp()
	{
		EmitSignal(SignalName.LevelOptionSelected, Level);
	}

}
