using System;
using System.Collections.Generic;
using System.IO;
using Core;

namespace Game
{
    public class Language : IDisposable
    {
        public static Language Instance => SingleManager.Instance.Get<Language>();
        private readonly Dictionary<int, Dictionary<string, string>> LangDic = new Dictionary<int, Dictionary<string, string>>();

        public string this[int id, string key]
        {
            get
            {
                if (!LangDic.ContainsKey(id))
                    return $"Lang,{id}:{key},no found";
                if (!LangDic[id].ContainsKey(key))
                    return $"Lang,{id}:{key},no found";
                return LangDic[id][key];
            }
        }

        public int ParseLanguage(int id, StreamReader reader) {
            int cnt = 0;
            while (!reader.EndOfStream) {
                var line = reader.ReadLine();
                if (is_comment(line))
                    continue;
                line = line.Trim();
                var idx = line.IndexOf('>');
                if (idx == -1) {
                    $"lang {id} : {line}".error();
                    continue;
                }
                add_lang(id, line.Substring(0, idx), line.Substring(idx + 1, line.Length - idx - 1));
                cnt++;
            }
            return cnt;
        }

        private bool is_comment(string line) {
            return string.IsNullOrEmpty(line) || (line[0] == '/' && line[1] == '/') || line.TrimStart().StartsWith("//");
        }

        private void add_lang(int id, string key, string lang) {
            if (!LangDic.ContainsKey(id))
                LangDic.Add(id, new Dictionary<string, string>());
            if (!LangDic[id].ContainsKey(key))
                LangDic[id].Add(key, lang);
            else
                $"Lang conflicted : {id}:{key} -> {LangDic[id][key]} -- lang".error();
        }

        public void Dispose() {
            LangDic.Clear();
        }
    }
}