using UnityEngine;
using UnityEditor;

public class FolderPref : EditorPrefsGUI<string>
{
    public FolderPref(string id) : base(id)
    {
    }

    public override void Layout()
    {
        base.Layout();

        if (GUILayout.Button(value))
        {
            string newValue = EditorUtility.OpenFolderPanel("Select folder", value, "");
            newValue = newValue.Substring(Application.dataPath.Length);
            if(newValue != null && newValue.Length > 0 && newValue != value)
            {
                value = newValue;
                Save();
                Change();
            }
        }
    }

    protected override void Load()
    {
        value = EditorPrefs.GetString(id);
        Change();
    }

    protected override void Save()
    {
        EditorPrefs.SetString(id, value);
    }
}