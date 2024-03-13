using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public static List<Timer> timers = new();

    public float time { get; set; }
    public bool repeating { get; set; }
    public bool active { get; set; }
    private float maxTimer { get; set; }

    public delegate void TimerEnd();
    public event TimerEnd OnTimerEnd;

    public static Timer AddTimer(float time, bool repeating = false)
    {
        var timer = new Timer()
        {
            maxTimer = time,
            time = time,
            repeating = repeating,
            active = true
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
        if (!active)
            return;

        this.time -= time;

        if (this.time > 0)
            return;

        EndTimer();
        if (repeating)
        {
            this.time = maxTimer;
        }
        else
        {
            this.active = false;
        }
    }
}
