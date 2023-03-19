using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtensions
{
    public static T GetRandom<T>(this List<T> collection)
    {
        var randomIndex = Random.Range(0, collection.Count);
        return collection[randomIndex];
    }
}