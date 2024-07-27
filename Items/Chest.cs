using Godot;
using Killer_Kube.Scripts;
using Microsoft.CodeAnalysis;
using System;

public partial class Chest : Area2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect(SignalName.BodyEntered, new Callable(this, MethodName.OnBodyEntered));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	// TODO: Actually implment this function with something useful
	public void OnBodyEntered(Node2D body)
	{
        if (body.IsInGroup("Player"))
        {
			Optional<Item> item = LootPool.GetRandomItemFromPool();
			if (item.HasValue)
			{
				(body as PlayableCharacter).AddItem(item.Value);
			}
			QueueFree();
        }
    }
}
