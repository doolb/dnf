using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractorSharp.Core.Lib;
using ExtractorSharp.Core.Model;

namespace ExtractorSharp.Core
{
    public interface ITexture {
        Size Size { get; }
        int Width { get;}
        int Height { get;}
        byte[] ToArray();
        byte[] ToArray(Model.ColorBits colorType);
    }

    public abstract class TextureUitls
    {
        private static TextureUitls instance;
        public static TextureUitls Instance { get { return instance ?? (instance = new GodotTextureUitls()); } }
        public abstract ITexture FromArray(byte[] data, Size size, Model.ColorBits bits);
        public abstract ITexture FromArray(byte[] data, Size size);

        public abstract ITexture Empty { get; }
    }

    public class GodotTexture : ITexture
    {
        public Godot.Image Image { get; private set; }
        public Size Size { get; private set; }
        public int Width => Size.Width;
        public int Height => Size.Height;

        public GodotTexture(Godot.Image img, Size size)
        {
            this.Image = img;
            this.Size = size;
        }
        public GodotTexture()
        {

        }

        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }

        public byte[] ToArray(ColorBits colorType)
        {
            throw new NotImplementedException();
        }
    }

    public class GodotTextureUitls : TextureUitls
    {
        private ITexture empty;
        public override ITexture Empty => empty ?? (empty = new GodotTexture());

        public override ITexture FromArray(byte[] data, Size size, ColorBits bits)
        {
            var ms = new MemoryStream(data);
            data = new byte[size.Width * size.Height * 4];
            for (var i = 0; i < data.Length; i += 4)
            {
                Colors.ReadColorRgba(ms, (int)bits, data, i);
            }
            ms.Close();

            var img = new Godot.Image();
            img.CreateFromData(size.Width, size.Height, false, Godot.Image.Format.Rgba8, data);
            GodotTexture tex = new GodotTexture(img, size);
            return tex;
        }

        // data form argb 8 bit
        public override ITexture FromArray(byte[] data, Size size)
        {
            var img = new Godot.Image();
            img.Create(size.Width, size.Height, false, Godot.Image.Format.Rgba8);
            img.Lock();
            for(int i=0; i<size.Width; i++)
                for(int j=0; j < size.Height; j++)
                {
                    var idx = j * size.Width + i;
                    var color = Godot.Color.Color8(data[idx], data[idx + 1], data[idx + 2], data[idx + 3]);
                    img.SetPixel(i, j, color);
                }
            img.Unlock();
            
            GodotTexture tex = new GodotTexture(img, size);
            return tex;
        }
    }
}