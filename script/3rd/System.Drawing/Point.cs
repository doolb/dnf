using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    /// <summary>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</summary>
    // Token: 0x02000046 RID: 70
    [ComVisible(true)]
    [TypeConverter(typeof(PointConverter))]
    [Serializable]
    public struct Point
    {
        /// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by rounding the values of the <see cref="T:System.Drawing.PointF" /> to the next higher integer values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
        // Token: 0x060006EC RID: 1772 RVA: 0x0000FF34 File Offset: 0x0000E134
        public static Point Ceiling(PointF value) {
            checked {
                int num = (int)Math.Ceiling((double)value.X);
                int num2 = (int)Math.Ceiling((double)value.Y);
                return new Point(num, num2);
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> object by rounding the <see cref="T:System.Drawing.Point" /> values to the nearest integer.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
        // Token: 0x060006ED RID: 1773 RVA: 0x0000FF64 File Offset: 0x0000E164
        public static Point Round(PointF value) {
            checked {
                int num = (int)Math.Round((double)value.X);
                int num2 = (int)Math.Round((double)value.Y);
                return new Point(num, num2);
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.PointF" /> to a <see cref="T:System.Drawing.Point" /> by truncating the values of the <see cref="T:System.Drawing.Point" />.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.PointF" /> to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.Point" /> this method converts to.</returns>
        // Token: 0x060006EE RID: 1774 RVA: 0x0000FF94 File Offset: 0x0000E194
        public static Point Truncate(PointF value) {
            checked {
                int num = (int)value.X;
                int num2 = (int)value.Y;
                return new Point(num, num2);
            }
        }

        /// <summary>Translates a <see cref="T:System.Drawing.Point" /> by a given <see cref="T:System.Drawing.Size" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate. </param>
        /// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
        /// <returns>The translated <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x060006EF RID: 1775 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
        public static Point operator +(Point pt, Size sz) {
            return new Point(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are equal.</summary>
        /// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare. </param>
        /// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare. </param>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
        // Token: 0x060006F0 RID: 1776 RVA: 0x0000FFDD File Offset: 0x0000E1DD
        public static bool operator ==(Point left, Point right) {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>Compares two <see cref="T:System.Drawing.Point" /> objects. The result specifies whether the values of the <see cref="P:System.Drawing.Point.X" /> or <see cref="P:System.Drawing.Point.Y" /> properties of the two <see cref="T:System.Drawing.Point" /> objects are unequal.</summary>
        /// <param name="left">A <see cref="T:System.Drawing.Point" /> to compare. </param>
        /// <param name="right">A <see cref="T:System.Drawing.Point" /> to compare. </param>
        /// <returns>
        ///     <see langword="true" /> if the values of either the <see cref="P:System.Drawing.Point.X" /> properties or the <see cref="P:System.Drawing.Point.Y" /> properties of <paramref name="left" /> and <paramref name="right" /> differ; otherwise, <see langword="false" />.</returns>
        // Token: 0x060006F1 RID: 1777 RVA: 0x00010001 File Offset: 0x0000E201
        public static bool operator !=(Point left, Point right) {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>Translates a <see cref="T:System.Drawing.Point" /> by the negative of a given <see cref="T:System.Drawing.Size" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.Point" /> to translate. </param>
        /// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the pair of numbers to subtract from the coordinates of <paramref name="pt" />. </param>
        /// <returns>A <see cref="T:System.Drawing.Point" /> structure that is translated by the negative of a given <see cref="T:System.Drawing.Size" /> structure.</returns>
        // Token: 0x060006F2 RID: 1778 RVA: 0x00010028 File Offset: 0x0000E228
        public static Point operator -(Point pt, Size sz) {
            return new Point(pt.X - sz.Width, pt.Y - sz.Height);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
        /// <returns>The <see cref="T:System.Drawing.Size" /> that results from the conversion.</returns>
        // Token: 0x060006F3 RID: 1779 RVA: 0x0001004D File Offset: 0x0000E24D
        public static explicit operator Size(Point p) {
            return new Size(p.X, p.Y);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Point" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
        /// <param name="p">The <see cref="T:System.Drawing.Point" /> to be converted.</param>
        /// <returns>The <see cref="T:System.Drawing.PointF" /> that results from the conversion.</returns>
        // Token: 0x060006F4 RID: 1780 RVA: 0x00010062 File Offset: 0x0000E262
        public static implicit operator PointF(Point p) {
            return new PointF((float)p.X, (float)p.Y);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class using coordinates specified by an integer value.</summary>
        /// <param name="dw">A 32-bit integer that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />. </param>
        // Token: 0x060006F5 RID: 1781 RVA: 0x00010079 File Offset: 0x0000E279
        public Point(int dw) {
            this.y = dw >> 16;
            this.x = (int)((short)(dw & 65535));
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class from a <see cref="T:System.Drawing.Size" />.</summary>
        /// <param name="sz">A <see cref="T:System.Drawing.Size" /> that specifies the coordinates for the new <see cref="T:System.Drawing.Point" />. </param>
        // Token: 0x060006F6 RID: 1782 RVA: 0x00010093 File Offset: 0x0000E293
        public Point(Size sz) {
            this.x = sz.Width;
            this.y = sz.Height;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Point" /> class with the specified coordinates.</summary>
        /// <param name="x">The horizontal position of the point. </param>
        /// <param name="y">The vertical position of the point. </param>
        // Token: 0x060006F7 RID: 1783 RVA: 0x000100AF File Offset: 0x0000E2AF
        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Point" /> is empty.</summary>
        /// <returns>
        ///     <see langword="true" /> if both <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> are 0; otherwise, <see langword="false" />.</returns>
        // Token: 0x1700020B RID: 523
        // (get) Token: 0x060006F8 RID: 1784 RVA: 0x000100BF File Offset: 0x0000E2BF
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.x == 0 && this.y == 0;
            }
        }

        /// <summary>Gets or sets the x-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
        /// <returns>The x-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x1700020C RID: 524
        // (get) Token: 0x060006F9 RID: 1785 RVA: 0x000100D4 File Offset: 0x0000E2D4
        // (set) Token: 0x060006FA RID: 1786 RVA: 0x000100DC File Offset: 0x0000E2DC
        public int X
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

        /// <summary>Gets or sets the y-coordinate of this <see cref="T:System.Drawing.Point" />.</summary>
        /// <returns>The y-coordinate of this <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x1700020D RID: 525
        // (get) Token: 0x060006FB RID: 1787 RVA: 0x000100E5 File Offset: 0x0000E2E5
        // (set) Token: 0x060006FC RID: 1788 RVA: 0x000100ED File Offset: 0x0000E2ED
        public int Y
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

        /// <summary>Specifies whether this <see cref="T:System.Drawing.Point" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Point" /> and has the same coordinates as this <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x060006FD RID: 1789 RVA: 0x000100F6 File Offset: 0x0000E2F6
        public override bool Equals(object obj) {
            return obj is Point && this == (Point)obj;
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Drawing.Point" />.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x060006FE RID: 1790 RVA: 0x00010113 File Offset: 0x0000E313
        public override int GetHashCode() {
            return this.x ^ this.y;
        }

        /// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified amount.</summary>
        /// <param name="dx">The amount to offset the x-coordinate. </param>
        /// <param name="dy">The amount to offset the y-coordinate. </param>
        // Token: 0x060006FF RID: 1791 RVA: 0x00010122 File Offset: 0x0000E322
        public void Offset(int dx, int dy) {
            this.x += dx;
            this.y += dy;
        }

        /// <summary>Converts this <see cref="T:System.Drawing.Point" /> to a human-readable string.</summary>
        /// <returns>A string that represents this <see cref="T:System.Drawing.Point" />.</returns>
        // Token: 0x06000700 RID: 1792 RVA: 0x00010140 File Offset: 0x0000E340
        public override string ToString() {
            return string.Format("{{X={0},Y={1}}}", this.x.ToString(CultureInfo.InvariantCulture), this.y.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>Adds the specified <see cref="T:System.Drawing.Size" /> to the specified <see cref="T:System.Drawing.Point" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.Point" /> to add.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.Size" /> to add</param>
        /// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the addition operation.</returns>
        // Token: 0x06000701 RID: 1793 RVA: 0x0001016C File Offset: 0x0000E36C
        public static Point Add(Point pt, Size sz) {
            return new Point(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>Translates this <see cref="T:System.Drawing.Point" /> by the specified <see cref="T:System.Drawing.Point" />.</summary>
        /// <param name="p">The <see cref="T:System.Drawing.Point" /> used offset this <see cref="T:System.Drawing.Point" />.</param>
        // Token: 0x06000702 RID: 1794 RVA: 0x00010191 File Offset: 0x0000E391
        public void Offset(Point p) {
            this.Offset(p.X, p.Y);
        }

        /// <summary>Returns the result of subtracting specified <see cref="T:System.Drawing.Size" /> from the specified <see cref="T:System.Drawing.Point" />.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.Point" /> to be subtracted from. </param>
        /// <param name="sz">The <see cref="T:System.Drawing.Size" /> to subtract from the <see cref="T:System.Drawing.Point" />.</param>
        /// <returns>The <see cref="T:System.Drawing.Point" /> that is the result of the subtraction operation.</returns>
        // Token: 0x06000703 RID: 1795 RVA: 0x000101A7 File Offset: 0x0000E3A7
        public static Point Subtract(Point pt, Size sz) {
            return new Point(pt.X - sz.Width, pt.Y - sz.Height);
        }

        // Token: 0x0400030C RID: 780
        private int x;

        // Token: 0x0400030D RID: 781
        private int y;

        /// <summary>Represents a <see cref="T:System.Drawing.Point" /> that has <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values set to zero. </summary>
        // Token: 0x0400030E RID: 782
        public static readonly Point Empty;
    }
}
