using Core;
using ExtractorSharp.Core;
using Game.Config;
using Godot;

namespace Game.Render
{
    public class AnimeRender : Godot.TextureRect
    {
        public float animeTime;
        public int animeIndex;
        public bool isshow = false;
        public AnimeConfig Data { get; private set; }
        ImageTexture tex = new ImageTexture();
        bool newFrame = false;

        public override void _Process(float delta) {
            if (!isshow)
                return;
            animeTime += delta;
            // next frame
            if (animeTime >= Data.Frames[animeIndex].Delay / 1000.0) {
                animeTime = 0;
                animeIndex++;
                newFrame = true;
            }
            // reach end
            if (animeIndex >= Data.Frames.Length) {
                if (Data.Loop) {
                    animeTime = 0;
                    animeIndex = 0;
                    newFrame = true;
                }
                else {
                    isshow = false;
                    newFrame = false;
                }
            }

            if (newFrame) {
                newFrame = false;
                if (!ResourceManager.Instance.allNpkData.ContainsKey(Data.Frames[animeIndex].Image)) {
                    Debug.LogError($"npk file now found : {Data.Frames[animeIndex].Image}");
                    return;
                }
                var npk = ResourceManager.Instance.allNpkData[Data.Frames[animeIndex].Image];
                var idx = Data.Frames[animeIndex].ImageIdx;
                if (idx >= npk.album.List.Count) {
                    Debug.Log($"npk image now found : {npk.album.Path} {idx} {npk.filePath}");
                    return;
                }

                Debug.Log($"select : {npk.album.Path} {idx} {npk.filePath}");
                if (npk.album.List[idx].Picture is GodotTexture godotText) {
                    tex.CreateFromImage(godotText.Image);
                }
                Texture = tex;
            }
        }

        public void Show(AnimeConfig data, int index = 0) {
            load_image(data);
            animeTime = 0;
            animeIndex = index;
            isshow = true;
            newFrame = true;
        }

        private void load_image(AnimeConfig cfg) {
            Data = cfg;
            ResourceManager.Instance.LoadAnime(cfg);
        }
    }
}