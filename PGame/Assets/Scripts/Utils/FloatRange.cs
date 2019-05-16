using UnityEngine;
using System.Collections;

[System.Serializable]
public class FloatRange
{
    public float min;
    public float max;

    public float Pick()
    {
        return Random.Range(min, max);
    }
}
