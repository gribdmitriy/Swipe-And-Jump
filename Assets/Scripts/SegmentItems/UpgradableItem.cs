using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableItem : PoolObject
{
    [SerializeField]
    private int currentLevel;
    public int CurrentLevel => currentLevel;

    [SerializeField]
    private int maxLevel;
    public int MaxLevel => maxLevel;

    public virtual void Upgrade()
    {
        if(currentLevel < maxLevel)
            currentLevel += 1;
    }

}
