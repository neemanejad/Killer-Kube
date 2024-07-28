using Godot;
using Killer_Kube.Scripts;
using System;

public partial class InventoryHUD : Control
{
	ItemList InventoryItemsList { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InventoryItemsList = GetNode<ItemList>("InventoryItemsList");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }

	public void AddInventoryItem(InventoryItem item)
	{
		if (InventoryItemsList.ItemCount == item.OrderReceived)
		{
            InventoryItemsList.AddItem($"{item.Item.Name} {$"({item.Count})"}");
            InventoryItemsList.SetItemTooltipEnabled(item.OrderReceived, true);
            InventoryItemsList.SetItemTooltip(item.OrderReceived, item.Item.Description);
		}
        else
        {
            InventoryItemsList.SetItemText(item.OrderReceived, $"{item.Item.Name} {$"({item.Count})"}");
        }
    }
}
