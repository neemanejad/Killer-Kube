using Godot;
using System;

public partial class LevelSelectMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void LevelButtonUp()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Menus/CharacterSelectMenu.tscn");
    }

	public void BackButtonUp()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
	}
}
