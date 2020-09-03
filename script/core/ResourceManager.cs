using ExtractorSharp.Core.Coder;
using Game.Config;
using Godot;
using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace Core
{
    public sealed class ResourceManager : Node
    {
        [DebuggerHidden]
        public static ResourceManager Instance => GameManager.Instance.Get<ResourceManager>();

        public void Load(string path, Action<Resource> callback) {
            path = "res://" + path;
            this.StartCoroutine(load(path, callback));
        }

        public override void _ExitTree() {
            allNpkData.Clear();
            baseConfigs?.Dispose();
        }

        public override void _Process(float delta) {
            if (OS.IsDebugBuild()) {
                StringBuilder sb = new StringBuilder();
                sb.Append("npk count: ").Append(allNpkData.Count);
                EditorDescription = sb.ToString();
            }
        }

        IEnumerator load(string path, Action<Resource> callback) {
            Debug.Log("start load: " + path + " " + Time.time);
            var res = GD.Load(path);
            Debug.Log("finish load: " + path + " " + Time.time);
            callback(res);
            yield return null;
        }
        public void Load(string path, IConfigLoader loader) {
            Coroutine.Instance.StartOnTask(() =>
            {
                for (int i = 0; i < ResPath.Count; i++) {
                    var p = System.IO.Path.Combine(ResPath[i], path);
                    if (loader._loadOK(p))
                        break;
                }
            });

            //var f = new File();
            //Debug.LogError($"file no found: {path}");
            //callback?.Invoke(null);
        }

        #region ResPath

        // get the folder refer to resource
        // first : res/
        // then: res.txt 
        // then : res/res.txt
        private List<string> getResFolder() {
            List<string> paths = new List<string>();
            var dir = new Godot.Directory();
            if (dir.DirExists("res://res/"))
                paths.Add(GameManager.Instance.PrjPath + "res");
            dir.Dispose();

            using (var file = new Godot.File()) {
                if (!_getResFolder(file, "res://res.txt", paths)) {
                    if (!_getResFolder(file, "res://res/res.txt", paths)) {
                        if (paths.Count == 0)
                            Debug.LogError("no res folder found!");
                    }
                }
            }
            return paths;
        }
        private bool _getResFolder(Godot.File file, string path, List<string> paths) {
            if (file.FileExists(path)) {
                file.Open(path, File.ModeFlags.Read);
                var ss = file.GetAsText();
                if (string.IsNullOrEmpty(ss))
                    return false;
                var cfg = Hocon.HoconParser.Parse(ss);
                var res = cfg.GetString("res");
                if (res.valid() && !paths.Contains(res)) {
                    if (paths.Count == 0)
                        paths.Add(res);
                    else
                        paths[0] = res;
                }
                foreach (var val in cfg.AsEnumerable()) {
                    if (val.Key != "res" && !paths.Contains(val.Key))
                        paths.Add(val.Value.GetString());
                }
                file.Close();
                return true;
            }
            return false;
        }
        public List<string> ResPath => resPath ?? (resPath = getResFolder());
        private List<string> resPath;

        #endregion

        #region  npk sprite file

        public class NpkData
        {
            public string filePath;     // npk file
            public int index;           // npk image index
            public ExtractorSharp.Core.Model.Album album; // npk image data;
        }
        // type, path, data
        public Dictionary<string, NpkData> allNpkData { get; } = new Dictionary<string, NpkData>();


        [Signal]
        public delegate void load_npk_header_(int @count);
        public void LoadNpkHeader() {
            for (int i = 0; i < ResPath.Count; i++) {
                loadNpkHeader(System.IO.Path.Combine(ResPath[i], "sprite"));
                loadNpkHeader(System.IO.Path.Combine(ResPath[i], "ImagePacks2"));
            }

            if (allNpkData.Count == 0) {
                Debug.LogError("no npk file found!");
            }
            this.EmitSignal(nameof(load_npk_header_), allNpkData.Count);
        }

        private void loadNpkHeader(string dir) {
            if (!System.IO.Directory.Exists(dir))
                return;
            var files = System.IO.Directory.GetFiles(dir, "*.npk");
            for (int i = 0; i < files.Length; i++) {
                var albums = NpkCoder.Load(true, files[i]);
                for (int a = 0; a < albums.Count; a++) {
                    var npk = new NpkData
                    {
                        filePath = files[i],
                        index = a,
                        album = albums[a]
                    };

                    var path = albums[a].Path.Substring("sprite/".Length);
                    allNpkData[path] = npk;
                }
            }
            Debug.Log("load npk header : " + dir + " " + allNpkData.Count);
        }
        #endregion

        #region config
        // name, valus
        public IniData modConfigs { get; private set; }
        public void LoadModConfig() {
            for (int i = 1; i < ResPath.Count; i++) {
                loadNpkHeader(System.IO.Path.Combine(ResPath[i], "config"));
            }
        }
        private void loadModConfig(string dir) {
            if (!System.IO.Directory.Exists(dir))
                return;
            var files = System.IO.Directory.GetFiles(dir, "*.txt");
            for (int i = 0; i < files.Length; i++) {
                var cfg = new IniParser.FileIniDataParser().ReadFile(files[i]);
                if (modConfigs == null)
                    modConfigs = cfg;
                else
                    modConfigs.Merge(cfg);
            }
            Debug.Log("load mod config : " + dir + " " + modConfigs.Sections.Count);
        }
        public ZipArchive baseConfigs { get; private set; }
        public void LoadBaseConfig(ZipArchive @z) {
            baseConfigs = @z;
        }
        #endregion

        #region album
        public class ResUsage
        {
            public uint nowCount;
            public uint allCount;
            public bool cache;
            public float lastTime;
            //public uint stayInMem;
        }
        private Dictionary<string, ResUsage> resCache = new Dictionary<string, ResUsage>();

        public void LoadAnime(AnimeConfig config) {
            for (int i = 0; i < config.Frames.Length; i++) {
                if (!ResourceManager.Instance.allNpkData.ContainsKey(config.Frames[i].Image))
                    return;
                var npk = ResourceManager.Instance.allNpkData[config.Frames[i].Image];
                var sprite = config.Frames[i].ImageIdx;

                if (!resCache.ContainsKey(npk.filePath))
                    resCache.Add(npk.filePath, new ResUsage());
                var usage = resCache[npk.filePath];
                if (!usage.cache && check_memory()) {
                    npk.album.LoadImage(npk.filePath);

                    //usage.stayInMem += stayInMem ? 1 : 0;
                    usage.nowCount++;
                    usage.allCount++;
                    usage.cache = true;
                    usage.lastTime = Time.time;
                }
            }
        }

        public void UnloadAnime(AnimeConfig config) {
            for (int i = 0; i < config.Frames.Length; i++) {
                if (!ResourceManager.Instance.allNpkData.ContainsKey(config.Frames[i].Image))
                    return;
                var npk = ResourceManager.Instance.allNpkData[config.Frames[i].Image];
                var sprite = config.Frames[i].ImageIdx;

                if (!resCache.ContainsKey(npk.filePath))
                    resCache.Add(npk.filePath, new ResUsage());
                var usage = resCache[npk.filePath];
                if (usage.cache) {
                    npk.album.UnloadImage();
                    //usage.stayInMem -= stayInMem ? 1 : 0;
                    usage.nowCount--;
                    usage.cache = false;
                }
            }
        }

        public bool check_memory() {
            return true;
        }
        #endregion
    }
}