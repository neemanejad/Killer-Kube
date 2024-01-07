using Godot;
using Nova.Scripts;
using System;

public partial class InventoryMenu : Control
{
    public PlayableCharacter Player { get; set; }
    public ItemList InventoryWeaponsList { get; set; }
    
    public override void _Ready()
    {
        ProcessMode = Control.ProcessModeEnum.Always;
        GetTree().Paused = true;
        Player = Globals.Target;

        InventoryWeaponsList = GetNode<ItemList>("InventoryPanel/InventoryTabs/Weapons");
        InventoryWeaponsList.Connect(ItemList.SignalName.ItemSelected, new Callable(this, MethodName.WeaponSelected));

        foreach (var weapon in Player.Weapons)
        {
            InventoryWeaponsList.AddItem(weapon.GetWeaponName());
        }
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

    public void WeaponSelected(int index)
    {
        Player.SelectWeapon(InventoryWeaponsList.GetItemText(index));
    }
}
