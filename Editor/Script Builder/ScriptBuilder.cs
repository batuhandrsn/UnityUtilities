using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public sealed class ScriptBuilder
{
    private readonly ScriptBuildContext _context;
    private readonly List<ScriptFieldDefinition> _fields = new();

    public ScriptBuilder(ScriptBuildContext context)
    {
        _context = context;
        _context.Name = CleanUpFieldName(_context.Name);
        if (string.IsNullOrEmpty(_context.Path))
        {
            var settings = Resources.LoadAll<ScriptBuilderSettings>(string.Empty);
            _context.Path = settings.Length == 0 ? "Scripts/Runtime/References" : settings[0].DefaultPath;
        }
        _context.Path = Path.Combine(_context.Path, $"{_context.Name}.cs");
    }

    public void AddField(ScriptFieldDefinition definition)
    {
        _fields.Add(definition);
    }

    public async void Build()
    {
        var fullClassPath = Path.Combine(Application.dataPath, _context.Path);
        var directoryPath = Path.GetDirectoryName(fullClassPath);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath ?? string.Empty);

        var validScriptContents = ToString();
        if (File.Exists(fullClassPath))
        {
            var currentScriptContents = await File.ReadAllTextAsync(fullClassPath);
            // If there is no difference in the script, do not rebuild
            if (string.Equals(currentScriptContents, validScriptContents)) return;
        }

        await File.WriteAllTextAsync(fullClassPath, validScriptContents);
        var relativeScriptPath = Path.Combine("Assets", _context.Path);
        AssetDatabase.ImportAsset(relativeScriptPath);
    }

    public override string ToString()
    {
        var validScriptContents = new StringBuilder();
        {
            var header = string.Format(_context.Header, _context.Name);
            validScriptContents.AppendLine(header);
            validScriptContents.AppendLine("{");
            foreach (var field in _fields)
            {
                for (var i = 0; i < _context.Indentation; i++) validScriptContents.Append(' ');
                var validFieldName = CleanUpFieldName(field.Name);
                var line = string.Format(field.Format, validFieldName, field.Value);
                validScriptContents.AppendLine(line);
            }
            validScriptContents.Append("}");
        }
        return validScriptContents.ToString();
    }

    private static string CleanUpFieldName(string input)
    {
        var validFieldName = Regex.Replace(input, "[^a-zA-Z0-9_.]+", string.Empty);
        // Ensure the first character is a letter (valid field name start). If not, prefix it with an underscore
        if (!char.IsLetter(validFieldName[0]))
            validFieldName = "_" + validFieldName;
        return validFieldName;
    }
}