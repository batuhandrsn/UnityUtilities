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
        foreach (var animatorGuid in animatorGuids)
        {
            var animatorPath = AssetDatabase.GUIDToAssetPath(animatorGuid);
            var animatorAsset = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(animatorPath);
            var builder = new ScriptBuilder(new ScriptBuildContext($"{animatorAsset.name}Refs", "public enum {0}"));
            var animatorController = animatorAsset as AnimatorController;
            if (animatorController == null) continue;
            includedNames.Clear();
            foreach (var parameter in animatorController.parameters)
            {
                if (includedNames.Contains(parameter.name)) continue;
                var definition = new ScriptFieldDefinition
                {
                    Name = parameter.name,
                    Value = parameter.nameHash.ToString(),
                    Format = "{0} = {1},"
                };
                builder.AddField(definition);
                includedNames.Add(parameter.name);
            }
            builder.Build();
        }
    }
}