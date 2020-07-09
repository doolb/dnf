
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
    public class SysFileLoader : IConfigLoader
    {
        public bool _loadOK(string file) {
            return false;
        }
        public static void LoadFile(string file, Action<Stream> loader) {
            if (!System.IO.File.Exists(file)) {
                return;
            }
            //using (var stream = new FileStream(file, FileMode.Open)) {
            //    loader(stream);
            //}
            var stream = new FileStream(file, FileMode.Open);
            loader(stream);
        }
    }
    public class SysZipLoader : IConfigLoader
    {
        public bool _loadOK(string file) {
            return false;
        }
        public static void LoadFile(string file, Action<ZipArchive> loader) {
            SysFileLoader.LoadFile(file, (@s) =>
            {
                Debug.Log($"read zip file : {file} {GC.GetTotalMemory(true)}");
                //using (var zip = new ZipArchive(@s)) {
                //    loader(zip);
                //}
                var zip = new ZipArchive(@s);
                loader(zip);
            });
        }
    }

    public class ScriptConfigLoader : IConfigLoader
    {
        public bool _loadOK(string path) {
            Debug.Log($"start load: {path} {GC.GetTotalMemory(true)}");
            bool ok = false;
            // get zip
            // cost much time to uncompress !!!
            int totalCount = 0;
            SysZipLoader.LoadFile(path, (@z) =>
            {
                ResourceManager.Instance.LoadBaseConfig(@z);
                totalCount += loadAllConfig(z, totalCount);

                totalCount += loadLanguage(z, parseList(z.GetEntry("n_string.lst")));
                totalCount += loadCharacter(z, parseList(z.GetEntry("n_string.lst")));
                Debug.Log($"zip file count : {totalCount}");
                Debug.Log($"finish load: {path} {GC.GetTotalMemory(true)}");
                Debug.Log($"sample: {ConfigManager.Instance.Sample} flat: {ConfigManager.Instance.Flat} res: {ConfigManager.Instance.Res}");

                // parse end
                // print the error config
                if (OS.GetName() == "Windows") {
                    using (var _log = new StreamWriter(GameManager.Instance.PrjPath + "error.cfg")) {
                        _log.WriteLine(EnumExtension.PrintMissEnum());
                        _log.WriteLine(ConfigUtils.PrintMiss());
                    }
                    Debug.LogError($"erro config save in {GameManager.Instance.PrjPath}error.cfg.");
                }
                System.GC.Collect();
                ConfigManager.Instance.EmitSignal(nameof(ConfigManager.load_config_ok));
                ok = true;
            });
            return ok;
        }

        private int loadAllConfig(ZipArchive z, int totalCount) {
            foreach (var entry in @z.Entries) {
                // skip empty file and directory
                if (entry.Length == 0 || string.IsNullOrEmpty(entry.Name))
                    continue;
                totalCount++;
                var ext = System.IO.Path.GetExtension(entry.Name);
                if (configType.ContainsKey(ext))
                    parseConfig(entry, configType[ext]);
                else if (flatConfigType.ContainsKey(ext))
                    parseConfig(entry, flatConfigType[ext]);
                else if (resConfigType.ContainsKey(ext))
                    parseRes(entry, resConfigType[ext]);
                //Debug.Log(entry.FullName);
                //yield return new WaitForFrame(1);
                //using (var open = new System.IO.StreamReader(entry.Open())) {
                //    //var txt = open.ReadToEnd();
                //    //txt.log();
                //}
            }

            return totalCount;
        }

        private int loadLanguage(ZipArchive @zip, Dictionary<int, string> lst) {
            int @langCount = 0;
            foreach (var f in lst) {
                using (var reader = getFile(zip, f.Value)) {
                    if (reader != null)
                        langCount += Language.Instance.ParseLanguage(f.Key, reader);
                }
            }
            Debug.Log($"load Lang : {lst.Count} {langCount} {GC.GetTotalMemory(true)}");
            return langCount;
        }
        private int loadCharacter(ZipArchive @zip, Dictionary<int, string> lst) {
            int @charCount = 0;
            foreach (var f in lst) {
                parseConfig(zip.GetEntry(f.Value), typeof(Config.Character.Config), f.Key);
            }
            return charCount;
        }

        static readonly Dictionary<string, Type> configType = new Dictionary<string, Type>()
        {
            //[".ani"] = typeof(Game.Config.AnimeConfig),
        };
        static readonly Dictionary<string, Type> flatConfigType = new Dictionary<string, Type>()
        {
            //[".ani"] = typeof(Game.Config.AnimeConfig),
        };
        static readonly Dictionary<string, Type> resConfigType = new Dictionary<string, Type>()
        {
            [".ani"] = typeof(Game.Config.AnimeConfig),
        };

        public static void parseConfig(ZipArchiveEntry entry, Type type, int sid = 0) {
            using (var open = new StreamReader(entry.Open())) {
                StreamConfigSource cfgSrc = new StreamConfigSource(open);
                IConfig cfg = Activator.CreateInstance(type) as IConfig;
                try {
                    cfg.Parse(cfgSrc);
                }
                catch (EnumException _ee) {
                    EnumExtension.missEnum[_ee.enumType][_ee.missEnum] = entry.FullName; // save file name
                }
                catch (ConfigException _ce) {
                    ConfigUtils.missConfig[type][_ce.Key][1] = entry.FullName; // save file name
                }
                catch (Exception _e) {
                    var nmsg = $"parse config failed: {entry.FullName} {_e.Message}\n{cfgSrc.Line}\n{_e.StackTrace}";
                    throw new GameException(nmsg);
                }
                if (type.IsSubclassOf(typeof(SampleConfig))) {
                    var samp = (SampleConfig)cfg;
                    samp.Sid = sid;
                    ConfigManager.Instance.Add(samp);
                }
                else if (type.IsSubclassOf(typeof(ResConfig))) {
                    var flat = cfg as ResConfig;
                    flat.key = entry.FullName;
                    ConfigManager.Instance.AddRes(flat);
                }
                else if (type.IsSubclassOf(typeof(FlatConfig))) {
                    var flat = cfg as FlatConfig;
                    flat.key = entry.FullName;
                    ConfigManager.Instance.Add(flat);
                }
            }
        }

        // config as resource, dont parse now
        void parseRes(ZipArchiveEntry entry, Type type) {
            ResConfig cfg = Activator.CreateInstance(type) as ResConfig;
            cfg.key = entry.FullName;
            ConfigManager.Instance.AddRes(cfg);
        }

        // parse the .lst file
        private Dictionary<int, string> parseList(ZipArchiveEntry @entry) {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            using (var open = new StreamReader(entry.Open())) {
                while (!open.EndOfStream) {
                    var line = open.ReadLine();
                    if (string.IsNullOrEmpty(line) || line[0] == '#')
                        continue;
                    // id \n `path`
                    if (line[0] != '`') {
                        var id = line.ToInt();
                        var path = open.ReadLine().RemoveQuotes().ToLower();
                        lst.Add(id, path);
                    }
                }
            }
            return lst;
        }
        System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding(950); // big 5
        private StreamReader getFile(ZipArchive @zip, string path) {
            var entry = zip.GetEntry(path);
            if (entry == null) {
                $"config file :{path} no found!".error();
                return null;
            }
            return new StreamReader(entry.Open(), Encoding, true);
        }
    }
}