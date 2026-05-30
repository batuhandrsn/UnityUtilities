using System;

public struct ScriptFieldDefinition : IEquatable<ScriptFieldDefinition>
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string Format { get; set; }

    public bool Equals(ScriptFieldDefinition other) => Name == other.Name;
    public override bool Equals(object obj) => obj is ScriptFieldDefinition other && Equals(other);
    public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;
}