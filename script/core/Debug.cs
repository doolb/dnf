using Godot;

namespace Core
{
    public static class Debug
    {
        static Debug() {
            level = Level.Log;
        }
        const string nullString = "Null";

        public enum Level
        {
            On = Log,
            Log = 7,    // 111
            Warning = 3,// 011
            Error = 1,  // 001
            Off = 0
        }

        public delegate bool DebugCallback(Level level, string msg);
        public static DebugCallback OnDebugCallback;
        public static Level level { get; set; }

        public static bool isDebugBuild { get { return OS.IsDebugBuild(); } }

        public static void Log(object obj) {
            if (!isDebugBuild)
                return;
            if (level < Level.Log)
                return;
            var msg = "Log: " + Time.time + " " + (obj != null ? obj.ToString() : nullString);
            if (OnDebugCallback == null || OnDebugCallback(Level.Log, msg))
                GD.Print(msg);
        }

        public static void LogWarning(object obj) {
            if (level < Level.Warning)
                return;
            var msg = "Warn: " + Time.time + " " + (obj != null ? obj.ToString() : nullString);
            if (OnDebugCallback == null || OnDebugCallback(Level.Warning, msg))
                GD.Print(msg);
        }


        public static void LogError(object obj) {
            if (level < Level.Error)
                return;
            var msg = "Error: " + Time.time + " " + (obj != null ? obj.ToString() : nullString);
            if (OnDebugCallback == null || OnDebugCallback(Level.Error, msg))
                GD.PrintErr(msg);
        }

        #region string extension
        public static void log(this string str) {
            Debug.Log(str);
        }
        public static void warning(this string str) {
            Debug.LogWarning(str);
        }
        public static void error(this string str) {
            Debug.LogError(str);
        }
        #endregion
    }
}