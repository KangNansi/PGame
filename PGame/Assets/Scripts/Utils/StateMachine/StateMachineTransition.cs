using UnityEngine;
using System.Collections;
using System;

public class StateMachineTransition<T> where T : State
{
    public Func<bool> condition;
    public T source;
    public T target;

    public StateMachineTransition(T source, T target, Func<bool> condition)
    {
        this.source = source;
        this.target = target;
        this.condition = condition;
    }
}
