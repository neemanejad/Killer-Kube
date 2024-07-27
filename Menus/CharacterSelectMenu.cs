using Godot;
using Nova.Scripts;
using System;

public partial class CharacterSelectMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CharacterOptionButtonUp(PackedScene character)
	{
        Globals.SelectedCharacter = character;

		// Remove this as a child
		var root = GetTree().Root;
		root.RemoveChild(this);

        // Instantiate level with appropriate data
        var level = Globals.SelectedLevel.Instantiate<Level>();
		level.PlayerScene = Globals.SelectedCharacter;

		// Add level as child of root
		root.AddChild(level);

		// Free this node
		QueueFree();
	}

	public void BackButtonUp()
	{
		GetTree().ChangeSceneToFile("res://Menus/LevelSelectMenu.tscn");
	}
}
