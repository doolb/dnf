using System;
using System.Collections.Generic;

[System.Serializable]
public class EnumException : System.Exception
{
    public Type enumType { get; set; }
    public string missEnum { get; set; }
    public EnumException() { }
    public EnumException(string message) : base(message) { }
    public EnumException(string message, System.Exception inner) : base(message, inner) { }
    protected EnumException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public static class EnumExtension
{
    public static Dictionary<Type, Dictionary<string, string>> missEnum { get; } = new Dictionary<Type, Dictionary<string, string>>();

    public static T ToEnum<T>(this string str) {
        var type = typeof(T);
        try {
            return (T)Enum.Parse(type, str);
        }
        catch (Exception) {
            if (!missEnum.ContainsKey(type))
                missEnum.Add(type, new Dictionary<string, string>());
            if (missEnum[type].ContainsKey(str))
                return default;
            missEnum[type].Add(str, "");
            throw new EnumException($"enum {type.Name} no found : {str}") { enumType = type, missEnum = str };
        }
    }

    public static string PrintMissEnum(){
        System.Text.StringBuilder sbd = new System.Text.StringBuilder();
        foreach(var _type in missEnum){
            sbd.AppendLine(_type.Key.FullName);
            foreach(var _enum in _type.Value){
                sbd.Append(' ').Append(_enum.Key).Append(", //").AppendLine(_enum.Value);
            }
            sbd.AppendLine();
        }
        return sbd.ToString();
    }
}