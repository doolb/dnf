using ExtractorSharp.Core.Coder;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core
{
    public sealed class ResourceManager : Node
    {
        [DebuggerHidden]
        public static ResourceManager Instance => GameManager.Instance.Get<ResourceManager>();

        public class NpkData
        {
            public string filePath;     // npk file
            public int index;           // npk image index
            public ExtractorSharp.Core.Model.Album sprite; // npk image data;
        }
        // type, path, data
        public Dictionary<string, NpkData> allNpkData { get; } = new Dictionary<string, NpkData>();

        public void Load(string path, Action<Resource> callback) {
            path = "res://" + path;
            this.StartCoroutine(load(path, callback));
        }

        public override void _ExitTree() {
            allNpkData.Clear();
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
        public void Load(string path, Action<Godot.File> callback) {
            var f = new File();
            var p = "user://" + path;
            if (OS.IsDebugBuild())
                p = GameManager.Instance.PrjPath + path;
            if (!f.FileExists(p)) {
                Debug.LogError($"file no found: {p}");
                callback?.Invoke(null);
                return;
            }
            f.Open(p, File.ModeFlags.Read);
            // this.StartCoroutine(loader._loadRaw(f));
            Coroutine.Instance.StartOnTask(() => callback?.Invoke(f));
        }

        [Signal]
        public delegate void load_npk_header_(int @count);


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
                if (res.valid())
                    paths.Add(res);
                foreach(var val in cfg.AsEnumerable()){
                    if(val.Key != "res")
                        paths.Add(val.Value.GetString());
                }
                file.Close();
                return true;
            }
            return false;
        }

        public void LoadNpkHeader() {
            var resPath = getResFolder();
            for (int i = 0; i < resPath.Count; i++) {
                loadNpkHeader(System.IO.Path.Combine(resPath[i], "sprite"));
                loadNpkHeader(System.IO.Path.Combine(resPath[i], "ImagePacks2"));
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
                        index = a
                    };

                    var path = albums[a].Path.Substring("sprite/".Length);
                    allNpkData[path] = npk;
                }
            }
            Debug.Log("load npk header : " + dir + " " + allNpkData.Count);
        }
    }
}