using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    /// <summary>Stores an ordered pair of floating-point numbers, typically the width and height of a rectangle.</summary>
    // Token: 0x0200004F RID: 79
    [ComVisible(true)]
    [TypeConverter(typeof(SizeFConverter))]
    [Serializable]
    public struct SizeF
    {
        /// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add. </param>
        /// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add. </param>
        /// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        // Token: 0x060007CB RID: 1995 RVA: 0x000121B0 File Offset: 0x000103B0
        public static SizeF operator +(SizeF sz1, SizeF sz2) {
            return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are equal.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the equality operator. </param>
        /// <returns>This operator returns <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, <see langword="false" />.</returns>
        // Token: 0x060007CC RID: 1996 RVA: 0x000121D5 File Offset: 0x000103D5
        public static bool operator ==(SizeF sz1, SizeF sz2) {
            return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are different.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the inequality operator. </param>
        /// <returns>This operator returns <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; <see langword="false" /> if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
        // Token: 0x060007CD RID: 1997 RVA: 0x000121F9 File Offset: 0x000103F9
        public static bool operator !=(SizeF sz1, SizeF sz2) {
            return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
        }

        /// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator. </param>
        /// <returns>A <see cref="T:System.Drawing.SizeF" /> that is the result of the subtraction operation.</returns>
        // Token: 0x060007CE RID: 1998 RVA: 0x00012220 File Offset: 0x00010420
        public static SizeF operator -(SizeF sz1, SizeF sz2) {
            return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
        /// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure to be converted</param>
        /// <returns>The <see cref="T:System.Drawing.PointF" /> structure to which this operator converts.</returns>
        // Token: 0x060007CF RID: 1999 RVA: 0x00012245 File Offset: 0x00010445
        public static explicit operator PointF(SizeF size) {
            return new PointF(size.Width, size.Height);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified <see cref="T:System.Drawing.PointF" /> structure.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> structure from which to initialize this <see cref="T:System.Drawing.SizeF" /> structure. </param>
        // Token: 0x060007D0 RID: 2000 RVA: 0x0001225A File Offset: 0x0001045A
        public SizeF(PointF pt) {
            this.width = pt.X;
            this.height = pt.Y;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified existing <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure from which to create the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
        // Token: 0x060007D1 RID: 2001 RVA: 0x00012276 File Offset: 0x00010476
        public SizeF(SizeF size) {
            this.width = size.Width;
            this.height = size.Height;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified dimensions.</summary>
        /// <param name="width">The width component of the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
        /// <param name="height">The height component of the new <see cref="T:System.Drawing.SizeF" /> structure. </param>
        // Token: 0x060007D2 RID: 2002 RVA: 0x00012292 File Offset: 0x00010492
        public SizeF(float width, float height) {
            this.width = width;
            this.height = height;
        }

        /// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.SizeF" /> structure has zero width and height.</summary>
        /// <returns>This property returns <see langword="true" /> when this <see cref="T:System.Drawing.SizeF" /> structure has both a width and height of zero; otherwise, <see langword="false" />.</returns>
        // Token: 0x1700022B RID: 555
        // (get) Token: 0x060007D3 RID: 2003 RVA: 0x000122A2 File Offset: 0x000104A2
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return (double)this.width == 0.0 && (double)this.height == 0.0;
            }
        }

        /// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <returns>The horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
        // Token: 0x1700022C RID: 556
        // (get) Token: 0x060007D4 RID: 2004 RVA: 0x000122CA File Offset: 0x000104CA
        // (set) Token: 0x060007D5 RID: 2005 RVA: 0x000122D2 File Offset: 0x000104D2
        public float Width
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

        /// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
        // Token: 0x1700022D RID: 557
        // (get) Token: 0x060007D6 RID: 2006 RVA: 0x000122DB File Offset: 0x000104DB
        // (set) Token: 0x060007D7 RID: 2007 RVA: 0x000122E3 File Offset: 0x000104E3
        public float Height
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

        /// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.SizeF" /> structure with the same dimensions as this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.SizeF" /> and has the same width and height as this <see cref="T:System.Drawing.SizeF" />; otherwise, <see langword="false" />.</returns>
        // Token: 0x060007D8 RID: 2008 RVA: 0x000122EC File Offset: 0x000104EC
        public override bool Equals(object obj) {
            return obj is SizeF && this == (SizeF)obj;
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
        // Token: 0x060007D9 RID: 2009 RVA: 0x00012309 File Offset: 0x00010509
        public override int GetHashCode() {
            return (int)this.width ^ (int)this.height;
        }

        /// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
        /// <returns>Returns a <see cref="T:System.Drawing.PointF" /> structure.</returns>
        // Token: 0x060007DA RID: 2010 RVA: 0x0001231A File Offset: 0x0001051A
        public PointF ToPointF() {
            return new PointF(this.width, this.height);
        }

        /// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>Returns a <see cref="T:System.Drawing.Size" /> structure.</returns>
        // Token: 0x060007DB RID: 2011 RVA: 0x00012330 File Offset: 0x00010530
        public Size ToSize() {
            checked {
                int num = (int)this.width;
                int num2 = (int)this.height;
                return new Size(num, num2);
            }
        }

        /// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <returns>A string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</returns>
        // Token: 0x060007DC RID: 2012 RVA: 0x00012352 File Offset: 0x00010552
        public override string ToString() {
            return string.Format("{{Width={0}, Height={1}}}", this.width.ToString(CultureInfo.CurrentCulture), this.height.ToString(CultureInfo.CurrentCulture));
        }

        /// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
        /// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
        /// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is the result of the addition operation.</returns>
        // Token: 0x060007DD RID: 2013 RVA: 0x0001237E File Offset: 0x0001057E
        public static SizeF Add(SizeF sz1, SizeF sz2) {
            return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
        /// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator. </param>
        /// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is a result of the subtraction operation.</returns>
        // Token: 0x060007DE RID: 2014 RVA: 0x000123A3 File Offset: 0x000105A3
        public static SizeF Subtract(SizeF sz1, SizeF sz2) {
            return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        // Token: 0x04000331 RID: 817
        private float width;

        // Token: 0x04000332 RID: 818
        private float height;

        /// <summary>Gets a <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0. </summary>
        /// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0.</returns>
        // Token: 0x04000333 RID: 819
        public static readonly SizeF Empty;
    }
}
