using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Extensions
{
    /// <summary>
    /// Retrieves all types that are derived from the specified base class type.
    /// </summary>
    /// <returns> An IEnumerable of Type representing the derived class types.</returns>
    public static IEnumerable<Type> GetDerivedClassTypes(this Type baseClassType)
    {
        if (!baseClassType.IsClass) return null;
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && type.IsSubclassOf(baseClassType));
    }
}