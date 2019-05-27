using UnityEngine;
using System.Collections;

public class DestroyWhenLeaveScreen : MonoBehaviour
{
    public float offset;

    // Update is called once per frame
    void Update()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(viewportPosition.x < -offset || viewportPosition.x > 1 + offset || viewportPosition.y < -offset || viewportPosition.y > 1 + offset)
        {
            Destroy(gameObject);
        }
    }
}
