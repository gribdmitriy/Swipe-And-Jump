using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(PatternManager))]

public class PatternManagerEditor : Editor
{
    private PatternManager pm;
    private float showList;
    private List<float> showSets = new List<float>();
    private List<List<float>> showPatterns = new List<List<float>>();
    private bool show;

    public void OnEnable()
    {
        pm = (PatternManager)target;
        showSets.InitializeColletion(pm.Sets.Count, 0);

        for (int i = 0; i < pm.Sets.Count; i++)
        {
            showPatterns.Add(new List<float>());
            for (int j = 0; j < pm.Sets[i].patterns.Count; j++)
            {
                showPatterns[i].Add(0);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        showList = Show(pm.Sets, showList);
        if (GUI.changed) SetObjectDirty(pm.gameObject);
        serializedObject.ApplyModifiedProperties();
    }

    public float Show(List<Set> sets, float f)
    {
        if (GUILayout.Button("INFINITY PRESSETS", GUILayout.MinWidth(500), GUILayout.Height(30)))
            f = f == 0 ? 1 : 0;
        //show = EditorGUILayout.BeginFoldoutHeaderGroup(show, "Infinity presets");


        if (EditorGUILayout.BeginFadeGroup(f))
        {
            if (sets.Count > 0)
            {
                for (int i = 0; i < sets.Count; i++)
                {
                    showSets[i] = DrowSet(sets[i], showSets[i], i);
                }
            }

            if (GUILayout.Button("Add New Set"))
            {
                showSets.Add(1);
                Set temp = new Set();
                pm.Sets.Add(temp);
                temp.patterns = new List<Pattern>();
                pm.Sets[pm.Sets.Count - 1].patterns.Add(new Pattern(SegmentType.Ground));
            }
        }
           
        EditorGUILayout.EndFadeGroup();
    
        return f;
    }

    public float DrowSet(Set set, float f, int i)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginVertical("box");

        if (GUILayout.Button("Set " + i, GUILayout.MaxWidth(1000), GUILayout.Height(30)))
            f = f == 0 ? 1 : 0;

        if (EditorGUILayout.BeginFadeGroup(f))
        {
            if (set.Patterns.Count > 0)
            {
                for (int j = 0; j < set.Patterns.Count; j++)
                {
                    showPatterns[i][j] = DrowPattern(set, set.Patterns[j], showPatterns[i][j], j); //f
                }
            }

            DrowFooter(set);
        }

        EditorGUILayout.EndFadeGroup();

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        return f;
    }

    public float DrowPattern(Set set, Pattern pattern, float f, int index)
    {
        if (GUILayout.Button("Pattern " + index, GUILayout.MaxWidth(1000), GUILayout.Height(15)))
            f = f == 0 ? 1 : 0;

        if (EditorGUILayout.BeginFadeGroup(f))
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
                            if (GUILayout.Button(pattern.Segments[i].ToString().Substring(0, 2).ToUpper(), GUILayout.MaxWidth(100), GUILayout.MinWidth(5), GUILayout.Height(30)))
                            {
                                pattern.Segments[i] = SegmentType.Let;
                            }
                            break;
                        case SegmentType.Let:
                            GUI.backgroundColor = Color.red;
                            if (GUILayout.Button(pattern.Segments[i].ToString().Substring(0, 2).ToUpper(), GUILayout.MaxWidth(100), GUILayout.MinWidth(5), GUILayout.Height(30)))
                            {
                                pattern.Segments[i] = SegmentType.Abyss;
                            }
                            break;
                        case SegmentType.Abyss:
                            GUI.backgroundColor = Color.blue;
                            if (GUILayout.Button(pattern.Segments[i].ToString().Substring(0, 2).ToUpper(), GUILayout.MaxWidth(100), GUILayout.MinWidth(5), GUILayout.Height(30)))
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
        EditorGUILayout.EndFadeGroup();

        return f;
    }

    public void DrowFooter(Set set)
    {
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

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}