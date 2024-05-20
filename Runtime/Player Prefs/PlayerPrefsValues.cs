using System;
using UnityEngine;

public struct PlayerPrefsInt
{
    private readonly string _key;
    private readonly int _defaultValue;

    public PlayerPrefsInt(string key)
    {
        _key = key;
        _defaultValue = 0;
    }
    
    public PlayerPrefsInt(string key, int defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }
    
    public int Value
    {
        get => PlayerPrefs.GetInt(_key, _defaultValue);
        set => PlayerPrefs.SetInt(_key, value);
    }
    
    public void Set(int value) => Value = value;
    public bool HasValue() => PlayerPrefs.HasKey(_key);
    public static implicit operator int(PlayerPrefsInt playerPrefs) => playerPrefs.Value;
}

public struct PlayerPrefsFloat
{
    private readonly string _key;
    private readonly float _defaultValue;

    public PlayerPrefsFloat(string key)
    {
        _key = key;
        _defaultValue = 0f;
    }
    
    public PlayerPrefsFloat(string key, float defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }
    
    public float Value
    {
        get => PlayerPrefs.GetFloat(_key, _defaultValue);
        set => PlayerPrefs.SetFloat(_key, value);
    }
    
    public void Set(float value) => Value = value;
    public bool HasValue() => PlayerPrefs.HasKey(_key);
    public static implicit operator float(PlayerPrefsFloat playerPrefs) => playerPrefs.Value;
}

public struct PlayerPrefsString
{
    private readonly string _key;
    private readonly string _defaultValue;

    public PlayerPrefsString(string key)
    {
        _key = key;
        _defaultValue = string.Empty;
    }
    
    public PlayerPrefsString(string key, string defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }
    
    public string Value
    {
        get => PlayerPrefs.GetString(_key, _defaultValue);
        set => PlayerPrefs.SetString(_key, value);
    }
    
    public void Set(string value) => Value = value;
    public bool HasValue() => PlayerPrefs.HasKey(_key);
    public static implicit operator string(PlayerPrefsString playerPrefs) => playerPrefs.Value;
}

public struct PlayerPrefsBool
{
    private readonly string _key;
    private readonly bool _defaultValue;

    public PlayerPrefsBool(string key)
    {
        _key = key;
        _defaultValue = false;
    }
    
    public PlayerPrefsBool(string key, bool defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }
    
    public bool Value
    {
        get
        {
            var isParsed = bool.TryParse(PlayerPrefs.GetString(_key, _defaultValue.ToString()), out var result);
            return isParsed && result;
        }
        set => PlayerPrefs.SetString(_key, value.ToString());
    }

    public void Set(bool value) => Value = value;
    public bool HasValue() => PlayerPrefs.HasKey(_key);
    public static implicit operator bool(PlayerPrefsBool playerPrefs) => playerPrefs.Value;
}

public struct PlayerPrefsEnum<TEnum> where TEnum : struct
{
    private readonly string _key;
    private readonly TEnum _defaultValue;

    public PlayerPrefsEnum(string key)
    {
        _key = key;
        _defaultValue = Enum.Parse<TEnum>(Enum.GetValues(typeof(TEnum)).GetValue(0).ToString());
    }
    
    public PlayerPrefsEnum(string key, TEnum defaultValue)
    {
        _key = key;
        _defaultValue = defaultValue;
    }
    
    public TEnum Value
    {
        get
        {
            var isParsed = Enum.TryParse<TEnum>(PlayerPrefs.GetString(_key, _defaultValue.ToString()), out var result);
            return isParsed ? result : _defaultValue;
        }
        set => PlayerPrefs.SetString(_key, value.ToString());
    }
    
    public void Set(TEnum value) => Value = value;
    public bool HasValue() => PlayerPrefs.HasKey(_key);
    public static implicit operator TEnum(PlayerPrefsEnum<TEnum> playerPrefs) => playerPrefs.Value;
}