using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(PatternManager))]
[CanEditMultipleObjects]
public class PatternManagerEditor : Editor
{
    private PatternManager pm;

    public void OnEnable()
    {
        pm = (PatternManager)target;
    }

    public override void OnInspectorGUI()
    {

        if (pm.Sets.Count > 0)
        {
            foreach (Set set in pm.Sets)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginVertical("window");
                {
                    if (set.Patterns.Count > 0)
                    {
                        foreach (Pattern pattern in set.Patterns)
                        {

                            EditorGUILayout.BeginHorizontal();

                            
                            if (pattern.Segments.Count > 0)
                            {
                                Color previousColor = GUI.backgroundColor;
                                for (int i = 0; i < pattern.Segments.Count; i++)
                                {
                                    switch (pattern.Segments[i])
                                    {
                                        case SegmentType.Ground:
                                            GUI.backgroundColor = Color.cyan;
                                            if (GUILayout.Button(pattern.Segments[i].ToString().Substring(0, 2).ToUpper(), GUILayout.Width(30), GUILayout.Height(30)))
                                            {
                                                pattern.Segments[i] = SegmentType.Let;
                                            }
                                            break;
                                        case SegmentType.Let:
                                            GUI.backgroundColor = Color.red;
                                            if (GUILayout.Button(pattern.Segments[i].ToString().Substring(0, 2).ToUpper(), GUILayout.Width(30), GUILayout.Height(30)))
                                            {
                                                pattern.Segments[i] = SegmentType.Abyss;
                                            }
                                            break;
                                        case SegmentType.Abyss:
                                            GUI.backgroundColor = Color.blue;
                                            if (GUILayout.Button(pattern.Segments[i].ToString().Substring(0, 2).ToUpper(), GUILayout.Width(30), GUILayout.Height(30)))
                                            {
                                                pattern.Segments[i] = SegmentType.Ground;
                                            }
                                            break;
                                    }
                                }
                                GUI.backgroundColor = previousColor;

                                if (GUILayout.Button("X", GUILayout.Width(30), GUILayout.Height(30)))
                                {
                                    set.Patterns.Remove(pattern);
                                    SetObjectDirty(pm.gameObject);
                                }
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }

                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("Add pattern", GUILayout.Height(30)))
                    {
                        Pattern temp = new Pattern();
                        temp.segments = new List<SegmentType>();
                        for (int i = 0; i < 12; i++)
                        {
                            temp.segments.Add(SegmentType.Ground);
                        }
                        set.Patterns.Add(temp);
                    }

                    if (GUILayout.Button("Remove set", GUILayout.Height(30)))
                    {
                        pm.Sets.Remove(set);
                        SetObjectDirty(pm.gameObject);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();
            }
        }

        if (GUILayout.Button("Add New Set"))
        {
            Set temp = new Set();
            pm.Sets.Add(temp);
        }

        if(GUI.changed) SetObjectDirty(pm.gameObject);
    }

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}