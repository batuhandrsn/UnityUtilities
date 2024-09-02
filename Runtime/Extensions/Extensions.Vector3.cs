using UnityEngine;

public static partial class Extensions
{
    public static Vector3 Add(this Vector3 v, Vector3 direction, float value) => v + direction.normalized * value;

    public static Vector3 AddX(this Vector3 v, float value) => v.Add(Vector3.right, value);

    public static Vector3 AddY(this Vector3 v, float value) => v.Add(Vector3.up, value);

    public static Vector3 AddZ(this Vector3 v, float value) => v.Add(Vector3.forward, value);
}