using UnityEditor;

public sealed class SceneNameRefsBuilder : IScriptBuilderTarget
{
    public void Build()
    {
        var builder = new ScriptBuilder(new ScriptBuildContext("SceneNameRefs", "public static class {0}"));
        foreach (var scene in EditorBuildSettings.scenes)
        {
            var s1 = scene.path.Split("/")[^1].Replace(".unity", string.Empty);
            var definition = new ScriptFieldDefinition
            {
                Name = s1,
                Value = scene.path,
                Format = "public const string {0} = \"{1}\";"
            };
            builder.AddField(definition);
        }
        builder.Build();
    }
}