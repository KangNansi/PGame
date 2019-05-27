using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected float h, v;

    [HideInInspector, NonSerialized]
    public float a;
    public bool aDown { get; set; }
    [HideInInspector, NonSerialized]
    public float b;
    public bool bDown { get; set; }
    [HideInInspector, NonSerialized]
    public float c;
    public bool cDown { get; set; }

    public void SetAxis(float h, float v)
    {
        this.h = h;
        this.v = v;
    }

    public virtual void A() { }

    public virtual void B() { }
    public virtual void BUp() { }

    public virtual void C() { }
}
