using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator
{
    public static bool GetRandomItemByTotalChance(List<ItemData> items, int totalChance, out PoolObject result)
    {
        int rand = Random.Range(0, totalChance);
        int currentChance = 0;

        foreach (var item in items)
        {
            if (rand < currentChance + item.chance)
            {
                result = item.gameObject;
                return true;
            }
            else
            {
                currentChance += item.chance;
            }
        }

        result = null;
        return false;
    }
}

[System.Serializable]
public class ItemData
{
    public PoolObject gameObject;
    public int chance;
}