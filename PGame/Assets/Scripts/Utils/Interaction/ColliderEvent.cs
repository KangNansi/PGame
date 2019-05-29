using UnityEngine;
using System.Collections;
using System;

public class ColliderEvent : MonoBehaviour
{
    public LayerMask mask;

    public event Action<Collision2D> onEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((1<<collision.gameObject.layer & mask) != 0)
        {
            onEnter?.Invoke(collision);
        }
    }
}
