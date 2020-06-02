using Godot;
using Core;
using Hocon;
using System;

namespace Game
{
    public class Main : Node
    {
        public override void _EnterTree() {
            Name = nameof(Main);
            Debug.Log(nameof(Main) + " start");
            loadCfg(GetParent().EditorDescription);
        }

        void loadCfg(string str) {
            if(string.IsNullOrEmpty(str)) return;
            var cfg = HoconParser.Parse(str ?? "");
            Debug.level = (Debug.Level)Enum.Parse(typeof(Debug.Level), cfg.GetString("debug", nameof(Debug.Level.Log)));
        }
    }
}