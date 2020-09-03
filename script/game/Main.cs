using Godot;
using Core;
using System;
using System.IO;

namespace Game
{
    public class Main : Node, IConfigable
    {
        public override void _EnterTree() {
            Name = nameof(Main);

            log_file = open_log_file();
            Debug.OnDebugCallback += on_debug_msg;

            Debug.Log(nameof(Main) + " start");
            LoadCfg(EditorDescription);

            Coroutine.Instance.StartOnTask(() => ResourceManager.Instance.LoadNpkHeader());
            ConfigManager.Instance.LoadConfig("config.zip", new ScriptConfigLoader());

        }

        public override void _ExitTree() {
            save_log_file(log_file);
        }

        public void LoadCfg(string str) {
            if (string.IsNullOrEmpty(str)) return;
            var cfg = new IniParser.Parser.IniDataParser().Parse(str ?? "");
            Debug.level = (Debug.Level)Enum.Parse(typeof(Debug.Level), cfg.Global["debug"]);
        }

        StreamWriter log_file;
        bool on_debug_msg(Debug.Level level, string msg) {
            log_file.WriteLine(msg);
            log_file.Flush();
            return true;
        }
        StreamWriter open_log_file() {
            return new StreamWriter("log.txt");
        }
        void save_log_file(StreamWriter writer) {
            writer.Close();
        }
    }
}