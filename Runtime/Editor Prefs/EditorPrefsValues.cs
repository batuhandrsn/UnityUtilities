#if UNITY_EDITOR
using System;
using UnityEditor;

public struct EditorPrefsInt
{
    private readonly string _key;
    private readonly int _defaultValue;

    public EditorPrefsInt(string key)
    {
        _key = key;
        _defaultValue = 0;
    }

    public EditorPrefsInt(string key, int defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }

    public int Value
    {
        get => EditorPrefs.GetInt(_key, _defaultValue);
        set => EditorPrefs.SetInt(_key, value);
    }

    public void Set(int value) => Value = value;
    public bool HasValue() => EditorPrefs.HasKey(_key);
    public void DeleteValue() => EditorPrefs.DeleteKey(_key);
    public static implicit operator int(EditorPrefsInt data) => data.Value;
}

public struct EditorPrefsFloat
{
    private readonly string _key;
    private readonly float _defaultValue;

    public EditorPrefsFloat(string key)
    {
        _key = key;
        _defaultValue = 0f;
    }

    public EditorPrefsFloat(string key, float defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }

    public float Value
    {
        get => EditorPrefs.GetFloat(_key, _defaultValue);
        set => EditorPrefs.SetFloat(_key, value);
    }

    public void Set(float value) => Value = value;
    public bool HasValue() => EditorPrefs.HasKey(_key);
    public void DeleteValue() => EditorPrefs.DeleteKey(_key);
    public static implicit operator float(EditorPrefsFloat data) => data.Value;
}

public struct EditorPrefsString
{
    private readonly string _key;
    private readonly string _defaultValue;

    public EditorPrefsString(string key)
    {
        _key = key;
        _defaultValue = string.Empty;
    }

    public EditorPrefsString(string key, string defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }

    public string Value
    {
        get => EditorPrefs.GetString(_key, _defaultValue);
        set => EditorPrefs.SetString(_key, value);
    }

    public void Set(string value) => Value = value;
    public bool HasValue() => EditorPrefs.HasKey(_key);
    public void DeleteValue() => EditorPrefs.DeleteKey(_key);
    public static implicit operator string(EditorPrefsString data) => data.Value;
}

public struct EditorPrefsBool
{
    private readonly string _key;
    private readonly bool _defaultValue;

    public EditorPrefsBool(string key)
    {
        _key = key;
        _defaultValue = false;
    }

    public EditorPrefsBool(string key, bool defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }

    public bool Value
    {
        get => EditorPrefs.GetBool(_key, _defaultValue);
        set => EditorPrefs.SetBool(_key, value);
    }

    public void Set(bool value) => Value = value;
    public bool HasValue() => EditorPrefs.HasKey(_key);
    public void DeleteValue() => EditorPrefs.DeleteKey(_key);
    public static implicit operator bool(EditorPrefsBool data) => data.Value;
}

public struct EditorPrefsEnum<TEnum> where TEnum : struct
{
    private readonly string _key;
    private readonly TEnum _defaultValue;

    public EditorPrefsEnum(string key)
    {
        _key = key;
        _defaultValue = Enum.Parse<TEnum>(Enum.GetValues(typeof(TEnum)).GetValue(0).ToString());
    }

    public EditorPrefsEnum(string key, TEnum defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }

    public TEnum Value
    {
        get
        {
            var isParsed = Enum.TryParse<TEnum>(EditorPrefs.GetString(_key, _defaultValue.ToString()), out var result);
            return isParsed ? result : _defaultValue;
        }
        set => EditorPrefs.SetString(_key, value.ToString());
    }

    public void Set(TEnum value) => Value = value;
    public bool HasValue() => EditorPrefs.HasKey(_key);
    public void DeleteValue() => EditorPrefs.DeleteKey(_key);
    public static implicit operator TEnum(EditorPrefsEnum<TEnum> data) => data.Value;
}
#endif