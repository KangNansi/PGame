using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(RuleTile))]
public class RuleTileEditor : EditorBase<RuleTile>
{
    private ReorderableList ruleList;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (self == null) return;
        if (self.rules == null) self.rules = new List<Rule>();
        ruleList = new ReorderableList(self.rules, typeof(Rule), true, true, true, true);
        ruleList.elementHeight = 64;

        if (ruleList == null) return;
        ruleList.drawHeaderCallback += DrawHeader;
        ruleList.drawElementCallback += DrawElement;

        ruleList.onAddCallback += AddItem;
        ruleList.onRemoveCallback += RemoveItem;
    }

    private void OnDisable()
    {
        if (ruleList == null) return;
        ruleList.drawHeaderCallback -= DrawHeader;
        ruleList.drawElementCallback -= DrawElement;

        ruleList.onAddCallback -= AddItem;
        ruleList.onRemoveCallback -= RemoveItem;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        self.sprite = (Sprite)EditorGUILayout.ObjectField(self.sprite, typeof(Sprite), false);
        self.colliderType = (RuleTile.ColliderType)EditorGUILayout.EnumPopup(self.colliderType);
        ruleList.DoLayoutList();
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "Rules");
    }

    private void RemoveItem(ReorderableList list)
    {
        self.rules.RemoveAt(list.index);
        EditorUtility.SetDirty(target);
    }

    private void AddItem(ReorderableList list)
    {
        self.rules.Add(new Rule());
        EditorUtility.SetDirty(target);
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        EditorGUI.BeginChangeCheck();
        Rect spritePicker = new Rect(rect.x+4, rect.y+4, 56, 56);
        self.rules[index].Sprite = (Sprite)EditorGUI.ObjectField(spritePicker, self.rules[index].Sprite, typeof(Sprite), false);
        Rect ruleEditor = new Rect(rect.x + 68, rect.y+4, 56, 56);
        RuleEditor(ruleEditor, self.rules[index]);

        Rect paramRect = new Rect(rect.x + 132, rect.y + 4, rect.size.x - 128, 16);
        
        Rule rule = self.rules[index];
        rule.flipX = EditorGUI.Toggle(paramRect, "FlipX", rule.flipX);
        paramRect.position += new Vector2(0, 16);
        rule.flipY = EditorGUI.Toggle(paramRect, "FlipY", rule.flipY);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void RuleEditor(Rect rect, Rule rule)
    {
        // Contour
        Handles.DrawLine(rect.min, new Vector2(rect.min.x, rect.max.y));
        Handles.DrawLine(rect.min, new Vector2(rect.max.x, rect.min.y));
        Handles.DrawLine(rect.max, new Vector2(rect.min.x, rect.max.y));
        Handles.DrawLine(rect.max, new Vector2(rect.max.x, rect.min.y));

        // Inside
        Handles.DrawLine(new Vector2(rect.min.x + rect.size.x*0.33f, rect.min.y), new Vector2(rect.min.x + rect.size.x*0.33f, rect.max.y));
        Handles.DrawLine(new Vector2(rect.min.x + rect.size.x * 0.66f, rect.min.y), new Vector2(rect.min.x + rect.size.x * 0.66f, rect.max.y));
        Handles.DrawLine(new Vector2(rect.min.x, rect.min.y + rect.size.y * 0.33f), new Vector2(rect.max.x, rect.min.y + rect.size.y * 0.33f));
        Handles.DrawLine(new Vector2(rect.min.x, rect.min.y + rect.size.y * 0.66f), new Vector2(rect.max.x, rect.min.y + rect.size.y * 0.66f));

        int controlId = EditorGUIUtility.GetControlID(FocusType.Passive);
        Vector2 mousePosition = Event.current.mousePosition;

        for(int i = 0; i < 9; i++)
        {
            
            Vector2 pos = new Vector2(rect.min.x + (i % 3) * (rect.size.x / 3f), rect.min.y + (i / 3) * (rect.size.y / 3f));
            Rect ruleRect = new Rect(pos, new Vector2(rect.size.x / 3f, rect.size.y / 3f));
            if(rule.rules[i] == 1) // Not
            {
                Handles.color = Color.red;
                Handles.DrawSolidDisc(ruleRect.center, Vector3.forward, Mathf.Min(ruleRect.size.x, ruleRect.size.y) * 0.4f);
                Handles.color = Color.white;
            }
            else if(rule.rules[i] == 2 || i == 4) // Has
            {
                Handles.DrawSolidRectangleWithOutline(ruleRect, Color.green, Color.green);
            }
            else if(rule.rules[i] == 3)
            {
                Handles.DrawAAConvexPolygon(ruleRect.min + new Vector2(0, ruleRect.size.y), ruleRect.max, ruleRect.min + new Vector2(ruleRect.size.x / 2f, 0));
            }
            else if (rule.rules[i] == 4)
            {
                Handles.color = Color.red;
                Handles.DrawAAConvexPolygon(ruleRect.min + new Vector2(0, ruleRect.size.y), ruleRect.max, ruleRect.min + new Vector2(ruleRect.size.x / 2f, 0));
                Handles.color = Color.white;
            }

            if (i == 4) continue;

            switch (Event.current.GetTypeForControl(controlId))
            {
                case EventType.MouseDown:
                    if (ruleRect.Contains(mousePosition))
                    {
                        if (Event.current.button == 0)
                        {
                            int currentValue = rule.rules[i];
                            if(currentValue == 0)
                            {
                                rule.rules[i] = 2;
                            }
                            else
                            {
                                rule.rules[i] = currentValue + 1;
                                if (rule.rules[i] > 4) rule.rules[i] = 1;
                            }
                            //rule.rules[i] = (rule.rules[i] == 0) ? 2 : (rule.rules[i]-1)%3+1;
                        }
                        else if (Event.current.button == 1)
                        {
                            rule.rules[i] = 0;
                        }
                        Event.current.Use();
                        GUI.changed = true;
                    }
                    break;
            }
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
    {
        RuleTile t = target as RuleTile;
        Texture2D tex = new Texture2D(width, height);
        /*if(t.sprite != null)
        {
            EditorUtility.CopySerialized(AssetPreview.GetAssetPreview(t.sprite), tex);
        }*/
        return tex;
    }
}