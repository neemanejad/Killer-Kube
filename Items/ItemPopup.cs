using Godot;
using System;

public partial class ItemPopup : ColorRect
{
	public String Description { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<Label>("ItemDescription").Text = Description;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
