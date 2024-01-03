using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class PrefabPainter : EditorWindow
{
    private FolderPref prefabDir = new FolderPref("prefab_dir");
    private List<GameObject> prefabs = new List<GameObject>();


    [MenuItem("Tools/Prefab Painter")]
    static void Open()
    {
        var w = GetWindow<PrefabPainter>();
        w.Show();
    }

    private void OnGUI()
    {
        prefabDir.Layout();
        foreach(var prefab in prefabs)
        {
            EditorGUILayout.LabelField(prefab.name);
            Rect rect = GUILayoutUtility.GetAspectRect(1f, GUILayout.Width(64));
            var preview = AssetPreview.GetAssetPreview(prefab);
            if (preview) EditorGUI.DrawPreviewTexture(rect, preview);
        }
    }

    private void OnEnable()
    {
        prefabDir.onValueChanged += onPrefabDirChanged;
    }

    void onPrefabDirChanged(string dir)
    {
        DirectoryInfo prefabDir = new DirectoryInfo(Application.dataPath + "/" + dir);
        prefabs.Clear();
        foreach(var file in prefabDir.GetFiles())
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/" + file.FullName.Substring(Application.dataPath.Length));
            if(prefab && PrefabUtility.GetPrefabAssetType(prefab) != PrefabAssetType.NotAPrefab)
            {
                prefabs.Add(prefab);
            }
        }
    }
}