using UnityEngine;
using System.Collections;

public class State
{
    private bool running = false;
    public bool Running {
        get {
            return running;
        }
    }

    public virtual void Awake() { }
    public virtual void Start() {
        running = true;
    }
    public virtual void Update() { }
    public virtual void End() { }

    protected void Stop()
    {
        running = false;
    }
}
