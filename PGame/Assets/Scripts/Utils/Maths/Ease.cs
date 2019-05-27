using UnityEngine;
using System.Collections;

public static class NormalizedFunctions
{
    public static float Sin(float value)
    {
        value = Mathf.Clamp01(value);
        return Mathf.Sin(value * Mathf.PI);
    }

    public static float Cubic(float value)
    {
        value = Mathf.Clamp01(value);
        float v = ((value - 0.5f) * 2f);
        return 1-(v * v);
    }

    public enum Function
    {
        Sin,
        Cubic
    }

    public static float Get(Function function, float value)
    {
        switch (function)
        {
            case Function.Sin: return Sin(value);
            case Function.Cubic: return Cubic(value);
            default: return 0;
        }
    }
}
