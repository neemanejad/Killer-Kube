using Godot;
using Killer_Kube.Scripts;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
    public Dictionary<string, InventoryItem> InventoryItems { get; set; } = new Dictionary<string, InventoryItem>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
