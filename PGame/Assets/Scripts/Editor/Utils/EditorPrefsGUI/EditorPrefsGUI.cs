using UnityEngine;
using UnityEditor;
using System;

public abstract class EditorPrefsGUI<T>
{
    protected T value;
    protected string id;
    protected bool isLoaded;

    public event Action<T> onValueChanged;

    public EditorPrefsGUI(string id){
        this.id = id;
    }

    ~EditorPrefsGUI()
    {
        Save();
    }

    public T Get()
    {
        return value;
    }

    public virtual void Layout()
    {
        if (!isLoaded) Load();
    }
    protected abstract void Load();
    protected abstract void Save();
    protected void Change()
    {
        onValueChanged?.Invoke(value);
    }
}