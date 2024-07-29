using Godot;
using Killer_Kube.Inventory;
using Killer_Kube.Scripts;
using Nova.Scripts;
using Nova.Scripts.Character;
using System;
using System.Collections.Generic;

public partial class InventoryHUD : Control
{
	public ItemList InventoryItemsList { get; set; }
	public ItemList StatsList { get; set; }
	public Dictionary<int, PlayerStat> Stats = new Dictionary<int, PlayerStat>()
	{
		{ 0, new PlayerStat("Health", (PlayableCharacter character) => $"Health: {Math.Ceiling(character.HealthBar.Health)}/{ Math.Ceiling(character.HealthBar.MaxHealth)}") },
		{ 1, new PlayerStat("Speed", (PlayableCharacter character) => $"Speed: {Math.Ceiling(character.Speed)}" ) },
		{ 2, new PlayerStat("Fire Rate", (PlayableCharacter character) => $"Fire rate: { Math.Ceiling(60 / character.Weapon.WeaponHitRate)} BPM") }
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InventoryItemsList = GetNode<ItemList>("InventoryItemsList");
		StatsList = GetNode<ItemList>("StatsList");
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
