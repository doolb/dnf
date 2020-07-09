using System;
using System.Collections.Generic;
using Core;

namespace Game.Config
{
    public class Box6D
    {
        public short Xmin, Xmax;
        public short Ymin, Ymax;
        public short Zmin, Zmax;

        public Box6D(string[] str) {
            Xmin = (short)str[0].ToInt();//short.Parse(str[0]);
            Xmax = short.Parse(str[1]);
            Ymin = short.Parse(str[2]);
            Ymax = short.Parse(str[3]);
            Zmin = short.Parse(str[4]);
            Zmax = short.Parse(str[5]);
        }
    }
    public class Box4D
    {
        public short Xmin, Xmax;
        public short Ymin, Ymax;

        public Box4D(string[] str) {
            Xmin = (short)str[0].ToInt();//short.Parse(str[0]);
            Ymin = short.Parse(str[1]);
            Xmax = short.Parse(str[2]);
            Ymax = short.Parse(str[3]);
        }
    }
    public class Color4D
    {
        public byte R, G, B, A;
        public Color4D(string[] str) {
            R = byte.Parse(str[0]);
            G = byte.Parse(str[1]);
            B = byte.Parse(str[2]);
            A = byte.Parse(str[3]);
        }
    }
    public class Color3D
    {
        public byte R, G, B;
        public Color3D(string[] str) {
            R = byte.Parse(str[0]);
            G = byte.Parse(str[1]);
            B = str.Length > 2 ? byte.Parse(str[2]) : (byte)0;
        }
    }
    public class Pos2D
    {
        public short X, Y;
        public Pos2D(string[] str) {
            X = short.Parse(str[0]);
            Y = short.Parse(str[1]);
        }
    }
    public class Pos2f
    {
        public float X, Y;
        public Pos2f(string[] str) {
            X = str[0].ToFloat();
            Y = str[1].ToFloat();
        }
    }
    public enum GraphicEffect
    {
        Normal,
        LINEARDODGE,
        DODGE,
        MONOCHROME, // change color
        DARK,
        XOR,
        SPACEDISTORT,
        NONE,
    }
    public enum DamageType
    {
        SUPERARMOR, //aicharacter/_jojochan/priest/hesiss/animation/sway.ani
        UNBREAKABLE, //aicharacter/fighter/sandman/animation/buff.ani
        NORMAL, //creature/sao/asuna/animation/asuna_overskill_dust01.ani
    }

    public enum FlipType
    {
        ALL,
        HORIZON, //character/common/animation/bustermode/buster_start_front_cross.ani
        VERTICAL, //character/fighter/effect/animation/athurricanespear/hit_dot_dodge.ani
    }

    public class AnimeFrame
    {
        // base image
        public string Image;
        public short ImageIdx;
        public Pos2D ImagePos;
        public Pos2f ImageRate;
        public float ImageRotate; // rotate on z

        // graphic
        public Color4D RGBA;
        public GraphicEffect GraphicEffect;
        public Color3D GraphicColor;

        // time
        public bool Interpolation;
        public short Delay;

        // fight
        public List<Box6D> DamageBox; // damageBox on character
        public Box6D AttackBox;
        public DamageType DamageType;
        public SoundType SoundType;

        // other
        public bool Preload;
        public int LoopMax;
        public bool LoopStart;
        public int SetFlag;
        public Box4D Clip;
        public FlipType FlipType;
    }

    public class Spectrum
    {
        public bool Enable;
        public short Term;
        public short LifeTime;
        public Color4D Color;
        public GraphicEffect Effect;
    }
    public class AnimeConfig : ResConfig
    {
        public bool Loop { get; private set; }
        public bool Shadow { get; private set; } = true;
        public bool Coord;
        public bool Operation;

