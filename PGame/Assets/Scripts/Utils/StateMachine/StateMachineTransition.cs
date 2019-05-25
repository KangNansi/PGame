using UnityEngine;
using System.Collections;
using System;

public class StateMachineTransition
{
    public Func<bool> condition;
    public State source;
    public State target;

    public StateMachineTransition(State source, State target, Func<bool> condition)
    {
        this.source = source;
        this.target = target;
        this.condition = condition;
    }
}
