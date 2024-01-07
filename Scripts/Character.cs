using Godot;
using Nova.Scripts;

public partial class Character : CharacterBody2D, ICharacter
{
    [Export]
    public float Speed { get; set; } = 500;
    public Weapon CurrentWeapon { get; set; }
    public Marker2D WeaponLocation { get; set; }
    public HealthBar HealthBar { get; set; }
    public Marker2D HealthBarLocation { get; set; }
    [Export]
    public double HealthPoints { get; set; } = 100;

    [Signal]
    public delegate void OnLostAllHealthEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void EnableAttack()
    {
        CurrentWeapon.EnableWeaponAction();
    }

    public void DisableAttack()
    {
        CurrentWeapon.DisableWeaponAction();
    }

    public virtual void LostAllHealth()
    {
        EmitSignal(SignalName.OnLostAllHealth);
        QueueFree();
    }
}
