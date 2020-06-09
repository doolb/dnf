using System.Drawing;
using ExtractorSharp.Core.Handle;
using ExtractorSharp.Core.Lib;
using ExtractorSharp.Json.Attr;

namespace ExtractorSharp.Core.Model {
    public class Sprite {
        [LSIgnore]
        private ITexture _image;

        /// <summary>
        ///     帧域宽高
        /// </summary>
        [LSIgnore]
        public Size FrameSize = Size.Empty;

        /// <summary>
        ///     压缩类型
        /// </summary>
        public CompressMode CompressMode = CompressMode.NONE;

        /// <summary>
        ///     贴图在V2,V4时的数据
        /// </summary>
        [LSIgnore]
        public byte[] Data = new byte[2];

        /// <summary>
        ///     贴图在img中的下标
        /// </summary>
        public int Index;


        /// <summary>
        ///     数据长度
        /// </summary>
        [LSIgnore]
        public int Length = 2;


        /// <summary>
        ///     贴图坐标
        /// </summary>
        [LSIgnore]
        public Point Location;

        /// <summary>
        ///     存储该贴图的img
        /// </summary>
        [LSIgnore]
        public Album Parent;

        /// <summary>
        ///     贴图宽高
        /// </summary>
        [LSIgnore]
        public Size Size = new Size(1, 1);

        /// <summary>
        ///     当贴图为链接贴图时所指向的贴图
        /// </summary>
        [LSIgnore]
        public Sprite Target;

        public Sprite() { }

        public Sprite(Album parent) {
            Parent = parent;
        }

        /// <summary>
        ///     色位
        /// </summary>
        public ColorBits Type { set; get; } = ColorBits.ARGB_1555;

        /// <summary>
        ///     贴图内容
        /// </summary>
        [LSIgnore]
        public ITexture Picture {
            get {
                if (Type == ColorBits.LINK) {
                    return Target?.Picture;
                }
                if (IsOpen) {
                    return _image;
                }
                return _image = Parent.ConvertToBitmap(this); //使用父容器解析
            }
            set {
                _image = value;
                if (value != null) {
                    Size = value.Size;
                }
            }
        }

        [LSIgnore]
        public bool IsOpen => _image != null;

        public int X {
            set => Location.X = value;
            get => Location.X;
        }

        public int Y {
            set => Location.Y = value;
            get => Location.Y;
        }

        public int Width {
            set => Size.Width = value;
            get => Size.Width;
        }

        public int Height {
            set => Size.Height = value;
            get => Size.Height;
        }

        public int FrameWidth {
            set => FrameSize = new Size(value, FrameHeight);
            get => FrameSize.Width;
        }

        public int FrameHeight {
            set => FrameSize = new Size(FrameWidth, value);
            get => FrameSize.Height;
        }

        /// <summary>
        ///     文件版本
        /// </summary>
        [LSIgnore]
        public ImgVersion Version => Parent.Version;

        [LSIgnore]
        public bool Hidden => Width * Height == 1 && CompressMode == CompressMode.NONE;


        public void Load() {
            _image = Parent.ConvertToBitmap(this); //使用父容器
        }
        

        /// <summary>
        ///     数据校正
        /// </summary>
        public virtual void Adjust() {
            if (Type == ColorBits.LINK) {
                Length = 0;
                return;
            }
            if (!IsOpen) {
                return;
            }
            Data = Parent.ConvertToByte(this);
            if (Data.Length > 0 && CompressMode >= CompressMode.ZLIB) Data = Zlib.Compress(Data);
            Length = Data.Length; //不压缩时，按原长度保存
        }


        public bool Equals(Sprite entity) {
            return entity != null && Parent.Equals(entity.Parent) && Index == entity.Index;
        }

        public override string ToString() {
            if (Type == ColorBits.LINK && Target != null) {
                return Index + "," + Language.Default["TargetIndex"] + Target.Index;
            }
            return Index + "," + Type + "," + Language.Default["Position"] + Location.GetString() + "," +
                   Language.Default["Size"] + Size.GetString() + "," + Language.Default["FrameSize"]  +
                   FrameSize.GetString();
        }

        public Sprite Clone(Album album) {
            return new Sprite(album) {
                Picture = Picture,
                CompressMode = CompressMode,
                Type = Type,
                Location = Location,
                FrameSize = FrameSize,
                Target = Target
            };
        }
    }

    /// <summary>
    ///     色位
    /// </summary>
    public enum ColorBits {
        ARGB_1555 = 0x0e,
        ARGB_4444 = 0x0f,
        ARGB_8888 = 0x10,
        LINK = 0x11,
        DXT_1 = 0x12,
        DXT_3 = 0x13,
        DXT_5 = 0x14,
        UNKNOWN = 0x00
    }

    /// <summary>
    ///     压缩类型
    /// </summary>
    public enum CompressMode {
        ZLIB = 0x06,
        NONE = 0x05,
        DDS_ZLIB = 0x07,
        UNKNOWN = 0x01
    }
}