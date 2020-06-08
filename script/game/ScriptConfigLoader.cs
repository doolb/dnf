
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;
using Core;
using Godot;

namespace Game
{
    public class ScriptConfigLoader : IConfigLoader
    {
        public void _loadOK(Godot.File file) {
            Debug.Log($"start load: {file.GetPath()} {GC.GetTotalMemory(true)}");

            // get file content
            var len = file.GetLen();
            var buff = file.GetBuffer((int)len);
            Debug.Log($"read file : {len} {GC.GetTotalMemory(true)}");

            // get zip
            // cost much time to uncompress !!!
            int totalCount = 0;
            using (var steam = new System.IO.MemoryStream(buff)) {
                using (var zip = new ZipArchive(steam, ZipArchiveMode.Read)) {
                    foreach (var entry in zip.Entries) {
                        // skip empty file and directory
                        if (entry.Length == 0 || string.IsNullOrEmpty(entry.Name))
                            continue;
                        totalCount++;
                        var ext = System.IO.Path.GetExtension(entry.Name);
                        if (configType.ContainsKey(ext))
                            parseConfig(file, entry, configType[ext]);

                        //Debug.Log(entry.FullName);
                        //yield return new WaitForFrame(1);
                        //using (var open = new System.IO.StreamReader(entry.Open())) {
                        //    //var txt = open.ReadToEnd();
                        //    //txt.log();
                        //}
                    }
                }
            }

            Debug.Log($"zip file count : {totalCount}");
            Debug.Log($"finish load: {file.GetPath()} {GC.GetTotalMemory(true)}");

            // parse end
            // print the error config
            if (OS.GetName() == "Windows") {
                using (var _log = new StreamWriter(GameManager.Instance.PrjPath + "error.cfg")) {
                    _log.WriteLine(EnumExtension.PrintMissEnum());
                    _log.WriteLine(ConfigUtils.PrintMiss());
                }
                Debug.LogError($"erro config save in {GameManager.Instance.PrjPath}error.cfg.");
            }
            //System.GC.Collect();
        }

        static readonly Dictionary<string, Type> configType = new Dictionary<string, Type>()
        {
            [".ani"] = typeof(Game.Config.AnimeConfig),
        };

        void parseConfig(Godot.File file, ZipArchiveEntry entry, Type type) {
            string current_line = "";
            using (var open = new StreamReader(entry.Open())) {
                // use read line for less memory ??
                IConfig cfg = Activator.CreateInstance(type) as IConfig;
                try {
                    cfg.Parse2(open, ref current_line);
                }
                catch (EnumException _ee) {
                    EnumExtension.missEnum[_ee.enumType][_ee.missEnum] = entry.FullName; // save file name
                }
                catch (ConfigException _ce) {
                    ConfigUtils.missConfig[type][_ce.Key][1] = entry.FullName; // save file name
                }
                catch (Exception _e) {
                    var nmsg = $"parse config failed: {entry.FullName} {_e.Message}\n{current_line}\n{_e.StackTrace}";
                    throw new GameException(nmsg);
                }
                if (type.IsSubclassOf(typeof(SampleConfig))) {
                    ConfigManager.Instance.Add((SampleConfig)cfg);
                }
                else {
                    var flat = cfg as FlatConfig;
                    flat.key = entry.FullName;
                    ConfigManager.Instance.Add(flat);
                }
            }
        }
    }
}