using System.Linq;
using UnityEditor;
using UnityEngine;

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
        serializedObject.Update();

        Color baseColor = GUI.backgroundColor;

        GUILayout.Space(10);
        DrawItemsDataFields();

        GUILayout.BeginVertical("box");

        DrawItemsDataLists();

        GUILayout.Space(15);
        GUI.backgroundColor = Color.green * 1.4f;
        if (GUILayout.Button("ADD ITEM", GUILayout.Height(40)))
        {
            itemsDataTarget.items.Add(new Item());
        }

        GUILayout.EndVertical();

        GUI.backgroundColor = baseColor;


        if (GUI.changed) EditorUtility.SetDirty(itemsDataTarget);
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawItemsDataFields()
    {
        GUI.backgroundColor = new Color(0.45f, 0.6f, 0.8f) * 2.5f;

        GUILayout.BeginVertical("box");
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        itemsDataTarget.name = EditorGUILayout.TextField("DB NAME", itemsDataTarget.name);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        itemsDataTarget.totalChance = EditorGUILayout.IntField("TOTAL CHANCE", itemsDataTarget.totalChance);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
    private void DrawItemsDataLists()
    {
        for (int i = 0; i < itemsDataTarget.items.Count; i++)
        {
            GUILayout.Space(30);

            GUI.backgroundColor = new Color(0.45f, 0.6f, 0.8f) * 1.2f;
            Color baseColor = GUI.backgroundColor;


            GUILayout.BeginVertical("box");
            GUILayout.BeginVertical("window");


            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            if (string.IsNullOrEmpty(itemsDataTarget.items[i].name)) itemsDataTarget.items[i].name = "name";
            itemsDataTarget.items[i].name = EditorGUILayout.TextField(itemsDataTarget.items[i].name, GUILayout.Height(20));

            GUI.backgroundColor = Color.red * 1.4f;
            if (GUILayout.Button("X", GUILayout.Width(38), GUILayout.Height(20)))
            {
                itemsDataTarget.items.RemoveAt(i);
                break;
            }

            GUI.backgroundColor = baseColor;
            GUILayout.EndHorizontal();

            DrawMenuHeader("general", new Color(0.45f, 0.6f, 0.8f) * 3f);
            GUILayout.Space(8);
            DrawItemGeneral(itemsDataTarget.items[i]);
            GUILayout.Space(8);
            DrawBaseStats(itemsDataTarget.items[i]);
            GUILayout.EndVertical();

            GUILayout.Space(20);
            GUILayout.BeginVertical();
            DrawMenuHeader("upgrades", new Color(0.45f, 0.8f, 0.6f) * 3f);
            DrawUpgrades(itemsDataTarget.items[i]);
            GUILayout.EndVertical();

            GUILayout.Space(20);
            GUILayout.BeginVertical();
            DrawMenuHeader("constraints", new Color(0.8f, 0.6f, 0.45f) * 3f);
            DrawConstraints(itemsDataTarget.items[i]);
            GUILayout.EndVertical();


            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
    }

    private void DrawItemGeneral(Item item)
    {
        GUILayout.BeginHorizontal();
        item.prefab = EditorGUILayout.ObjectField(item.prefab, typeof(SegmentItem), true) as SegmentItem;
        GUILayout.EndHorizontal();
    }
    private void DrawBaseStats(Item item)
    {
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
    private void DrawUpgrades(Item item)
    {
        Color baseColor = GUI.backgroundColor;

        for (int i = 0; i < item.upgrades.Count; i++)
        {
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            item.upgrades[i].upgradeType = (UpgradeType)EditorGUILayout.EnumPopup(item.upgrades[i].upgradeType);
            item.upgrades[i].upgradeValue = EditorGUILayout.FloatField(item.upgrades[i].upgradeValue);
            GUILayout.EndVertical();

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(38), GUILayout.Height(38)))
            {
                item.upgrades.RemoveAt(i);
                break;
            }
            GUI.backgroundColor = baseColor;

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+", GUILayout.Height(16)))
        {
            item.upgrades.Add(new Upgrade());
        }
        GUI.backgroundColor = baseColor;
    }
    private void DrawConstraints(Item item)
    {
        Color baseColor = GUI.backgroundColor;

        for (int i = 0; i < item.constraints.Count; i++)
        {
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            item.constraints[i].constraint = (ConstraintType)EditorGUILayout.EnumPopup(item.constraints[i].constraint);
            item.constraints[i].segmentType = (SegmentType)EditorGUILayout.EnumPopup(item.constraints[i].segmentType);
            GUILayout.EndVertical();

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(38), GUILayout.Height(38)))
            {
                item.constraints.RemoveAt(i);
                break;
            }
            GUI.backgroundColor = baseColor;

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+", GUILayout.Height(16)))
        {
            item.constraints.Add(new Constraint());
        }
        GUI.backgroundColor = baseColor;
    }

    private void DrawMenuHeader(string text, Color? backColor = null, Color? frontColor = null)
    {
        Color baseBackColor = GUI.backgroundColor;
        Color baseFrontColor = GUI.contentColor;

        if (backColor != null) GUI.backgroundColor = backColor.Value;
        if (frontColor != null) GUI.contentColor = frontColor.Value;

        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField(text.ToUpper(),
            GUILayout.MaxWidth(text.Count(t => t == ' ' || t.ToString().ToUpper() == "I") * 4f +
            (text.Length - text.Count(t => t == ' ' || t.ToString().ToUpper() == "I")) * 9f));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUI.backgroundColor = baseBackColor;
        GUI.contentColor = baseFrontColor;
    }
}
