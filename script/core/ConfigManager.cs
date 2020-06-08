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
                sb.Append($"sample: {sampleConfigDatas.Count}\n");
                foreach (var cfgs in sampleConfigDatas)
                    sb.Append($"    {cfgs.Key} : {cfgs.Value.Count}\n");
                sb.Append($"flat: {flatConfigDatas.Count}\n");
                foreach (var cfgs in flatConfigDatas)
                    sb.Append($"    {cfgs.Key} : {cfgs.Value.Count}\n");

                EditorDescription = sb.ToString();
            }
        }
        // load config
        Dictionary<string, Dictionary<int, SampleConfig>> sampleConfigDatas = new Dictionary<string, Dictionary<int, SampleConfig>>();
        Dictionary<string, Dictionary<string, FlatConfig>> flatConfigDatas = new Dictionary<string, Dictionary<string, FlatConfig>>();
        public void LoadConfig(string name, IConfigLoader loader) {
            ResourceManager.Instance.Load("config/" + name, loader._loadOK);
        }

        // add config to dic
        public void Add(SampleConfig _config) {
            var key = _config.type;
            if (!sampleConfigDatas.ContainsKey(key)) {
                var cfgs = new Dictionary<int, SampleConfig>();
                cfgs.Add(_config.sid, _config);
                sampleConfigDatas.Add(key, cfgs);
            }
            else {
                var cfgs = sampleConfigDatas[key];
                if (cfgs.ContainsKey(_config.sid)) {

                    Debug.LogWarning($"replace config : {key} {_config.sid}");
                    cfgs[_config.sid] = _config;
                }
                else {
                    //Debug.Log("add config : " + key);
                    cfgs.Add(_config.sid, _config);
                }
            }
        }
        public void Add(FlatConfig _config) {
            var key = _config.type;
            if (!flatConfigDatas.ContainsKey(key)) {
                var cfgs = new Dictionary<string, FlatConfig>();
                cfgs.Add(_config.key, _config);
                flatConfigDatas.Add(key, cfgs);
            }
            else {
                var cfgs = flatConfigDatas[key];
                if (cfgs.ContainsKey(_config.key)) {

                    Debug.LogWarning($"replace config : {key} {_config.key}");
                    cfgs[_config.key] = _config;
                }
                else {
                    //Debug.Log("add config : " + key);
                    cfgs.Add(_config.key, _config);
                }
            }
        }
        // get sample config by sid
        public T Get<T>(string type, int sid) where T : SampleConfig {
            if (!sampleConfigDatas.ContainsKey(type)) {
                Debug.LogWarning($"config no found: {type}");
                return null;
            }
            var cfgs = sampleConfigDatas[type];
            if (!cfgs.ContainsKey(sid)) {
                Debug.LogWarning($"config no found: {type} {sid}");
                return null;
            }
            return (T)cfgs[sid];
        }
        // get flat config by key
        public T Get<T>(string type, string key) where T : FlatConfig {
            if (!flatConfigDatas.ContainsKey(type)) {
                Debug.LogWarning($"config no found: {type}");
                return null;
            }
            var cfgs = flatConfigDatas[type];
            if (!cfgs.ContainsKey(key)) {
                Debug.LogWarning($"config no found: {type} {key}");
                return null;
            }
            return (T)cfgs[key];
        }
    }

    #region config base
    public interface IConfig
    {
        string type { get; }
        void Parse(string data);
        void Parse2(System.IO.StreamReader reader, ref string line);
    }
    // sample config
    // unique by sid
    public abstract class SampleConfig : IConfig
    {
        public abstract string type { get; }
        public int sid { get; private set; }
        public virtual void Parse(string data) { }
        public virtual void Parse2(System.IO.StreamReader reader, ref string line) { }
    }
    // flat config
    // unique by string
    public abstract class FlatConfig : IConfig
    {
        public abstract string type { get; }
        public string key { get; /*private*/ set; }
        public virtual void Parse(string data) { }
        public virtual void Parse2(System.IO.StreamReader reader, ref string line) { }
    }
    #endregion

    #region config loader
    public interface IConfigLoader
    {
        void _loadOK(File file);
    }
    #endregion
}