using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    /// <summary>Represents an ordered pair of floating-point x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
    // Token: 0x02000047 RID: 71
    [ComVisible(true)]
    [Serializable]
    public struct PointF
    {
        /// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate. </param>
        /// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
        /// <returns>Returns the translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000704 RID: 1796 RVA: 0x000101CC File Offset: 0x0000E3CC
        public static PointF operator +(PointF pt, Size sz) {
            return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
        }

        /// <summary>Translates the <see cref="T:System.Drawing.PointF" /> by the specified <see cref="T:System.Drawing.SizeF" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the x- and y-coordinates of the <see cref="T:System.Drawing.PointF" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000705 RID: 1797 RVA: 0x000101F3 File Offset: 0x0000E3F3
        public static PointF operator +(PointF pt, SizeF sz) {
            return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>Compares two <see cref="T:System.Drawing.PointF" /> structures. The result specifies whether the values of the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> properties of the two <see cref="T:System.Drawing.PointF" /> structures are equal.</summary>
        /// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare. </param>
        /// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare. </param>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of the left and right <see cref="T:System.Drawing.PointF" /> structures are equal; otherwise, <see langword="false" />.</returns>
        // Token: 0x06000706 RID: 1798 RVA: 0x00010218 File Offset: 0x0000E418
        public static bool operator ==(PointF left, PointF right) {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>Determines whether the coordinates of the specified points are not equal.</summary>
        /// <param name="left">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
        /// <param name="right">A <see cref="T:System.Drawing.PointF" /> to compare.</param>
        /// <returns>
        ///     <see langword="true" /> to indicate the <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />. </returns>
        // Token: 0x06000707 RID: 1799 RVA: 0x0001023C File Offset: 0x0000E43C
        public static bool operator !=(PointF left, PointF right) {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000708 RID: 1800 RVA: 0x00010263 File Offset: 0x0000E463
        public static PointF operator -(PointF pt, Size sz) {
            return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
        }

        /// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified <see cref="T:System.Drawing.SizeF" />. </summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000709 RID: 1801 RVA: 0x0001028A File Offset: 0x0000E48A
        public static PointF operator -(PointF pt, SizeF sz) {
            return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.PointF" /> class with the specified coordinates.</summary>
        /// <param name="x">The horizontal position of the point. </param>
        /// <param name="y">The vertical position of the point. </param>
        // Token: 0x0600070A RID: 1802 RVA: 0x000102AF File Offset: 0x0000E4AF
        public PointF(float x, float y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.PointF" /> is empty.</summary>
        /// <returns>
        ///     <see langword="true" /> if both <see cref="P:System.Drawing.PointF.X" /> and <see cref="P:System.Drawing.PointF.Y" /> are 0; otherwise, <see langword="false" />.</returns>
        // Token: 0x1700020E RID: 526
        // (get) Token: 0x0600070B RID: 1803 RVA: 0x000102BF File Offset: 0x0000E4BF
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return (double)this.x == 0.0 && (double)this.y == 0.0;
            }
        }

        /// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
        /// <returns>The x-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x1700020F RID: 527
        // (get) Token: 0x0600070C RID: 1804 RVA: 0x000102E7 File Offset: 0x0000E4E7
        // (set) Token: 0x0600070D RID: 1805 RVA: 0x000102EF File Offset: 0x0000E4EF
        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.PointF" />.</summary>
        /// <returns>The y-coordinate of this <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x17000210 RID: 528
        // (get) Token: 0x0600070E RID: 1806 RVA: 0x000102F8 File Offset: 0x0000E4F8
        // (set) Token: 0x0600070F RID: 1807 RVA: 0x00010300 File Offset: 0x0000E500
        public float Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>Specifies whether this <see cref="T:System.Drawing.PointF" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.PointF" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x06000710 RID: 1808 RVA: 0x00010309 File Offset: 0x0000E509
        public override bool Equals(object obj) {
            return obj is PointF && this == (PointF)obj;
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Drawing.PointF" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.PointF" /> structure.</returns>
        // Token: 0x06000711 RID: 1809 RVA: 0x00010326 File Offset: 0x0000E526
        public override int GetHashCode() {
            return (int)this.x ^ (int)this.y;
        }

        /// <summary>Converts this <see cref="T:System.Drawing.PointF" /> to a human readable string.</summary>
        /// <returns>A string that represents this <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000712 RID: 1810 RVA: 0x00010337 File Offset: 0x0000E537
        public override string ToString() {
            return string.Format("{{X={0}, Y={1}}}", this.x.ToString(CultureInfo.CurrentCulture), this.y.ToString(CultureInfo.CurrentCulture));
        }

        /// <summary>Translates a given <see cref="T:System.Drawing.PointF" /> by the specified <see cref="T:System.Drawing.Size" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000713 RID: 1811 RVA: 0x00010363 File Offset: 0x0000E563
        public static PointF Add(PointF pt, Size sz) {
            return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
        }

        /// <summary>Translates a given <see cref="T:System.Drawing.PointF" /> by a specified <see cref="T:System.Drawing.SizeF" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000714 RID: 1812 RVA: 0x0001038A File Offset: 0x0000E58A
        public static PointF Add(PointF pt, SizeF sz) {
            return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified size.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000715 RID: 1813 RVA: 0x000103AF File Offset: 0x0000E5AF
        public static PointF Subtract(PointF pt, Size sz) {
            return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
        }

        /// <summary>Translates a <see cref="T:System.Drawing.PointF" /> by the negative of a specified size.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        /// <returns>The translated <see cref="T:System.Drawing.PointF" />.</returns>
        // Token: 0x06000716 RID: 1814 RVA: 0x000103D6 File Offset: 0x0000E5D6
        public static PointF Subtract(PointF pt, SizeF sz) {
            return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
        }

        // Token: 0x0400030F RID: 783
        private float x;

        // Token: 0x04000310 RID: 784
        private float y;

        /// <summary>Represents a new instance of the <see cref="T:System.Drawing.PointF" /> class with member data left uninitialized.</summary>
        // Token: 0x04000311 RID: 785
        public static readonly PointF Empty;
    }
}
