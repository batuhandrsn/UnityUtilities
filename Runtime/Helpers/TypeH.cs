using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Pool;

public static class TypeH
{
    private static readonly Dictionary<Assembly, Type[]> CachedAssemblyTypes = new();

    /// <summary>
    /// Retrieves all types that are derived from the specified base type.
    /// </summary>
    public static PooledObject<List<Type>> GetTypesDerivedFrom(Type baseType, out List<Type> result)
    {
        result = null;

        if (baseType == null || (!baseType.IsClass && !baseType.IsInterface))
            return default;

        var p = ListPool<Type>.Get(out result);

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (!CachedAssemblyTypes.TryGetValue(assembly, out var types))
            {
                try
                {
                    types = assembly.GetTypes();
                }
                // Some Unity editor/IL2CPP assemblies may fail to load all types.
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types;
                }

                CachedAssemblyTypes[assembly] = types;
            }

            if (types == null)
                continue;

            foreach (var type in types)
            {
                if (type == null) continue;
                if (type == baseType) continue;
                if (!type.IsClass) continue;
                if (!baseType.IsAssignableFrom(type)) continue;

                result.Add(type);
            }
        }

        return p;
    }

    /// <summary>
    /// Retrieves all types that are derived from the specified base type.
    /// </summary>
    public static PooledObject<List<Type>> GetTypesDerivedFrom<T>(out List<Type> result)
    {
        return GetTypesDerivedFrom(typeof(T), out result);
    }
}