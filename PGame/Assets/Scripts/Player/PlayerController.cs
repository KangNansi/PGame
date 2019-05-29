using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected float h, v;
    protected float h2, v2;

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

    public void SetAxis2(float h, float v)
    {
        this.h2 = h;
        this.v2 = v;
    }

    public virtual void A() { }

    public virtual void B() { }
    public virtual void BUp() { }

    public virtual void C() { }
    public virtual void CUp() { }
}
