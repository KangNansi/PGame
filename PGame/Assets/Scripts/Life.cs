using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class LifeBehaviour {
    public Action onHit;
}

public class Life : MonoBehaviour
{
    public float maxLife;
    public float CurrentLife { get; private set; }

    public event Action onDie;
    public event Action onHit;
    public UnityEvent onDieEvent;

    public static Action<LayerMask> anyHit;

    public bool isInvicible = false;

    public LayerMask invicibilityLayer { get; set; }

    private void Start()
    {
        CurrentLife = maxLife;
    }

    public void Hit(GameObject source, float value)
    {
        if (isInvicible) return;
        CurrentLife -= value;
        if (CurrentLife <= 0)
        {
            onDieEvent?.Invoke();
            onDie?.Invoke();
            CurrentLife = 0;
        }
        onHit?.Invoke();
        anyHit?.Invoke(source.layer);
    }
}
