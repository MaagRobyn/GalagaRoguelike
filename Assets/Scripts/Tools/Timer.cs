using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public static List<Timer> timers = new();

    public float Time { get; private set; }
    public bool Repeating { get; private set; }
    public bool Active { get; private set; }
    private float MaxTimer { get; set; }

    public delegate void TimerEnd();
    public event TimerEnd OnTimerEnd;

    public static Timer AddTimer(float time, bool repeating = false)
    {
        var timer = new Timer()
        {
            MaxTimer = time,
            Time = time,
            Repeating = repeating,
            Active = true
        };
        timers.Add(timer);
        return timer;
    }
    protected virtual void EndTimer()
    {
        OnTimerEnd?.Invoke();
    }
    public void TickTimer(float time)
    {
        if (!Active)
            return;

        this.Time -= time;

        if (this.Time > 0)
            return;

        EndTimer();
        if (Repeating)
        {
            this.Time = MaxTimer;
        }
        else
        {
            this.Active = false;
        }
    }
}
