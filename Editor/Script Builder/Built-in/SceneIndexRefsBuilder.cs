using UnityEditor;

public sealed class SceneIndexRefsBuilder : IScriptBuilderTarget
{
    public void Build()
    {
        var builder = new ScriptBuilder(new ScriptBuildContext("SceneIndexRefs", "public enum {0}"));
        for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var sceneSettings = EditorBuildSettings.scenes[i];
            var s1 = sceneSettings.path.Split("/")[^1].Replace(".unity", string.Empty);
            var definition = new ScriptFieldDefinition
            {
                Name = s1,
                Value = i.ToString(),
                Format = "{0} = {1},"
            };
            builder.AddField(definition);
        }
        builder.Build();
    }
}