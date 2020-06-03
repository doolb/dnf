using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace Core
{
    public class ConfigManager : Node
    {
        public static ConfigManager Instance => GameManager.Instance.Get<ConfigManager>();

        public override void _EnterTree() {

        }

        public override void _Process(float delta) {
            if (OS.IsDebugBuild()) {
                StringBuilder sb = new StringBuilder();
                sb.Append("data: ").Append(configDatas.Count);
                EditorDescription = sb.ToString();
            }
        }

        Dictionary<string, Config> configDatas = new Dictionary<string, Config>();
        public void LoadConfig(string name, IConfigLoader loader) {
            ResourceManager.Instance.Load("config/" + name, (r) => loader._loadOK(r));
        }
        public void LoadConfigRaw(string path, IConfigLoaderRaw loader) {
            var f = new File();
            var p = "user://" + path;
            if (!f.FileExists(p)) {
                Debug.LogWarning("file no found: " + p);
                return;
            }
            f.Open(p, File.ModeFlags.Read);
            // this.StartCoroutine(loader._loadRaw(f));
            Coroutine.Instance.StartOnTask(loader._loadRaw(f));
        }
    }
    public class Config
    {

    }
    public interface IConfigLoader
    {
        void _loadOK(Resource res);
    }
    public interface IConfigLoaderRaw
    {
        System.Collections.IEnumerator _loadRaw(File file);
    }
}