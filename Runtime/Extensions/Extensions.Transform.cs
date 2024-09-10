using UnityEngine;

public static partial class Extensions
{
    public static float DistanceTo(this Transform from, Transform to) => from.position.DistanceTo(to.position);
}