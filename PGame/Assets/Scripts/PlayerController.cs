using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected float h, v;

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
