using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowCamera))]
public class FollowCameraEditor : EditorBase<FollowCamera>
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        Handles.DrawPolyLine(self.bounds.min,
            new Vector3(self.bounds.max.x, self.bounds.min.y),
            self.bounds.max,
            new Vector3(self.bounds.min.x, self.bounds.max.y),
            self.bounds.min);

        Vector3 left = self.bounds.min + new Vector3(0, self.bounds.size.y / 2f);
        left = Handles.FreeMoveHandle(left, Quaternion.identity, 0.1f, Vector3.one, Handles.DotHandleCap);
        Vector3 right = self.bounds.max + new Vector3(0, -self.bounds.size.y / 2f);
        right = Handles.FreeMoveHandle(right, Quaternion.identity, 0.1f, Vector3.one, Handles.DotHandleCap);

        Vector3 down = self.bounds.min + new Vector3(self.bounds.size.x / 2f, 0);
        down = Handles.FreeMoveHandle(down, Quaternion.identity, 0.1f, Vector3.one, Handles.DotHandleCap);
        Vector3 up = self.bounds.max + new Vector3(-self.bounds.size.x / 2f, 0);
        up = Handles.FreeMoveHandle(up, Quaternion.identity, 0.1f, Vector3.one, Handles.DotHandleCap);

        self.bounds.min = new Vector3(Mathf.Floor(left.x), Mathf.Floor(down.y));
        self.bounds.max = new Vector3(Mathf.Floor(right.x), Mathf.Floor(up.y));

        self.bounds.extents = new Vector3(Mathf.Abs(self.bounds.extents.x),
            Mathf.Abs(self.bounds.extents.y),
            Mathf.Abs(self.bounds.extents.z));

        Handles.color = Color.white;
    }
}