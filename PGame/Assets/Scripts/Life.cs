using UnityEngine;
using System.Collections;
using System;

public class Life : MonoBehaviour
{
    public float maxLife;
    private float currentLife;
    
    public event Action onDie;

    private void Start()
    {
        currentLife = maxLife;
    }

    public void Hit(float value)
    {
        currentLife -= value;
        if (currentLife <= 0)
        {
            onDie?.Invoke();
            currentLife = 0;
        }
    }
}
