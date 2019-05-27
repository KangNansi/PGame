using UnityEngine;
using System.Collections;

public class PointDetector2D : MonoBehaviour
{
    public LayerMask layerMask;

    private Collider2D overlapping;
    public bool IsOverlapped {
        get {
            return overlapping != null;
        }
    }

    public float LastOverlapped { get; private set; }

    // Update is called once per frame
    void Update()
    {
        overlapping = Physics2D.OverlapPoint(transform.position, layerMask);
        if (overlapping)
        {
            LastOverlapped = 0;
        }
        else
        {
            LastOverlapped += Time.deltaTime;
        }
    }
}
