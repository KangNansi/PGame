using UnityEngine;
using System.Collections;

public struct Cooldown
{
    private float last;
    public float necessary;
    public float max;
    public bool Ready {
        get {
            return Time.time - last > necessary;
        }
    }

    public float Value {
        get {
            return Mathf.Max(max, Time.time - last);
        }
    }

    public bool Use()
    {
        if (Ready)
        {
            float elapsed = Time.time - last;
            if(elapsed > max)
            {
                last = Time.time - (max - necessary);
            }
            else
            {
                last += necessary;
            }
            return true;
        }
        return false;
    }
}
