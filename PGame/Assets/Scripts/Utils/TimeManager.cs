using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    private static TimeManager Instance {
        get {
            if(instance == null)
            {
                instance = new GameObject("TimeManager").AddComponent<TimeManager>();
            }
            return instance;
        }
    }

    protected class DelayedAction
    {
        public float time;
        public float started;
        public Action action;

        public bool Ended {
            get {
                return Time.time - started > time;
            }
        }
    }
    private List<DelayedAction> delayedActions = new List<DelayedAction>();

    public static void Delay(float time, Action action)
    {
        Instance.delayedActions.Add(new DelayedAction()
        {
            time = time,
            action = action,
            started = Time.time
        });
    }

    private void Update()
    {
        for(int i = 0; i < delayedActions.Count; i++)
        {
            if (delayedActions[i].Ended)
            {
                delayedActions[i].action?.Invoke();
                delayedActions.RemoveAt(i);
                i--;
            }
        }
    }
}
