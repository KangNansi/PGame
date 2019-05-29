using UnityEngine;
using System.Collections;

public struct Timer
{

    private float last;
    private bool stopped;
    public bool Stopped {
        get {
            return stopped;
        }
    }

    public float Elapsed {
        get {
            if (stopped) return 0;
            return Time.time - last;
        }
    }

    public void Restart()
    {
        stopped = false;
        last = Time.time;
    }

    public void Stop()
    {
        stopped = true;
    }

    public static Timer operator-(Timer timer, float value)
    {
        timer.last += value;
        return timer;
    }
}
