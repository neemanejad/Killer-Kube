using Godot;
using System;

public partial class Controller : Node2D
{
    private Character Character { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Character = GetParent().GetNode<Character>("Character");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Character.ProcessMovementInput(delta);
        Character.ProcessMouseMovement(delta);
    }

    public void PlayerLostAllHealth()
    {
        QueueFree();
    }
}