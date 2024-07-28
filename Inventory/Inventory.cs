using Godot;
using Killer_Kube.Scripts;
using System;
using System.Collections.Generic;

public partial class Inventory : Control
{
    public Dictionary<string, InventoryItem> InventoryItems { get; set; } = new Dictionary<string, InventoryItem>();
	public InventoryHUD InventoryHUD { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		InventoryHUD = GetNode<InventoryHUD>("InventoryHUD");
		ProcessMode = ProcessModeEnum.Always;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("inventory") || (Visible && Input.IsActionJustPressed("pause")))
        {
            Visible = !Visible;
            GetTree().Paused = Visible;
        }
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
			InventoryItems.Add(item.Name, new InventoryItem(InventoryItems.Count, item));
		}
		AddInventoryItemToHUD(item);
    }

	private void AddInventoryItemToHUD(Item item)
	{
		InventoryHUD.AddInventoryItem(InventoryItems[item.Name]);
	}

	public void ToggleInventoryVisibility()
	{
		InventoryHUD.Visible = !InventoryHUD.Visible;
	}
}
