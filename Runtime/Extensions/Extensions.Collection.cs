using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static T GetRandom<T>(this IList<T> collection)
    {
        var randomIndex = Random.Range(0, collection.Count);
        return collection[randomIndex];
    }
    
    public static T GetRandom<T>(this T[] collection)
    {
        var randomIndex = Random.Range(0, collection.Length);
        return collection[randomIndex];
    }
}