using UnityEngine;
using UnityEditor;

public class EditorBase<T> : Editor where T : class
{
    protected T self;

    private void OnEnable()
    {
        self = target as T;
    }
}