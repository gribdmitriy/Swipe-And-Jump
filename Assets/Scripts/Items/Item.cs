using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { SpawnChance, StacksCap, Duration, EffectStrength }
public enum ConstraintType { DontSpawnsOn, SpawnsOnlyOn }

[System.Serializable]
public class Item
{
    public string name;
    public SegmentItem prefab;

    public int spawnChance;
    public int stacksCap;
    public float duration;
    public float effectStrength;

    public List<Upgrade> upgrades;
    public List<Constraint> constraints;

    public Item()
    {
        upgrades = new List<Upgrade>();
        constraints = new List<Constraint>();
    }
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
