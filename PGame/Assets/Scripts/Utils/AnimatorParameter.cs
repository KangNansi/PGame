using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimatorParameter
{
    public string name;
    private Animator animator;
    private int id;

    private bool initialized = false;
    public bool Initialized {
        get {
            return initialized;
        }
    }

    public void Initialize(Animator target)
    {
        this.animator = target;
        id = Animator.StringToHash(name);
        initialized = true;
    }

    public void SetBool(bool value)
    {
        animator.SetBool(id, value);
    }

    public void SetTrigger()
    {
        animator.SetTrigger(id);
    }

    public void SetFloat(float value)
    {
        animator.SetFloat(id, value);
    }
}
