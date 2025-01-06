#if ENABLE_ADDRESSABLES
using UnityEngine.AddressableAssets;

public sealed class AddressableRefsBuilder : IScriptBuilderTarget
{
    public async void Build()
    {
        await Addressables.InitializeAsync().Task;
        var builder = new ScriptBuilder(new ScriptBuildContext("AddressableRefs", "public static class {0}"));
        foreach (var resourceLocator in Addressables.ResourceLocators)
        {
            foreach (var resourceLocatorKey in resourceLocator.Keys)
            {
                if (resourceLocatorKey is not string key) continue;
                if (!key.Contains(".") && !key.Contains("/")) continue;
                var s1 = key.Split("/")[^1];
                var s2 = s1.Split(".")[0];
                var definition = new ScriptFieldDefinition
                {
                    Name = s2,
                    Value = key,
                    Format = "public const string {0} = \"{1}\";"
                };
                builder.AddField(definition);
            }
        }
        builder.Build();
    }
}
#endif