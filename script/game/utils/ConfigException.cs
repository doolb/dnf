using System;
using System.Collections.Generic;

[System.Serializable]
public class ConfigException : System.Exception
{
    public string Key { get; set; }
    public string Value { get; set; }
    public ConfigException() { }
    public ConfigException(string message) : base(message) { }
    public ConfigException(string message, System.Exception inner) : base(message, inner) { }
    protected ConfigException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public static class ConfigUtils
{
    // type; key; value,file
    public static Dictionary<Type, Dictionary<string, string[]>> missConfig { get; } = new Dictionary<Type, Dictionary<string, string[]>>();

    public static void RecordMiss(Type @type, string @key, string @value) {
        if (!missConfig.ContainsKey(@type))
            missConfig.Add(@type, new Dictionary<string, string[]>());
        if (missConfig[@type].ContainsKey(@key))
            return;
        missConfig[@type].Add(@key, new string[2] { @value, null });
        throw new ConfigException($"enum {@type.Name} key no found : {@key}") { Key = @key, Value = @value };
    }

    public static string PrintMiss() {
        System.Text.StringBuilder sbd = new System.Text.StringBuilder();
        foreach (var _type in missConfig) {
            sbd.AppendLine(_type.Key.FullName);
            foreach (var _enum in _type.Value) {
                sbd.AppendLine($"  case \"{_enum.Key}\": // {_enum.Value[1]} at {_enum.Value[0]}\n  break;");
            }
            sbd.AppendLine();
        }
        return sbd.ToString();
    }
}