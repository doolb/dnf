using Godot;
using Core;
using System;

namespace Game
{
    public class Main : Node, IConfigable
    {
        public override void _EnterTree() {
            Name = nameof(Main);
            Debug.Log(nameof(Main) + " start");
            LoadCfg(EditorDescription);

            Coroutine.Instance.StartOnTask(() => ResourceManager.Instance.LoadNpkHeader());
            ConfigManager.Instance.LoadConfig("config.zip", new ScriptConfigLoader());
        }

        public void LoadCfg(string str) {
            if (string.IsNullOrEmpty(str)) return;
            var cfg = new IniParser.Parser.IniDataParser().Parse(str ?? "");
            Debug.level = (Debug.Level)Enum.Parse(typeof(Debug.Level), cfg.Global["debug"]);
        }
    }
}