using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator
{
    public static bool GetRandomItemByTotalChance<T>(List<ItemData<T>> items, int totalChance, out T result)
    {
        int rand = Random.Range(0, totalChance);
        int currentChance = 0;

        foreach (var item in items)
        {
            if (rand < currentChance + item.chance)
            {
                result = item.item;
                return true;
            }
            else
            {
                currentChance += item.chance;
            }
        }

        result = default;
        return false;
    }
}

[System.Serializable]
public class ItemData<T>
{
    public T item;
    public int chance;
}