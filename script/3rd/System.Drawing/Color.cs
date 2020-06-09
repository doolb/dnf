using System;
namespace System.Drawing
{
    [Serializable]
    public struct Color
    {
        internal long Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        internal long value;
        int state;
        public static Color FromArgb(int red, int green, int blue) {
            return Color.FromArgb(255, red, green, blue);
        }

        public static Color FromArgb(int alpha, int red, int green, int blue) {
            Color.CheckARGBValues(alpha, red, green, blue);
            return new Color
            {
                state = 2,
                Value = (long)((alpha << 24) + (red << 16) + (green << 8) + blue)
            };
        }

        public int ToArgb() {
            return (int)this.Value;
        }

        public static Color FromArgb(int alpha, Color baseColor) {
            return Color.FromArgb(alpha, (int)baseColor.R, (int)baseColor.G, (int)baseColor.B);
        }

        public static Color FromArgb(int argb) {
            return Color.FromArgb(argb >> 24 & 255, argb >> 16 & 255, argb >> 8 & 255, argb & 255);
        }

        public float GetBrightness() {
            byte b = Math.Min(this.R, Math.Min(this.G, this.B));
            return (float)(Math.Max(this.R, Math.Max(this.G, this.B)) + b) / 510f;
        }

        public float GetSaturation() {
            byte b = Math.Min(this.R, Math.Min(this.G, this.B));
            byte b2 = Math.Max(this.R, Math.Max(this.G, this.B));
            if (b2 == b) {
                return 0f;
            }
            int num = (int)(b2 + b);
            if (num > 255) {
                num = 510 - num;
            }
            return (float)(b2 - b) / (float)num;
        }
        public float GetHue() {
            int r = (int)this.R;
            int g = (int)this.G;
            int b = (int)this.B;
            byte b2 = (byte)Math.Min(r, Math.Min(g, b));
            byte b3 = (byte)Math.Max(r, Math.Max(g, b));
            if (b3 == b2) {
                return 0f;
            }
            float num = (float)(b3 - b2);
            float num2 = (float)((int)b3 - r) / num;
            float num3 = (float)((int)b3 - g) / num;
            float num4 = (float)((int)b3 - b) / num;
            float num5 = 0f;
            if (r == (int)b3) {
                num5 = 60f * (6f + num4 - num3);
            }
            if (g == (int)b3) {
                num5 = 60f * (2f + num2 - num4);
            }
            if (b == (int)b3) {
                num5 = 60f * (4f + num3 - num2);
            }
            if (num5 > 360f) {
                num5 -= 360f;
            }
            return num5;
        }
        public bool IsEmpty
        {
            get
            {
                return this.state == 0;
            }
        }
        public byte A
        {
            get
            {
                return (byte)(this.Value >> 24);
            }
        }

        /// <summary>Gets the red component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The red component value of this <see cref="T:System.Drawing.Color" />.</returns>
        // Token: 0x1700009C RID: 156
        // (get) Token: 0x06000132 RID: 306 RVA: 0x00004718 File Offset: 0x00002918
        public byte R
        {
            get
            {
                return (byte)(this.Value >> 16);
            }
        }

        /// <summary>Gets the green component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The green component value of this <see cref="T:System.Drawing.Color" />.</returns>
        // Token: 0x1700009D RID: 157
        // (get) Token: 0x06000133 RID: 307 RVA: 0x00004724 File Offset: 0x00002924
        public byte G
        {
            get
            {
                return (byte)(this.Value >> 8);
            }
        }

        /// <summary>Gets the blue component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
        /// <returns>The blue component value of this <see cref="T:System.Drawing.Color" />.</returns>
        // Token: 0x1700009E RID: 158
        // (get) Token: 0x06000134 RID: 308 RVA: 0x0000472F File Offset: 0x0000292F
        public byte B
        {
            get
            {
                return (byte)this.Value;
            }
        }
        public override string ToString() {
            if (this.IsEmpty) {
                return "Color [Empty]";
            }
            return string.Format("Color [A={0}, R={1}, G={2}, B={3}]", new object[]
            {
                this.A,
                this.R,
                this.G,
                this.B
            });
        }
        private static void CheckARGBValues(int alpha, int red, int green, int blue) {
            if (alpha > 255 || alpha < 0) {
                throw Color.CreateColorArgumentException(alpha, "alpha");
            }
            Color.CheckRGBValues(red, green, blue);
        }
        private static ArgumentException CreateColorArgumentException(int value, string color) {
            return new ArgumentException(string.Format("'{0}' is not a valid value for '{1}'. '{1}' should be greater or equal to 0 and less than or equal to 255.", value, color));
        }
        private static void CheckRGBValues(int red, int green, int blue) {
            if (red > 255 || red < 0) {
                throw Color.CreateColorArgumentException(red, "red");
            }
            if (green > 255 || green < 0) {
                throw Color.CreateColorArgumentException(green, "green");
            }
            if (blue > 255 || blue < 0) {
                throw Color.CreateColorArgumentException(blue, "blue");
            }
        }
    }
}