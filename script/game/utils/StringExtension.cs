public static class StringExtension
{
    public static bool ToBool(this string str) {
        if (!str.valid()) return false;
        else if (str == "1") return true;
        else if (str == "0") return false;
        else if (string.Equals(str, "true", System.StringComparison.OrdinalIgnoreCase)) return true;
        else if (string.Equals(str, "false", System.StringComparison.OrdinalIgnoreCase)) return false;
        else ConfigUtils.RecordMiss(typeof(StringExtension), "bool_" + str, str);
        return false;
    }

    public static int ToInt(this string str) {
        int result = 0;
        bool negitave = false;
        if (string.IsNullOrEmpty(str)) return 0;
        int i = 0;
        for (; i < str.Length && !char.IsDigit(str[i]); i++)
            ;
        if (i > 0 && str[i - 1] == '-') negitave = true;
        for (; i < str.Length && char.IsDigit(str[i]); i++)
            result = 10 * result + (str[i] - 48);
        return negitave ? -result : result;
    }
    public static int[] ToIntArray(this string str, char symbol) {
        if (string.IsNullOrEmpty(str)) {
            return new int[0];
        }
        string[] strArray = str.Split(symbol);
        if (strArray.Length < 1) {
            return new int[0];
        }
        int[] value = new int[strArray.Length];
        for (int i = 0; i < strArray.Length; i++) {
            value[i] = ToInt(strArray[i]);
        }
        return value;
    }
    public static uint ToUInt(this string str) {
        uint result = 0;
        if (string.IsNullOrEmpty(str)) return 0;
        int i = 0;
        for (; i < str.Length && !char.IsDigit(str[i]); i++)
            ;
        for (; i < str.Length && char.IsDigit(str[i]); i++)
            result = 10 * result + (uint)(str[i] - 48);
        return result;
    }
    public static byte ToByte(this string str) {
        byte rt;
        byte.TryParse(str, out rt);
        return rt;
    }
    public static short ToShort(this string str) {
        short rt;
        short.TryParse(str, out rt);
        return rt;
    }
    public static float ToFloat(this string str) {
        float rt;
        float.TryParse(str, out rt);
        return rt;
    }
    public static float[] ToFloatArray(this string str, char symbol) {
        if (string.IsNullOrEmpty(str)) {
            return new float[0];
        }
        string[] strArray = str.Split(symbol);
        if (strArray.Length < 1) {
            return new float[0];
        }
        float[] value = new float[strArray.Length];
        for (int i = 0; i < strArray.Length; i++) {
            value[i] = ToFloat(strArray[i]);
        }
        return value;
    }
    public static double ToDouble(this string str) {
        double rt;
        double.TryParse(str, out rt);
        return rt;
    }
    public static double[] ToDoubleArray(this string str, char symbol) {
        if (string.IsNullOrEmpty(str)) {
            return new double[0];
        }
        string[] strArray = str.Split(symbol);
        if (strArray.Length < 1) {
            return new double[0];
        }
        double[] value = new double[strArray.Length];
        for (int i = 0; i < strArray.Length; i++) {
            value[i] = ToDouble(strArray[i]);
        }
        return value;
    }
    public static long ToLong(this string str) {
        long result = 0;
        bool negitave = false;
        if (string.IsNullOrEmpty(str)) return 0;
        int i = 0;
        for (; i < str.Length && !char.IsDigit(str[i]); i++)
            ;
        if (i > 0 && str[i - 1] == '-') negitave = true;
        for (; i < str.Length && char.IsDigit(str[i]); i++)
            result = 10 * result + (str[i] - 48);
        return negitave ? -result : result;
    }
    public static long[] ToLongArray(this string str, char symbol) {
        if (string.IsNullOrEmpty(str)) {
            return new long[0];
        }
        string[] strArray = str.Split(symbol);
        if (strArray.Length < 1) {
            return new long[0];
        }
        long[] value = new long[strArray.Length];
        for (int i = 0; i < strArray.Length; i++) {
            value[i] = ToLong(strArray[i]);
        }
        return value;
    }
    /// <summary>
    /// 判断字符串是否 不为 null 或者 空 或者 不等于指定值
    /// </summary>
    public static bool valid(this string str, string not_equ = "") {
        return !string.IsNullOrEmpty(str) && str != not_equ;
    }

    public static string RemoveQuotes(this string str) {
        var path = str.Trim();
        return path.Length > 2 ? path.Substring(1, path.Length - 2) : "";
    }

    public static string GetLastStringOf(this string @str, char @ch, int @count) {
        if (string.IsNullOrEmpty(@str))
            return null;
        int _last = @str.Length - 1;
        for (int i = 0; i < @count; i++) {
            if (_last <= 0)
                return null;
            _last = @str.LastIndexOf(ch, _last) - 1;
        }
        return (_last != @str.Length -1) ? @str.Substring(0, _last + 2) : @str;
    }
}