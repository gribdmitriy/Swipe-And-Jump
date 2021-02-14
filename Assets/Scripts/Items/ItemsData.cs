using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum UpgradeType { SpawnChance, StacksCap, Duration, EffectStrength}
public enum ConstraintType { DontSpawnsOn, SpawnsOnlyOn}

[CreateAssetMenu(fileName = "Items Data", menuName = "Items/ItemsData")]
public class ItemsData : ScriptableObject
{
    public int totalChance;
    public List<Item> items;
}

[System.Serializable]
public class Item
{
    public string name;
    public GameObject prefab;

    public int spawnChance;
    public int stacksCap;
    public float duration;
    public float effectStrength;

    public List<Upgrade> upgrades;
    public List<Constraint> constraints;
}

[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    public float upgradeValue;
}

[System.Serializable]
public class Constraint
{
    public ConstraintType constraint;
    public SegmentType segmentType;
}

[CustomEditor(typeof(ItemsData))]
public class ItemsDataEditor : Editor
{
    ItemsData itemsDataTarget;

    private void OnEnable()
    {
        itemsDataTarget = target as ItemsData;
    }

    public override void OnInspectorGUI()
    {
        Color baseColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.92f, 0.97f, 1f);
        itemsDataTarget.name = EditorGUILayout.TextField(itemsDataTarget.name);

        for (int i = 0; i < itemsDataTarget.items.Count; i++)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginVertical("box");


            GUILayout.BeginVertical("window");
            DrowBaseStats(itemsDataTarget.items[i]);
            GUILayout.EndVertical();

            GUILayout.BeginVertical("window");
            DrowUpgrades(itemsDataTarget.items[i]);
            GUILayout.EndVertical();

            GUILayout.BeginVertical("window");
            DrowConstraints(itemsDataTarget.items[i]);
            GUILayout.EndVertical();


            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }

        GUI.backgroundColor = baseColor;
    }

    private void DrowBaseStats(Item item)
    {
        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("BASE STATS", GUILayout.MaxWidth(80));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("spawn chance", GUILayout.MaxWidth(100));
        item.spawnChance = EditorGUILayout.IntField(item.spawnChance, GUILayout.MaxWidth(60));
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("stacks cap", GUILayout.MaxWidth(100));
        item.stacksCap = EditorGUILayout.IntField(item.stacksCap, GUILayout.MaxWidth(60));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("duration", GUILayout.MaxWidth(100));
        item.duration = EditorGUILayout.FloatField(item.duration, GUILayout.MaxWidth(60));
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("effect strength", GUILayout.MaxWidth(100));
        item.effectStrength = EditorGUILayout.FloatField(item.effectStrength, GUILayout.MaxWidth(60));
        GUILayout.EndHorizontal();
    }

    private void DrowUpgrades(Item item)
    {
        Color baseColor = GUI.backgroundColor;

        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("APGRADES", GUILayout.MaxWidth(70));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        for (int i = 0; i < item.upgrades.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            item.upgrades[i].upgradeType = (UpgradeType)EditorGUILayout.EnumPopup(item.upgrades[i].upgradeType);
            item.upgrades[i].upgradeValue = EditorGUILayout.FloatField(item.upgrades[i].upgradeValue);
            GUILayout.EndVertical();

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(38), GUILayout.Height(38)))
            {

            }
            GUI.backgroundColor = baseColor;

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+", GUILayout.Height(16)))
        {

        }
        GUI.backgroundColor = baseColor;
    }

    private void DrowConstraints(Item item)
    {
        Color baseColor = GUI.backgroundColor;

        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("CONSTRAINTS", GUILayout.MaxWidth(100));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        for (int i = 0; i < item.constraints.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            item.constraints[i].constraint = (ConstraintType)EditorGUILayout.EnumPopup(item.constraints[i].constraint);
            item.constraints[i].segmentType = (SegmentType)EditorGUILayout.EnumPopup(item.constraints[i].segmentType);
            GUILayout.EndVertical();

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(38), GUILayout.Height(38)))
            {

            }
            GUI.backgroundColor = baseColor;

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+", GUILayout.Height(16)))
        {

        }
        GUI.backgroundColor = baseColor;
    }
}