using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

public sealed class ScriptBuilderEditorHelper
{
    [MenuItem("Tools/Script Builder/Build All")]
    public static void UpdateScriptBuilders()
    {
        UpdateAllSubclasses();
    }

    private static void UpdateAllSubclasses()
    {
        var subclassTypes = Assembly
            .GetAssembly(typeof(IScriptBuilderTarget))
            .GetTypes()
            .Where(t => typeof(IScriptBuilderTarget).IsAssignableFrom(t) && t.IsClass);

        foreach (var subclassType in subclassTypes)
        {
            var builder = Activator.CreateInstance(subclassType) as IScriptBuilderTarget;
            builder?.Build();
        }
    }
}