using UnityEngine;
using System.Collections;

public abstract class LonelyScript
{
    public bool running { get; protected set; }

    public virtual void Start()
    {
        running = true;
    }

    public virtual void Update() { }
    public virtual void End() { }

    public void Kill()
    {
        running = false;
    }
}
