using Godot;
using Nova.Scripts;

public partial class Character : CharacterBody2D, ICharacter
{
    [Export]
    public int Speed { get; set; } = 400;
    [Export]
    public PackedScene WeaponScene { get; set; }
    public Weapon Weapon { get; set; }
    public Marker2D WeaponLocation { get; set; }
    public HealthBar HealthBar { get; set; }
    public Marker2D HealthBarLocation { get; set; }
    [Export]
    public double HealthPoints { get; set; } = 100;

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
        Weapon.EnableWeaponAction();
    }

    public void DisableAttack()
    {
        Weapon.DisableWeaponAction();
    }

    public virtual void LostAllHealth()
    {
        QueueFree();
    }
}
