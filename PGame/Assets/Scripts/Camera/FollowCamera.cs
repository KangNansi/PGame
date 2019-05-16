using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public new Camera camera;
    [Range(0,1)]
    public float low;
    public Transform target;
    public Vector2 offset;

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
        transform.position = PixelPerfect(currentPosition);
    }

    private Vector3 PixelPerfect(Vector3 value)
    {
        return new Vector3(Mathf.FloorToInt(value.x * 64) / 64f, Mathf.FloorToInt(value.y * 64) / 64f, Mathf.FloorToInt(value.z * 64) / 64f);
    }
}
