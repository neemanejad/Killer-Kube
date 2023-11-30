using Godot;
using System;

public partial class GameOverMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        ProcessMode = Control.ProcessModeEnum.Always;
        GetTree().Paused = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void RetryButtonUp()
	{
        foreach (var child in GetTree().Root.GetChildren())
        {
            child.QueueFree();
        }

        GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
        GetTree().Paused = false;

        QueueFree();
    }

	public void QuitButtonUp()
	{
        foreach (var child in GetTree().Root.GetChildren())
        {
            child.QueueFree();
        }

        GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
        GetTree().Paused = false;

        QueueFree();
    }
}
