using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public sealed class AnimatorRefsBuilder : IScriptBuilderTarget
{
    public void Build()
    {
        var animatorGuids = AssetDatabase.FindAssets("t:AnimatorController");
        var includedNames = new HashSet<string>();
        var fieldDefinitions = new List<ScriptFieldDefinition>();
        foreach (var animatorGuid in animatorGuids)
        {
            var animatorPath = AssetDatabase.GUIDToAssetPath(animatorGuid);
            var animatorAsset = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(animatorPath);
            var animatorController = animatorAsset as AnimatorController;
            if (animatorController == null) continue;
            foreach (var parameter in animatorController.parameters)
            {
                if (!includedNames.Add(parameter.name)) continue;
                var definition = new ScriptFieldDefinition
                {
                    Name = parameter.name,
                    Value = parameter.nameHash.ToString(),
                    Format = "{0} = {1},"
                };
                fieldDefinitions.Add(definition);
            }
        }

        var builder = new ScriptBuilder(new ScriptBuildContext($"AnimatorRefs", "public enum {0}"));
        foreach (var definition in fieldDefinitions)
            builder.AddField(definition);
        builder.Build();
    }
}