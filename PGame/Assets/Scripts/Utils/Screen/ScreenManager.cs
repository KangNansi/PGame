using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;
    private static ScreenManager Instance {
        get {
            if(instance == null)
            {
                instance = new GameObject("ScreenManager").AddComponent<ScreenManager>();
            }
            return instance;
        }
    }

    private List<InactiveWhenLeaveScreen> inactives = new List<InactiveWhenLeaveScreen>();

    public static void AddInactive(InactiveWhenLeaveScreen inactive)
    {
        if (Instance.inactives.Contains(inactive)) return;
        Instance.inactives.Add(inactive);
    }

    public static void RemoveInactive(InactiveWhenLeaveScreen inactive)
    {
        instance?.inactives.Remove(inactive);
    }

    void Update()
    {
        foreach(var inactive in inactives)
        {
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(inactive.transform.position);
            if (viewportPosition.x < -inactive.offset || viewportPosition.x > 1 + inactive.offset || viewportPosition.y < -inactive.offset || viewportPosition.y > 1 + inactive.offset)
            {
                inactive.gameObject.SetActive(false);
            }
            else
            {
                inactive.gameObject.SetActive(true);
            }
        }
    }
}
