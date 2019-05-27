using UnityEngine;
using System.Collections;
using System;

public class Life : MonoBehaviour
{
    public float maxLife;
    public float CurrentLife { get; private set; }

    public event Action onDie;
    public event Action onHit;

    public bool isInvicible = false;

    private void Start()
    {
        CurrentLife = maxLife;
    }

    public void Hit(float value)
    {
        if (isInvicible) return;
        CurrentLife -= value;
        if (CurrentLife <= 0)
        {
            onDie?.Invoke();
            CurrentLife = 0;
        }
        onHit?.Invoke();
    }
}
