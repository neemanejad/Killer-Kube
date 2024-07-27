using Godot;
using System;

public partial class DamageNumber : Node2D
{
	private Timer VisibilityTimer;
	private Label DamageNumberLabel;
	public bool IsCriticalDamage;
	public double Number = 0;
	private Vector2 FadeOutDirection = new Vector2(GD.Randf(), GD.Randf()).Normalized();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		VisibilityTimer = GetNode<Timer>("VisibilityTimer");
		VisibilityTimer.Start();

        DamageNumberLabel = GetNode<Label>("DamageNumberLabel");
        DamageNumberLabel.Text = Number.ToString();

		// Change scale and color of damage number for crit damage
		if (IsCriticalDamage)
		{
			DamageNumberLabel.Scale *= 1.35f;
			DamageNumberLabel.LabelSettings = new LabelSettings();
			DamageNumberLabel.LabelSettings.FontColor = Colors.DarkOrange;
			DamageNumberLabel.LabelSettings.OutlineColor = Colors.Black;
			DamageNumberLabel.LabelSettings.OutlineSize = 6;
		}

        var DamageNumberLabelTween = DamageNumberLabel.CreateTween();
        DamageNumberLabelTween.TweenMethod(new Callable(this, MethodName.ChangeLabelAlpha), 1.0f, 0.0f, VisibilityTimer.WaitTime);
        DamageNumberLabelTween.SetEase(Tween.EaseType.In);
        DamageNumberLabelTween.Play();
    }

	private void ChangeLabelAlpha(float alpha)
	{
        var info = DamageNumberLabel.Modulate;
		info.A = alpha;
		DamageNumberLabel.Modulate = info;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += new Vector2(100 * FadeOutDirection.X * (float)delta, 100 * FadeOutDirection.Y * (float)delta);
	}

	public void VisibilityTimerTimeout()
	{
		QueueFree();
	}
}
