using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<T> where T : State
{
    public T Current { get; private set; }

    public bool DebugMode { get; set; }

    public List<StateMachineTransition<T>> transitions = new List<StateMachineTransition<T>>();

    public void SetState(T state)
    {
        if(Current != null)
        {
            Current.End();
        }
        
        Current = state;
        if(Current != null)
        {
            if (DebugMode) Debug.Log(state.GetType());
            Current.Start();
        }
    }

    public void Update()
    {
        if(Current != null)
        {
            Current.Update();

            if (!Current.Running)
            {
                var transition = transitions.Find(t => t.source == Current && t.condition == null);
                SetState(transition.target);
            }
            else
            {
                foreach (var transition in transitions)
                {
                    if (transition.condition == null) continue;
                    if (transition.source == Current && transition.condition())
                    {
                        SetState(transition.target);
                        break;
                    }
                }
            }
        }
    }
}
