using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine
{
    private State current;

    public List<StateMachineTransition> transitions = new List<StateMachineTransition>();

    public void SetState(State state)
    {
        if(current != null)
        {
            current.End();
        }
        current = state;
        if(current != null)
        {
            current.Start();
        }
    }

    public void Update()
    {
        if(current != null)
        {
            current.Update();

            if (!current.Running)
            {
                var transition = transitions.Find(t => t.source == current && t.condition == null);
                SetState(transition.target);
            }
            else
            {
                foreach (var transition in transitions)
                {
                    if (transition.condition == null) continue;
                    if (transition.source == current && transition.condition())
                    {
                        SetState(transition.target);
                        break;
                    }
                }
            }
        }
    }
}
