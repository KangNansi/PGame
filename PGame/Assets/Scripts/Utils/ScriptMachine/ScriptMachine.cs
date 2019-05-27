using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptMachine : MonoBehaviour
{
    private static ScriptMachine instance;
    private static ScriptMachine Instance {
        get {
            if(instance == null)
            {
                instance = new GameObject("Script Machine").AddComponent<ScriptMachine>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    [SerializeField]
    private List<LonelyScript> scripts = new List<LonelyScript>();

    public static void Launch(LonelyScript script)
    {
        Instance.scripts.Add(script);
        script.Start();
    }

    void Update()
    {
        if (instance == null) instance = this;
        UpdateScripts();
        CheckEndedScripts();
    }

    private void UpdateScripts()
    {
        foreach(var script in scripts)
        {
            script.Update();
        }
    }

    private void CheckEndedScripts()
    {
        for(int i = 0; i < scripts.Count; i++)
        {
            if (!scripts[i].running)
            {
                scripts[i].End();
                scripts.RemoveAt(i);
                i--;
            }
        }
    }
}
