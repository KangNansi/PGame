using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public new Camera camera;
    [Range(0,1)]
    public float low;
    public Transform target;
    public Vector2 offset;
    public Bounds bounds;

    private Vector2 targetViewportPosition;

    private void Start()
    {
        targetViewportPosition = new Vector2(0.5f, low);
    }

    void Update()
    {
        Vector3 currentPosition = camera.ViewportToWorldPoint(targetViewportPosition);
        Vector3 targetWorldPosition = PixelPerfect(target.transform.position + (Vector3)offset);
        Vector3 move = targetWorldPosition - currentPosition;
        move.z = 0;

        currentPosition = currentPosition + move;
        Vector3 newPosition = currentPosition;

        Vector3 diff = newPosition - transform.position;
        Vector3 lb = camera.ViewportToWorldPoint(Vector3.zero) + diff;
        Vector3 rt = camera.ViewportToWorldPoint(Vector3.one) + diff;


        float left = Mathf.Max(bounds.min.x - lb.x, 0);
        float right = Mathf.Max(rt.x - bounds.max.x, 0);
        float bottom = Mathf.Max(bounds.min.y - lb.y, 0);
        float top = Mathf.Max(rt.y - bounds.max.y, 0);
        

        Debug.Log("Top: " + top + "\nBottom: " + bottom);

        newPosition += new Vector3(left - right, bottom - top);

        transform.position = PixelPerfect(newPosition);
    }

    private Vector3 PixelPerfect(Vector3 value)
    {
        return new Vector3(Mathf.FloorToInt(value.x * 64) / 64f, Mathf.FloorToInt(value.y * 64) / 64f, Mathf.FloorToInt(value.z * 64) / 64f);
    }
}
