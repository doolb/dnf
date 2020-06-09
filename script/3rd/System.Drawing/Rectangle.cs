using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    /// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
    // Token: 0x02000049 RID: 73
    [ComVisible(true)]
    [TypeConverter(typeof(RectangleConverter))]
    [Serializable]
    public struct Rectangle
    {
        /// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> structure to a <see cref="T:System.Drawing.Rectangle" /> structure by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the next higher integer values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> structure to be converted. </param>
        /// <returns>Returns a <see cref="T:System.Drawing.Rectangle" />.</returns>
        // Token: 0x06000720 RID: 1824 RVA: 0x00010738 File Offset: 0x0000E938
        public static Rectangle Ceiling(RectangleF value) {
            checked {
                int num = (int)Math.Ceiling((double)value.X);
                int num2 = (int)Math.Ceiling((double)value.Y);
                int num3 = (int)Math.Ceiling((double)value.Width);
                int num4 = (int)Math.Ceiling((double)value.Height);
                return new Rectangle(num, num2, num3, num4);
            }
        }

        /// <summary>Creates a <see cref="T:System.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
        /// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
        /// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
        /// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure. </param>
        /// <returns>The new <see cref="T:System.Drawing.Rectangle" /> that this method creates.</returns>
        // Token: 0x06000721 RID: 1825 RVA: 0x00010788 File Offset: 0x0000E988
        public static Rectangle FromLTRB(int left, int top, int right, int bottom) {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.Rectangle" /> structure. The copy is enlarged by the specified amount. The original <see cref="T:System.Drawing.Rectangle" /> structure remains unmodified.</summary>
        /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to start. This rectangle is not modified. </param>
        /// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally. </param>
        /// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically. </param>
        /// <returns>The enlarged <see cref="T:System.Drawing.Rectangle" />.</returns>
        // Token: 0x06000722 RID: 1826 RVA: 0x00010798 File Offset: 0x0000E998
        public static Rectangle Inflate(Rectangle rect, int x, int y) {
            Rectangle result = new Rectangle(rect.Location, rect.Size);
            result.Inflate(x, y);
            return result;
        }

        /// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
        /// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally. </param>
        /// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically. </param>
        // Token: 0x06000723 RID: 1827 RVA: 0x000107C4 File Offset: 0x0000E9C4
        public void Inflate(int width, int height) {
            this.Inflate(new Size(width, height));
        }

        /// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
        /// <param name="size">The amount to inflate this rectangle. </param>
        // Token: 0x06000724 RID: 1828 RVA: 0x000107D4 File Offset: 0x0000E9D4
        public void Inflate(Size size) {
            this.x -= size.Width;
            this.y -= size.Height;
            this.Width += size.Width * 2;
            this.Height += size.Height * 2;
        }

        /// <summary>Returns a third <see cref="T:System.Drawing.Rectangle" /> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle" /> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle" /> is returned.</summary>
        /// <param name="a">A rectangle to intersect. </param>
        /// <param name="b">A rectangle to intersect. </param>
        /// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
        // Token: 0x06000725 RID: 1829 RVA: 0x00010838 File Offset: 0x0000EA38
        public static Rectangle Intersect(Rectangle a, Rectangle b) {
            if (!a.IntersectsWithInclusive(b)) {
                return Rectangle.Empty;
            }
            return Rectangle.FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
        }

        /// <summary>Replaces this <see cref="T:System.Drawing.Rectangle" /> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle" />.</summary>
        /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to intersect. </param>
        // Token: 0x06000726 RID: 1830 RVA: 0x000108A6 File Offset: 0x0000EAA6
        public void Intersect(Rectangle rect) {
            this = Rectangle.Intersect(this, rect);
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the nearest integer values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted. </param>
        /// <returns>The rounded interger value of the <see cref="T:System.Drawing.Rectangle" />.</returns>
        // Token: 0x06000727 RID: 1831 RVA: 0x000108BC File Offset: 0x0000EABC
        public static Rectangle Round(RectangleF value) {
            checked {
                int num = (int)Math.Round((double)value.X);
                int num2 = (int)Math.Round((double)value.Y);
                int num3 = (int)Math.Round((double)value.Width);
                int num4 = (int)Math.Round((double)value.Height);
                return new Rectangle(num, num2, num3, num4);
            }
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by truncating the <see cref="T:System.Drawing.RectangleF" /> values.</summary>
        /// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted. </param>
        /// <returns>The truncated value of the  <see cref="T:System.Drawing.Rectangle" />.</returns>
        // Token: 0x06000728 RID: 1832 RVA: 0x0001090C File Offset: 0x0000EB0C
        public static Rectangle Truncate(RectangleF value) {
            checked {
                int num = (int)value.X;
                int num2 = (int)value.Y;
                int num3 = (int)value.Width;
                int num4 = (int)value.Height;
                return new Rectangle(num, num2, num3, num4);
            }
        }

        /// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> structure that contains the union of two <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
        /// <param name="a">A rectangle to union. </param>
        /// <param name="b">A rectangle to union. </param>
        /// <returns>A <see cref="T:System.Drawing.Rectangle" /> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle" /> structures.</returns>
        // Token: 0x06000729 RID: 1833 RVA: 0x00010944 File Offset: 0x0000EB44
        public static Rectangle Union(Rectangle a, Rectangle b) {
            return Rectangle.FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures have equal location and size.</summary>
        /// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the equality operator. </param>
        /// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the equality operator. </param>
        /// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.Drawing.Rectangle" /> structures have equal <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties.</returns>
        // Token: 0x0600072A RID: 1834 RVA: 0x000109A2 File Offset: 0x0000EBA2
        public static bool operator ==(Rectangle left, Rectangle right) {
            return left.Location == right.Location && left.Size == right.Size;
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures differ in location or size.</summary>
        /// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the inequality operator. </param>
        /// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the inequality operator. </param>
        /// <returns>This operator returns <see langword="true" /> if any of the <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" /> or <see cref="P:System.Drawing.Rectangle.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise <see langword="false" />.</returns>
        // Token: 0x0600072B RID: 1835 RVA: 0x000109CE File Offset: 0x0000EBCE
        public static bool operator !=(Rectangle left, Rectangle right) {
            return left.Location != right.Location || left.Size != right.Size;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
        /// <param name="location">A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the rectangular region. </param>
        /// <param name="size">A <see cref="T:System.Drawing.Size" /> that represents the width and height of the rectangular region. </param>
        // Token: 0x0600072C RID: 1836 RVA: 0x000109FA File Offset: 0x0000EBFA
        public Rectangle(Point location, Size size) {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="width">The width of the rectangle. </param>
        /// <param name="height">The height of the rectangle. </param>
        // Token: 0x0600072D RID: 1837 RVA: 0x00010A30 File Offset: 0x0000EC30
        public Rectangle(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
        // Token: 0x17000211 RID: 529
        // (get) Token: 0x0600072E RID: 1838 RVA: 0x00010A4F File Offset: 0x0000EC4F
        [Browsable(false)]
        public int Bottom
        {
            get
            {
                return this.y + this.height;
            }
        }

        /// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The height of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
        // Token: 0x17000212 RID: 530
        // (get) Token: 0x0600072F RID: 1839 RVA: 0x00010A5E File Offset: 0x0000EC5E
        // (set) Token: 0x06000730 RID: 1840 RVA: 0x00010A66 File Offset: 0x0000EC66
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

        /// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle" /> have values of zero.</summary>
        /// <returns>This property returns <see langword="true" /> if the <see cref="P:System.Drawing.Rectangle.Width" />, <see cref="P:System.Drawing.Rectangle.Height" />, <see cref="P:System.Drawing.Rectangle.X" />, and <see cref="P:System.Drawing.Rectangle.Y" /> properties of this <see cref="T:System.Drawing.Rectangle" /> all have values of zero; otherwise, <see langword="false" />.</returns>
        // Token: 0x17000213 RID: 531
        // (get) Token: 0x06000731 RID: 1841 RVA: 0x00010A6F File Offset: 0x0000EC6F
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.x == 0 && this.y == 0 && this.width == 0 && this.height == 0;
            }
        }

        /// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
        // Token: 0x17000214 RID: 532
        // (get) Token: 0x06000732 RID: 1842 RVA: 0x00010A94 File Offset: 0x0000EC94
        [Browsable(false)]
        public int Left
        {
            get
            {
                return this.X;
            }
        }

        /// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
        // Token: 0x17000215 RID: 533
        // (get) Token: 0x06000733 RID: 1843 RVA: 0x00010A9C File Offset: 0x0000EC9C
        // (set) Token: 0x06000734 RID: 1844 RVA: 0x00010AAF File Offset: 0x0000ECAF
        [Browsable(false)]
        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }
            set
            {
                this.x = value.X;
                this.y = value.Y;
            }
        }

        /// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
        // Token: 0x17000216 RID: 534
        // (get) Token: 0x06000735 RID: 1845 RVA: 0x00010ACB File Offset: 0x0000ECCB
        [Browsable(false)]
        public int Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        /// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle" />.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
        // Token: 0x17000217 RID: 535
        // (get) Token: 0x06000736 RID: 1846 RVA: 0x00010ADA File Offset: 0x0000ECDA
        // (set) Token: 0x06000737 RID: 1847 RVA: 0x00010AED File Offset: 0x0000ECED
        [Browsable(false)]
        public Size Size
        {
            get
            {
                return new Size(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        /// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
        // Token: 0x17000218 RID: 536
        // (get) Token: 0x06000738 RID: 1848 RVA: 0x00010B09 File Offset: 0x0000ED09
        [Browsable(false)]
        public int Top
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The width of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
        // Token: 0x17000219 RID: 537
        // (get) Token: 0x06000739 RID: 1849 RVA: 0x00010B11 File Offset: 0x0000ED11
        // (set) Token: 0x0600073A RID: 1850 RVA: 0x00010B19 File Offset: 0x0000ED19
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

        /// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
        // Token: 0x1700021A RID: 538
        // (get) Token: 0x0600073B RID: 1851 RVA: 0x00010B22 File Offset: 0x0000ED22
        // (set) Token: 0x0600073C RID: 1852 RVA: 0x00010B2A File Offset: 0x0000ED2A
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

        /// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
        // Token: 0x1700021B RID: 539
        // (get) Token: 0x0600073D RID: 1853 RVA: 0x00010B33 File Offset: 0x0000ED33
        // (set) Token: 0x0600073E RID: 1854 RVA: 0x00010B3B File Offset: 0x0000ED3B
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

        /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <returns>This method returns <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
        // Token: 0x0600073F RID: 1855 RVA: 0x00010B44 File Offset: 0x0000ED44
        public bool Contains(int x, int y) {
            return x >= this.Left && x < this.Right && y >= this.Top && y < this.Bottom;
        }

        /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if the point represented by <paramref name="pt" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
        // Token: 0x06000740 RID: 1856 RVA: 0x00010B6C File Offset: 0x0000ED6C
        public bool Contains(Point pt) {
            return this.Contains(pt.X, pt.Y);
        }

        /// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
        // Token: 0x06000741 RID: 1857 RVA: 0x00010B82 File Offset: 0x0000ED82
        public bool Contains(Rectangle rect) {
            return rect == Rectangle.Intersect(this, rect);
        }

        /// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure and its <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise, <see langword="false" />.</returns>
        // Token: 0x06000742 RID: 1858 RVA: 0x00010B96 File Offset: 0x0000ED96
        public override bool Equals(object obj) {
            return obj is Rectangle && this == (Rectangle)obj;
        }

        /// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle" /> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .</summary>
        /// <returns>An integer that represents the hash code for this rectangle.</returns>
        // Token: 0x06000743 RID: 1859 RVA: 0x00010BB3 File Offset: 0x0000EDB3
        public override int GetHashCode() {
            return this.height + this.width ^ this.x + this.y;
        }

        /// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
        /// <param name="rect">The rectangle to test. </param>
        /// <returns>This method returns <see langword="true" /> if there is any intersection, otherwise <see langword="false" />.</returns>
        // Token: 0x06000744 RID: 1860 RVA: 0x00010BD0 File Offset: 0x0000EDD0
        public bool IntersectsWith(Rectangle rect) {
            return this.Left < rect.Right && this.Right > rect.Left && this.Top < rect.Bottom && this.Bottom > rect.Top;
        }

        // Token: 0x06000745 RID: 1861 RVA: 0x00010C10 File Offset: 0x0000EE10
        private bool IntersectsWithInclusive(Rectangle r) {
            return this.Left <= r.Right && this.Right >= r.Left && this.Top <= r.Bottom && this.Bottom >= r.Top;
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="x">The horizontal offset. </param>
        /// <param name="y">The vertical offset. </param>
        // Token: 0x06000746 RID: 1862 RVA: 0x00010C5E File Offset: 0x0000EE5E
        public void Offset(int x, int y) {
            this.x += x;
            this.y += y;
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="pos">Amount to offset the location. </param>
        // Token: 0x06000747 RID: 1863 RVA: 0x00010C7C File Offset: 0x0000EE7C
        public void Offset(Point pos) {
            this.x += pos.X;
            this.y += pos.Y;
        }

        /// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle" /> to a human-readable string.</summary>
        /// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle" /> structure ¾ for example, {X=20, Y=20, Width=100, Height=50} </returns>
        // Token: 0x06000748 RID: 1864 RVA: 0x00010CA8 File Offset: 0x0000EEA8
        public override string ToString() {
            return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", new object[]
            {
                this.x,
                this.y,
                this.width,
                this.height
            });
        }

        // Token: 0x04000312 RID: 786
        private int x;

        // Token: 0x04000313 RID: 787
        private int y;

        // Token: 0x04000314 RID: 788
        private int width;

        // Token: 0x04000315 RID: 789
        private int height;

        /// <summary>Represents a <see cref="T:System.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
        // Token: 0x04000316 RID: 790
        public static readonly Rectangle Empty;

        public static implicit operator Godot.Rect2(Rectangle rectangle) {
            return new Godot.Rect2(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
        }
    }
}
