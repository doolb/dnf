
using System.IO.Compression;
using System.Threading.Tasks;
using Core;
using Godot;

namespace Game
{
    public class ScriptConfigLoader : IConfigLoaderRaw
    {
        public System.Collections.IEnumerator _loadRaw(File file) {
            Debug.Log("start load: " + file.GetPath());

            // get file content
            var len = file.GetLen();
            yield return new WaitForFrame(1);
            var buff = file.GetBuffer((int)len);
            Debug.Log("read file : " + len);

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
                        //Debug.Log(entry.FullName);
                        //yield return new WaitForFrame(1);
                        //using (var open = new System.IO.StreamReader(entry.Open())) {
                        //    //var txt = open.ReadToEnd();
                        //    //txt.log();
                        //}
                    }
                }
            }

            Debug.Log("zip file count : " + totalCount);
            Debug.Log("finish load: " + file.GetPath());
            System.GC.Collect();
        }
    }
}