        public AnimeFrame[] Frames { get; private set; }
        public Spectrum Spectrum { get; private set; }
        IConfigSource current_reader;
        public override void Parse(IConfigSource reader) {
            string current_key = "";
            current_reader = reader;
            while (reader.MoveNext()) {
                var line = reader.Line.Trim();
                if (string.IsNullOrEmpty(line))
                    continue;
                else if (line[0] == '#') // comment
                    continue;
                else if (line[0] == '[') // key
                    current_key = line.Substring(1, line.IndexOf(']') - 1);
                else if (!string.IsNullOrEmpty(current_key))
                    parseData(current_key, line);

                // is frame ?
                if (current_key.StartsWith("FRAME") && current_key != "FRAME MAX")
                    current_frame = Frames[current_key.ToInt()];
                else if (current_key == "LOOP START")
                    current_frame.LoopStart = true;
            }
        }
        AnimeFrame current_frame;
        // return true when need next line
        void parseData(string key, string line) {
            string nline = "";
            string[] ss = null;
            switch (key) {
                case "SHADOW":
                    Shadow = line.Trim().ToBool();
                    break;
                case "LOOP":
                    Loop = line.Trim().ToBool();
                    break;
                case "FRAME MAX":
                    Frames = new AnimeFrame[line.Trim().ToInt()];
                    for (int i = 0; i < Frames.Length; i++)
                        Frames[i] = new AnimeFrame();
                    break;
                // frame
                case "IMAGE":
                    current_frame.Image = line.RemoveQuotes().ToLower();
                    nline = current_reader.ReadLine();
                    current_frame.ImageIdx = nline.ToShort();
                    break;
                case "IMAGE POS":
                    if (current_frame.Image.valid()) { // is valid image
                        ss = line.Trim().Split('\t');
                        current_frame.ImagePos = new Pos2D(ss);
                    }
                    break;
                case "GRAPHIC EFFECT":
                    current_frame.GraphicEffect = (GraphicEffect)Enum.Parse(typeof(GraphicEffect), line.RemoveQuotes());
                    if (current_frame.GraphicEffect == GraphicEffect.MONOCHROME ||
                        current_frame.GraphicEffect == GraphicEffect.SPACEDISTORT) {
                        nline = current_reader.ReadLine();
                        current_frame.GraphicColor = new Color3D(nline.Trim().Split('\t'));
                    }
                    break;
                case "DELAY":
                    current_frame.Delay = line.Trim().ToShort();
                    break;
                case "RGBA":
                    ss = line.Trim().Split('\t');
                    current_frame.RGBA = new Color4D(ss);
                    break;
                case "INTERPOLATION":
                    current_frame.Interpolation = line.Trim().ToBool();
                    break;
                case "DAMAGE BOX":
                    if (current_frame.DamageBox == null)
                        current_frame.DamageBox = new List<Box6D>();
                    current_frame.DamageBox.Add(new Box6D(line.Trim().Split('\t')));
                    break;
                case "IMAGE RATE":
                    current_frame.ImageRate = new Pos2f(line.Trim().Split('\t'));
                    break;
                case "ATTACK BOX":
                    current_frame.AttackBox = new Box6D(line.Trim().Split('\t'));
                    break;
                case "DAMAGE TYPE":
                    current_frame.DamageType = line.RemoveQuotes().ToEnum<DamageType>();
                    break;
                case "PLAY SOUND":
                    current_frame.SoundType = line.RemoveQuotes().ToEnum<SoundType>();
                    break;
                case "IMAGE ROTATE":
                    current_frame.ImageRotate = line.Trim().ToFloat();
                    break;
                case "PRELOAD":
                    current_frame.Preload = line.Trim().ToBool();
                    break;
                case "SET FLAG": // aicharacter/gunner/bomb_man/animation/c4remoteexthrow.ani at 1
                    current_frame.SetFlag = int.Parse(line.Trim());
                    break;
                case "FLIP TYPE": // aicharacter/gunner/cypher_man/animation/earthbreakout3.ani at `ALL`
                    current_frame.FlipType = line.RemoveQuotes().ToEnum<FlipType>();
                    break;
                case "COORD": // character/common/animation/booster_particle_0.ani at 1
                    Coord = line.Trim().ToBool();
                    break;
                case "CLIP": // character/fighter/atanimation/blockbusterready.ani at -99	-200	200	200
                    current_frame.Clip = new Box4D(line.Trim().Split('\t'));
                    break;
                case "LOOP END": // character/fighter/atanimation/chargespearrecharge.ani at 999
                    current_frame.LoopMax = int.Parse(line.Trim());
                    break;
                case "OPERATION": // character/thief/effect/animation/silverstream/finishslasheffectback.ani at 1
                    Operation = line.Trim().ToBool();
                    break;
                // spectrum
                case "SPECTRUM": // aicharacter/_jojochan/swordman/godsword/animation/dash.ani at 1
                    Spectrum = new Spectrum();
                    Spectrum.Enable = line.Trim().ToBool();
                    break;
                case "SPECTRUM TERM": // aicharacter/_jojochan/swordman/godsword/animation/move.ani at 100
                    Spectrum.Term = short.Parse(line.Trim());
                    break;
                case "SPECTRUM LIFE TIME": // aicharacter/_jojochan/swordman/soldoros/animation/dash.ani at 500
                    Spectrum.LifeTime = short.Parse(line.Trim());
                    break;
                case "SPECTRUM COLOR": // aicharacter/_jojochan/swordman/soldoros/animation/move.ani at 10	10	155	100
                    Spectrum.Color = new Color4D(line.Trim().Split('\t'));
                    break;
                case "SPECTRUM EFFECT": // character/priest/animation/highspeedslashattack1.ani at `DARK`
                    Spectrum.Effect = line.RemoveQuotes().ToEnum<GraphicEffect>();
                    break;
                default:
                    ConfigUtils.RecordMiss(typeof(AnimeConfig), key, line.Trim());
                    break;
                    //throw new Exception(("unkown key: " + key));
                    //("unkown key: " + key).warning();
            }
        }
    }
}