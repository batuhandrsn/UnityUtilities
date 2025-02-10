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

    /// <summary>
    /// Shuffles the elements of the list in a random order using the Fisher-Yates algorithm.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        if (list == null || list.Count <= 1) return;

        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            Swap(list, i, j);
        }
    }

    /// <summary>
    /// Shuffles the elements of the array in a random order using the Fisher-Yates algorithm.
    /// </summary>
    public static void Shuffle<T>(this T[] array)
    {
        if (array == null || array.Length <= 1) return;

        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            Swap(array, i, j);
        }
    }

    /// <summary>
    /// Swaps the elements at the specified indices in the list.
    /// </summary>
    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[i]);
    }

    /// <summary>
    /// Swaps the elements at the specified indices in the array.
    /// </summary>
    public static void Swap<T>(this T[] array, int i, int j)
    {
        (array[i], array[j]) = (array[j], array[i]);
    }
}