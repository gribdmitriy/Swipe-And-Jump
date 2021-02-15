using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Items Data", menuName = "Items/ItemsData")]
public class ItemsData : ScriptableObject
{
    public int totalChance;
    public List<Item> items;

    public bool TryToGetItem(string name, out Item result)
    {
        if (items.Any(i => i.name == name))
        {
            result = items.First(i => i.name == name);
            return true;
        }

        result = null;
        return false;
    }
    public bool TryToGetRandomItem(out Item result)
    {
        return ItemGenerator.GetRandomItemByTotalChance(
            items.Select(item => new ItemData<Item>() { item = item, chance = item.spawnChance }).ToList(),
            totalChance,
            out result);
    }
}