using Godot;
using Nova.Scripts;
using System;

public partial class InventoryMenu : Control
{
    public PlayableCharacter Character { get; set; }
    public override void _Ready()
    {
        ProcessMode = Control.ProcessModeEnum.Always;
        GetTree().Paused = true;
        Character = Globals.Target;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("pause") || Input.IsActionJustPressed("inventory"))
        {
            QueueFree();
            GetTree().Paused = false;
        }
    }
}
