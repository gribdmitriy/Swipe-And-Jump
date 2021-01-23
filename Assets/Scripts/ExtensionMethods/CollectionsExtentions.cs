using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionsExtentions
{
    public static bool IsNullOrEmpty(this ICollection collection)
    {
        return !collection.CountMoreThan(0);
    }

    public static bool CountMoreThan(this ICollection collection, int count)
    {
        if (collection != null)
            if (collection.Count > count)
                return true;

        return false;
    }
}
