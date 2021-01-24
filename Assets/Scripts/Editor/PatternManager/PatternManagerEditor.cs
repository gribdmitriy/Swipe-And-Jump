//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(PatternManager))]
//public class PatternManagerEditor : Editor
//{
//    private List<SegmentButton> buttons;
//    private PatternManager pm;

//    public void OnEnable()
//    {
//        pm = (PatternManager)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        if (pm.Sets.Count > 0)
//        {
//            foreach (Set set in pm.Sets)
//            {
//                //EditorGUILayout.BeginVertical("box");

//                {
//                    if (set.Patterns.Count > 0)
//                    {
//                        foreach (Pattern pattern in set.Patterns)
//                        {
//                            EditorGUILayout.BeginHorizontal("box");

//                            if (pattern.Segments.Count > 0)
//                            {
//                                foreach (string segment in pattern.Segments)
//                                {
//                                    GUILayout.Button("X", GUILayout.Width(30), GUILayout.Height(30));
//                                }
//                            }


//                            EditorGUILayout.EndHorizontal();
//                        }
//                    }

//                    if (set.Patterns.Count < 12)
//                    {
//                        if (GUILayout.Button("+", GUILayout.Width(30), GUILayout.Height(30))) set.Patterns.Add(new Pattern());
//                    }
//                }

//                //EditorGUILayout.EndVertical();
//            }
//        }

//        if (GUILayout.Button("Add New Set")) pm.Sets.Add(new Set());
//    }
//}


//public class SegmentButton : Editor
//{
//    public State state;

//    public enum State
//    {
//        Ground,
//        Let,
//        Abyss
//    }

//    public void SwitchState(State _state)
//    {
//        state = _state;
//    }

//    public State GetCurrentState()
//    {
//        return state;
//    }
//}