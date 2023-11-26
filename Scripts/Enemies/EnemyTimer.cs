using Godot;
using System;

public partial class EnemyTimer : Timer
{ 
    [Signal]
    public delegate void EnemyTimerTimeoutEventHandler(string enemyName);
    public string EnemyName { get; set; }

    public EnemyTimer(string enemyName) : base()
    {
        EnemyName = enemyName;
        Connect(Timer.SignalName.Timeout, new Callable(this, MethodName.TimerTimeout));
    }

    public void TimerTimeout()
    {
        EmitSignal(SignalName.EnemyTimerTimeout, EnemyName);
    }
}
