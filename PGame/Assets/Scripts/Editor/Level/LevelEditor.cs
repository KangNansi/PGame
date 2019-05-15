using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    private bool drawGrid;
    private Level level;

    private void OnEnable()
    {
        level = target as Level;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (drawGrid)
        {
            DrawGrid();
        }

        Handles.BeginGUI();

        drawGrid = EditorGUILayout.Toggle("Draw Grid", drawGrid);

        Handles.EndGUI();

        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        Vector2 position = ray.origin;
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(controlId);

        switch (Event.current.GetTypeForControl(controlId))
        {
            case EventType.MouseDown:
                level[Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y)].type = 1;
                break;
        }
    }

    private void DrawGrid()
    {
        Handles.color = new Color(1, 1, 1, 0.3f);

        Rect sceneViewPosition = SceneView.currentDrawingSceneView.position;
        Vector2 lb = HandleUtility.GUIPointToWorldRay(Vector2.zero).origin;
        Vector2 rt = HandleUtility.GUIPointToWorldRay(sceneViewPosition.size).origin;

        for (int x = Mathf.FloorToInt(lb.x); x < Mathf.CeilToInt(rt.x); x++)
        {
            Handles.DrawLine(new Vector3(x, lb.y), new Vector3(x, rt.y));
        }

        for (int y = Mathf.FloorToInt(rt.y); y < Mathf.CeilToInt(lb.y); y++)
        {
            Handles.DrawLine(new Vector3(lb.x, y), new Vector3(rt.x, y));
        }

        Handles.color = Color.white;
    }
}