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

        [Signal]
        public delegate void load_config_ok();

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
        Dictionary<Type, Dictionary<int, SampleConfig>> sampleConfigDatas = new Dictionary<Type, Dictionary<int, SampleConfig>>();
        public int Sample => sampleConfigDatas.Count;
        Dictionary<Type, Dictionary<string, FlatConfig>> flatConfigDatas = new Dictionary<Type, Dictionary<string, FlatConfig>>();
        public int Flat => flatConfigDatas.Count;
        Dictionary<Type, Dictionary<string, ResConfig>> resConfigDatas = new Dictionary<Type, Dictionary<string, ResConfig>>();
        public Dictionary<Type, Dictionary<string, ResConfig>> ResCfgs => resConfigDatas;
        public int Res => resConfigDatas.Count;
        public void LoadConfig(string name, IConfigLoader loader) {
            ResourceManager.Instance.Load("config/" + name, loader);
        }

        // add config to dic
        public void Add(SampleConfig _config) {
            var key = _config.GetType();
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
            var key = _config.GetType();
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
        public void AddRes(ResConfig _config) {
            var key = _config.GetType();
            if (!resConfigDatas.ContainsKey(key)) {
                var cfgs = new Dictionary<string, ResConfig>();
                cfgs.Add(_config.key, _config);
                resConfigDatas.Add(key, cfgs);
            }
            else {
                var cfgs = resConfigDatas[key];
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
        public T Get<T>(int sid) where T : SampleConfig {
            var type = typeof(T);
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
        public T Get<T>(string key) where T : FlatConfig {
            var type = typeof(T);
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
        // get res config by key
        public T GetRes<T>(string key) where T : ResConfig {
            var type = typeof(T);
            if (resConfigDatas.ContainsKey(type)) {
                var cfgs = resConfigDatas[type];
                if (cfgs.ContainsKey(key)) {
                    if (cfgs[key].isinit)
                        return (T)cfgs[key];
                }
            }

            var data = ResourceManager.Instance.baseConfigs.GetEntry(key);
            if (data == null) {
                Debug.LogWarning($"config no found: {type} {key}");
                return null;
            }
            Game.ScriptConfigLoader.parseConfig(data, typeof(T));
            return (T)resConfigDatas[type][key];
        }
        public void UnloadRes(ResConfig res) {
            if (!res.isinit) return;
            var type = res.GetType();
            var key = res.key;
            if (resConfigDatas.ContainsKey(type)) {
                var cfgs = resConfigDatas[type];
                if (cfgs.ContainsKey(key)) {
                    cfgs.Remove(key);
                }
            }
        }
    }

    #region config base
    public interface IConfig
    {
        void Parse(IConfigSource data);
    }
    // sample config
    // unique by sid
    public abstract class SampleConfig : IConfig
    {
        public int sid { get; private set; }
        public int Sid
        {
            get => sid;
            set => sid = value;
        }
        public virtual void Parse(IConfigSource data) { }
    }
    // flat config
    // unique by string
    public abstract class FlatConfig : IConfig
    {
        public string key { get; /*private*/ set; }
        public virtual void Parse(IConfigSource data) { }
    }
    public abstract class ResConfig : FlatConfig
    {
        public bool isinit = false;
    }
    #endregion

    #region config loader
    public interface IConfigLoader
    {
        bool _loadOK(string file);
    }
    #endregion

    #region config source
    // specify the source we parse config from
    // can be a string
    // or a stream
    public interface IConfigSource : System.Collections.IEnumerator
    {
        int LineIdx { get; } // current line number, from 1 base
        string Line { get; } // current line data
        string ReadLine();   // get next line
    }
    public class StringConfigSource : IConfigSource
    {
        protected string data;
        public StringConfigSource(string @data) {
            this.data = @data;
        }
        public int LineIdx { get; private set; }
        public string Line { get; private set; }

        public object Current => throw new NotImplementedException();

        public bool MoveNext() {
            throw new NotImplementedException();
        }

        public string ReadLine() {
            throw new NotImplementedException();
        }

        public void Reset() {
            throw new NotImplementedException();
        }
    }
    public class StreamConfigSource : IConfigSource
    {
        private System.IO.StreamReader @stream;
        public StreamConfigSource(System.IO.StreamReader @stream) {
            this.stream = stream;
            if (stream == null)
                Debug.LogError("stream is null");
        }

        public int LineIdx { get; private set; }
        public string Line { get; private set; }

        public object Current => Line;

        public bool MoveNext() {
            if (!stream.EndOfStream) {
                LineIdx++;
                Line = stream.ReadLine();
                return true;
            }
            return false;
        }

        public string ReadLine() {
            if (MoveNext())
                return Line;
            throw new IndexOutOfRangeException("reach the end of data!");
        }

        public void Reset() {
            LineIdx = 0;
            stream.BaseStream.Position = 0;
        }
    }
    #endregion
}