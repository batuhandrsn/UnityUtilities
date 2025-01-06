using UnityEngine;

public sealed class LayerRefsBuilder : IScriptBuilderTarget
{
    public void Build()
    {
        var builder = new ScriptBuilder(new ScriptBuildContext("LayerRefs", "public static class {0}"));
        for (var i = 0; i < 32; i++) // Unity has a fixed max of 32 layers
        {
            var layerName = LayerMask.LayerToName(i);
            if (string.IsNullOrEmpty(layerName)) continue;
            var definition = new ScriptFieldDefinition
            {
                Name = layerName,
                Value = layerName,
                Format = "public const string {0} = \"{1}\";"
            };
            builder.AddField(definition);
        }
        builder.Build();
    }
}