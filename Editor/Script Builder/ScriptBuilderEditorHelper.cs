using System;
using UnityEditor;
using UnityEngine;

public static class ScriptBuilderEditorHelper
{
    [MenuItem("Tools/Script Builder/Build All")]
    public static void UpdateScriptBuilders()
    {
        try
        {
            UpdateAllSubclasses();
        }
        finally
        {
            Debug.Log("All scripts built.");
            EditorUtility.ClearProgressBar();
        }
    }

    private static void UpdateAllSubclasses()
    {
        var subclassTypes = TypeCache.GetTypesDerivedFrom<IScriptBuilderTarget>();
        for (var i = 0; i < subclassTypes.Count; i++)
        {
            var subclassType = subclassTypes[i];
            var builder = Activator.CreateInstance(subclassType) as IScriptBuilderTarget;
            if (builder == null) continue;
            var progress = (float)i / subclassTypes.Count;
            EditorUtility.DisplayProgressBar("Building scripts", subclassType.Name, progress);
            builder.Build();
        }
    }
}