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

        public class ResourcesData
        {

        }
        private readonly Dictionary<string, Dictionary<string, ResourcesData>> allDataList = new Dictionary<string, Dictionary<string, ResourcesData>>();

        public void Load(string path, Action<Resource> callback) {
            path = "res://" + path;
            this.StartCoroutine(load(path, callback));
        }

        public override void _ExitTree() {
            allDataList.Clear();
        }

        public override void _Process(float delta) {
            if (OS.IsDebugBuild()) {
                StringBuilder sb = new StringBuilder();
                sb.Append("count: ").Append(allDataList.Count);
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
    }
}