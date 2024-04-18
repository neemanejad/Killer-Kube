using Godot;
using Killer_Kube.Scripts;
using System;
using System.Collections.Generic;

public partial class ItemPopupHUD : Node
{
    Timer PopupTimer;
    PackedScene PopupPackedScene = ResourceLoader.Load<PackedScene>("res://Scenes/ItemPopup.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PopupTimer = GetNode<Timer>("ItemPopupTimer");
        PopupTimer.Connect(Timer.SignalName.Timeout, new Callable(this, MethodName.RemovePopup));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void AddItem(Item item)
    {
        AddPopup(item);
    }

    public void AddPopup(Item item)
    {
        if (!PopupTimer.IsStopped())
        {
            RemovePopup();
        }

        
        ItemPopup popup = PopupPackedScene.Instantiate<ItemPopup>();
        popup.Description = item.Description;
        popup.Name = "Popup";
        AddChild(popup);
        popup.ZIndex = 4096;
        popup.ZAsRelative = false;

        PopupTimer.Start();
    }


    public void RemovePopup()
    {
        PopupTimer.Stop();

        ItemPopup popup = GetNode<ItemPopup>("Popup");
        popup.Visible = false;
        popup.Free();
    }
}
