using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    /// <summary>
    /// Returns a random element from the given collection.
    /// </summary>
    /// <returns> A random element from the collection.</returns>
    public static T GetRandom<T>(this IList<T> collection)
    {
        if (collection == null || collection.Count == 0) return default;
        var randomIndex = Random.Range(0, collection.Count);
        return collection[randomIndex];
    }

    /// <summary>
    /// Returns a random element from the given collection.
    /// </summary>
    /// <returns> A random element from the collection.</returns>
    public static T GetRandom<T>(this T[] collection)
    {
        if (collection == null || collection.Length == 0) return default;
        var randomIndex = Random.Range(0, collection.Length);
        return collection[randomIndex];
    }
}