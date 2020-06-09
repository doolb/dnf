using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    /// <summary>Stores an ordered pair of integers, which specify a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" />.</summary>
    // Token: 0x0200004E RID: 78
    [ComVisible(true)]
    [TypeConverter(typeof(SizeConverter))]
    [Serializable]
    public struct Size
    {
        /// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.Size" /> structure to the next higher integer values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
        // Token: 0x060007B6 RID: 1974 RVA: 0x00011F70 File Offset: 0x00010170
        public static Size Ceiling(SizeF value) {
            checked {
                int num = (int)Math.Ceiling((double)value.Width);
                int num2 = (int)Math.Ceiling((double)value.Height);
                return new Size(num, num2);
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.SizeF" /> structure to the nearest integer values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
        // Token: 0x060007B7 RID: 1975 RVA: 0x00011FA0 File Offset: 0x000101A0
        public static Size Round(SizeF value) {
            checked {
                int num = (int)Math.Round((double)value.Width);
                int num2 = (int)Math.Round((double)value.Height);
                return new Size(num, num2);
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by truncating the values of the <see cref="T:System.Drawing.SizeF" /> structure to the next lower integer values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
        // Token: 0x060007B8 RID: 1976 RVA: 0x00011FD0 File Offset: 0x000101D0
        public static Size Truncate(SizeF value) {
            checked {
                int num = (int)value.Width;
                int num2 = (int)value.Height;
                return new Size(num, num2);
            }
        }

        /// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> to add. </param>
        /// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> to add. </param>
        /// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        // Token: 0x060007B9 RID: 1977 RVA: 0x00011FF4 File Offset: 0x000101F4
        public static Size operator +(Size sz1, Size sz2) {
            return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are equal.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the equality operator. </param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, <see langword="false" />.</returns>
        // Token: 0x060007BA RID: 1978 RVA: 0x00012019 File Offset: 0x00010219
        public static bool operator ==(Size sz1, Size sz2) {
            return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are different.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the inequality operator. </param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; <see langword="false" /> if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
        // Token: 0x060007BB RID: 1979 RVA: 0x0001203D File Offset: 0x0001023D
        public static bool operator !=(Size sz1, Size sz2) {
            return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
        }

        /// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator. </param>
        /// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the subtraction operation.</returns>
        // Token: 0x060007BC RID: 1980 RVA: 0x00012064 File Offset: 0x00010264
        public static Size operator -(Size sz1, Size sz2) {
            return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.Point" /> structure.</summary>
        /// <param name="size">The <see cref="T:System.Drawing.Size" /> structure to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Point" /> structure to which this operator converts.</returns>
        // Token: 0x060007BD RID: 1981 RVA: 0x00012089 File Offset: 0x00010289
        public static explicit operator Point(Size size) {
            return new Point(size.Width, size.Height);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="p">The <see cref="T:System.Drawing.Size" /> structure to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.SizeF" /> structure to which this operator converts.</returns>
        // Token: 0x060007BE RID: 1982 RVA: 0x0001209E File Offset: 0x0001029E
        public static implicit operator SizeF(Size p) {
            return new SizeF((float)p.Width, (float)p.Height);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified <see cref="T:System.Drawing.Point" /> structure.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.Point" /> structure from which to initialize this <see cref="T:System.Drawing.Size" /> structure. </param>
        // Token: 0x060007BF RID: 1983 RVA: 0x000120B5 File Offset: 0x000102B5
        public Size(Point pt) {
            this.width = pt.X;
            this.height = pt.Y;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified dimensions.</summary>
        /// <param name="width">The width component of the new <see cref="T:System.Drawing.Size" />. </param>
        /// <param name="height">The height component of the new <see cref="T:System.Drawing.Size" />. </param>
        // Token: 0x060007C0 RID: 1984 RVA: 0x000120D1 File Offset: 0x000102D1
        public Size(int width, int height) {
            this.width = width;
            this.height = height;
        }

        /// <summary>Tests whether this <see cref="T:System.Drawing.Size" /> structure has width and height of 0.</summary>
        /// <returns>This property returns <see langword="true" /> when this <see cref="T:System.Drawing.Size" /> structure has both a width and height of 0; otherwise, <see langword="false" />.</returns>
        // Token: 0x17000228 RID: 552
        // (get) Token: 0x060007C1 RID: 1985 RVA: 0x000120E1 File Offset: 0x000102E1
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.width == 0 && this.height == 0;
            }
        }

        /// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>The horizontal component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
        // Token: 0x17000229 RID: 553
        // (get) Token: 0x060007C2 RID: 1986 RVA: 0x000120F6 File Offset: 0x000102F6
        // (set) Token: 0x060007C3 RID: 1987 RVA: 0x000120FE File Offset: 0x000102FE
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        /// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
        // Token: 0x1700022A RID: 554
        // (get) Token: 0x060007C4 RID: 1988 RVA: 0x00012107 File Offset: 0x00010307
        // (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001210F File Offset: 0x0001030F
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        /// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.Size" /> structure with the same dimensions as this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Size" /> and has the same width and height as this <see cref="T:System.Drawing.Size" />; otherwise, <see langword="false" />.</returns>
        // Token: 0x060007C6 RID: 1990 RVA: 0x00012118 File Offset: 0x00010318
        public override bool Equals(object obj) {
            return obj is Size && this == (Size)obj;
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
        // Token: 0x060007C7 RID: 1991 RVA: 0x00012135 File Offset: 0x00010335
        public override int GetHashCode() {
            return this.width ^ this.height;
        }

        /// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>A string that represents this <see cref="T:System.Drawing.Size" />.</returns>
        // Token: 0x060007C8 RID: 1992 RVA: 0x00012144 File Offset: 0x00010344
        public override string ToString() {
            return string.Format("{{Width={0}, Height={1}}}", this.width, this.height);
        }

        /// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> structure to add.</param>
        /// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> structure to add.</param>
        /// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        // Token: 0x060007C9 RID: 1993 RVA: 0x00012166 File Offset: 0x00010366
        public static Size Add(Size sz1, Size sz2) {
            return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator. </param>
        /// <returns>A <see cref="T:System.Drawing.Size" /> structure that is a result of the subtraction operation.</returns>
        // Token: 0x060007CA RID: 1994 RVA: 0x0001218B File Offset: 0x0001038B
        public static Size Subtract(Size sz1, Size sz2) {
            return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        // Token: 0x0400032E RID: 814
        private int width;

        // Token: 0x0400032F RID: 815
        private int height;

        /// <summary>Gets a <see cref="T:System.Drawing.Size" /> structure that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0. </summary>
        /// <returns>A <see cref="T:System.Drawing.Size" /> that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0.</returns>
        // Token: 0x04000330 RID: 816
        public static readonly Size Empty;

        public static implicit operator Size(Godot.Vector2 vector2) {
            return new Size((int)vector2.x, (int)vector2.y);
        }
    }
}
