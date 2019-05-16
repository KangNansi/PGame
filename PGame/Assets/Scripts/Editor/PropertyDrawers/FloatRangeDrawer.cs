using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FloatRange))]
public class FloatRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty min = property.FindPropertyRelative("min");
        SerializedProperty max = property.FindPropertyRelative("max");

        EditorGUI.BeginChangeCheck();

        float[] values = new float[2];
        values[0] = min.floatValue;
        values[1] = max.floatValue;
        EditorGUI.MultiFloatField(position, label, new GUIContent[] { new GUIContent("-"), new GUIContent("+") }, values);

        if (EditorGUI.EndChangeCheck())
        {
            min.floatValue = values[0];
            max.floatValue = values[1];
        }
        if (min.floatValue > max.floatValue) min.floatValue = max.floatValue;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}