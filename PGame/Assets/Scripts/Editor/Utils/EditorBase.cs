using UnityEngine;
using UnityEditor;

public class EditorBase<T> : Editor where T : class
{
    protected T self;

    protected virtual void OnEnable()
    {
        self = target as T;
    }
}