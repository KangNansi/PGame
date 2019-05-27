using UnityEngine;
using System.Collections;

public class InactiveWhenLeaveScreen : MonoBehaviour
{
    public float offset;

    private void Start()
    {
        ScreenManager.AddInactive(this);
    }

    private void OnDestroy()
    {
        ScreenManager.RemoveInactive(this);
    }
}
