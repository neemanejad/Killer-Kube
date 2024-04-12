using Godot;
using Killer_Kube.Scripts;
using System;

public partial class HUD : Control
{
	private Timer PopupTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PopupTimer = GetNode<Timer>("PopupTimer");
		PopupTimer.Connect(Timer.SignalName.Timeout, new Callable(this, MethodName.RemovePopup));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddPopup(Item item)
	{
		if (!PopupTimer.IsStopped()){
			RemovePopup();
		}

		PackedScene popupPackedScene = ResourceLoader.Load<PackedScene>("res://Scenes/ItemPopup.tscn");
        ItemPopup popup = popupPackedScene.Instantiate<ItemPopup>();
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
