using Godot;
using Killer_Kube.Scripts;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
    public Dictionary<string, InventoryItem> InventoryItems { get; set; } = new Dictionary<string, InventoryItem>();
	public InventoryHUD InventoryHUD { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		InventoryHUD = GetNode<InventoryHUD>("InventoryHUD");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void AddInventoryItem(Item item)
	{
		// Check if item already exists in inventory
		if (InventoryItems.ContainsKey(item.Name))
		{
			InventoryItems[item.Name].Count++;
		}
        else
        {
			InventoryItems.Add(item.Name, new InventoryItem(item));
        }
    }
}
