public struct ScriptBuildContext
{
    public string Name { get; set; }
    public string Header { get; set; }
    public string Path { get; set; }
    public int Indentation { get; set; }

    public ScriptBuildContext(string name, string header, string path = "", int indentation = 4)
    {
        Name = name;
        Header = header;
        Path = path;
        Indentation = indentation;
    }
}