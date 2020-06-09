using System;
using System.ComponentModel;

namespace System.Drawing
{
    /// <summary>Stores a set of four floating-point numbers that represent the location and size of a rectangle. For more advanced region functions, use a <see cref="T:System.Drawing.Region" /> object.</summary>
    // Token: 0x0200004A RID: 74
    [Serializable]
    public struct RectangleF
    {
        /// <summary>Creates a <see cref="T:System.Drawing.RectangleF" /> structure with upper-left corner and lower-right corner at the specified locations.</summary>
        /// <param name="left">The x-coordinate of the upper-left corner of the rectangular region. </param>
        /// <param name="top">The y-coordinate of the upper-left corner of the rectangular region. </param>
        /// <param name="right">The x-coordinate of the lower-right corner of the rectangular region. </param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of the rectangular region. </param>
        /// <returns>The new <see cref="T:System.Drawing.RectangleF" /> that this method creates.</returns>
        // Token: 0x06000749 RID: 1865 RVA: 0x00010CFD File Offset: 0x0000EEFD
        public static RectangleF FromLTRB(float left, float top, float right, float bottom) {
            return new RectangleF(left, top, right - left, bottom - top);
        }

        /// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.RectangleF" /> structure. The copy is enlarged by the specified amount and the original rectangle remains unmodified.</summary>
        /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to be copied. This rectangle is not modified. </param>
        /// <param name="x">The amount to enlarge the copy of the rectangle horizontally. </param>
        /// <param name="y">The amount to enlarge the copy of the rectangle vertically. </param>
        /// <returns>The enlarged <see cref="T:System.Drawing.RectangleF" />.</returns>
        // Token: 0x0600074A RID: 1866 RVA: 0x00010D0C File Offset: 0x0000EF0C
        public static RectangleF Inflate(RectangleF rect, float x, float y) {
            RectangleF result = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            result.Inflate(x, y);
            return result;
        }

        /// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> structure by the specified amount.</summary>
        /// <param name="x">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure horizontally. </param>
        /// <param name="y">The amount to inflate this <see cref="T:System.Drawing.RectangleF" /> structure vertically. </param>
        // Token: 0x0600074B RID: 1867 RVA: 0x00010D46 File Offset: 0x0000EF46
        public void Inflate(float x, float y) {
            this.Inflate(new SizeF(x, y));
        }

        /// <summary>Enlarges this <see cref="T:System.Drawing.RectangleF" /> by the specified amount.</summary>
        /// <param name="size">The amount to inflate this rectangle. </param>
        // Token: 0x0600074C RID: 1868 RVA: 0x00010D58 File Offset: 0x0000EF58
        public void Inflate(SizeF size) {
            this.x -= size.Width;
            this.y -= size.Height;
            this.width += size.Width * 2f;
            this.height += size.Height * 2f;
        }

        /// <summary>Returns a <see cref="T:System.Drawing.RectangleF" /> structure that represents the intersection of two rectangles. If there is no intersection, and empty <see cref="T:System.Drawing.RectangleF" /> is returned.</summary>
        /// <param name="a">A rectangle to intersect. </param>
        /// <param name="b">A rectangle to intersect. </param>
        /// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure the size of which represents the overlapped area of the two specified rectangles.</returns>
        // Token: 0x0600074D RID: 1869 RVA: 0x00010DC4 File Offset: 0x0000EFC4
        public static RectangleF Intersect(RectangleF a, RectangleF b) {
            if (!a.IntersectsWithInclusive(b)) {
                return RectangleF.Empty;
            }
            return RectangleF.FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
        }

        /// <summary>Replaces this <see cref="T:System.Drawing.RectangleF" /> structure with the intersection of itself and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <param name="rect">The rectangle to intersect. </param>
        // Token: 0x0600074E RID: 1870 RVA: 0x00010E32 File Offset: 0x0000F032
        public void Intersect(RectangleF rect) {
            this = RectangleF.Intersect(this, rect);
        }

        /// <summary>Creates the smallest possible third rectangle that can contain both of two rectangles that form a union.</summary>
        /// <param name="a">A rectangle to union. </param>
        /// <param name="b">A rectangle to union. </param>
        /// <returns>A third <see cref="T:System.Drawing.RectangleF" /> structure that contains both of the two rectangles that form the union.</returns>
        // Token: 0x0600074F RID: 1871 RVA: 0x00010E48 File Offset: 0x0000F048
        public static RectangleF Union(RectangleF a, RectangleF b) {
            return RectangleF.FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures have equal location and size.</summary>
        /// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the equality operator. </param>
        /// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the equality operator. </param>
        /// <returns>This operator returns <see langword="true" /> if the two specified <see cref="T:System.Drawing.RectangleF" /> structures have equal <see cref="P:System.Drawing.RectangleF.X" />, <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, and <see cref="P:System.Drawing.RectangleF.Height" /> properties.</returns>
        // Token: 0x06000750 RID: 1872 RVA: 0x00010EA8 File Offset: 0x0000F0A8
        public static bool operator ==(RectangleF left, RectangleF right) {
            return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
        }

        /// <summary>Tests whether two <see cref="T:System.Drawing.RectangleF" /> structures differ in location or size.</summary>
        /// <param name="left">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the left of the inequality operator. </param>
        /// <param name="right">The <see cref="T:System.Drawing.RectangleF" /> structure that is to the right of the inequality operator. </param>
        /// <returns>This operator returns <see langword="true" /> if any of the <see cref="P:System.Drawing.RectangleF.X" /> , <see cref="P:System.Drawing.RectangleF.Y" />, <see cref="P:System.Drawing.RectangleF.Width" />, or <see cref="P:System.Drawing.RectangleF.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise <see langword="false" />.</returns>
        // Token: 0x06000751 RID: 1873 RVA: 0x00010EF8 File Offset: 0x0000F0F8
        public static bool operator !=(RectangleF left, RectangleF right) {
            return left.X != right.X || left.Y != right.Y || left.Width != right.Width || left.Height != right.Height;
        }

        /// <summary>Converts the specified <see cref="T:System.Drawing.Rectangle" /> structure to a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> structure to convert. </param>
        /// <returns>The <see cref="T:System.Drawing.RectangleF" /> structure that is converted from the specified <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
        // Token: 0x06000752 RID: 1874 RVA: 0x00010F4A File Offset: 0x0000F14A
        public static implicit operator RectangleF(Rectangle r) {
            return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
        /// <param name="location">A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of the rectangular region. </param>
        /// <param name="size">A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of the rectangular region. </param>
        // Token: 0x06000753 RID: 1875 RVA: 0x00010F71 File Offset: 0x0000F171
        public RectangleF(PointF location, SizeF size) {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.RectangleF" /> class with the specified location and size.</summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="width">The width of the rectangle. </param>
        /// <param name="height">The height of the rectangle. </param>
        // Token: 0x06000754 RID: 1876 RVA: 0x00010FA7 File Offset: 0x0000F1A7
        public RectangleF(float x, float y, float width, float height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>Gets the y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.Y" /> and <see cref="P:System.Drawing.RectangleF.Height" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
        // Token: 0x1700021C RID: 540
        // (get) Token: 0x06000755 RID: 1877 RVA: 0x00010FC6 File Offset: 0x0000F1C6
        [Browsable(false)]
        public float Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }

        /// <summary>Gets or sets the height of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The height of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
        // Token: 0x1700021D RID: 541
        // (get) Token: 0x06000756 RID: 1878 RVA: 0x00010FD5 File Offset: 0x0000F1D5
        // (set) Token: 0x06000757 RID: 1879 RVA: 0x00010FDD File Offset: 0x0000F1DD
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

        /// <summary>Tests whether the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero.</summary>
        /// <returns>This property returns <see langword="true" /> if the <see cref="P:System.Drawing.RectangleF.Width" /> or <see cref="P:System.Drawing.RectangleF.Height" /> property of this <see cref="T:System.Drawing.RectangleF" /> has a value of zero; otherwise, <see langword="false" />.</returns>
        // Token: 0x1700021E RID: 542
        // (get) Token: 0x06000758 RID: 1880 RVA: 0x00010FE6 File Offset: 0x0000F1E6
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.width <= 0f || this.height <= 0f;
            }
        }

        /// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.RectangleF" /> structure. </returns>
        // Token: 0x1700021F RID: 543
        // (get) Token: 0x06000759 RID: 1881 RVA: 0x00011007 File Offset: 0x0000F207
        [Browsable(false)]
        public float Left
        {
            get
            {
                return this.X;
            }
        }

        /// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>A <see cref="T:System.Drawing.PointF" /> that represents the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
        // Token: 0x17000220 RID: 544
        // (get) Token: 0x0600075A RID: 1882 RVA: 0x0001100F File Offset: 0x0000F20F
        // (set) Token: 0x0600075B RID: 1883 RVA: 0x00011022 File Offset: 0x0000F222
        [Browsable(false)]
        public PointF Location
        {
            get
            {
                return new PointF(this.x, this.y);
            }
            set
            {
                this.x = value.X;
                this.y = value.Y;
            }
        }

        /// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.RectangleF.X" /> and <see cref="P:System.Drawing.RectangleF.Width" /> of this <see cref="T:System.Drawing.RectangleF" /> structure. </returns>
        // Token: 0x17000221 RID: 545
        // (get) Token: 0x0600075C RID: 1884 RVA: 0x0001103E File Offset: 0x0000F23E
        [Browsable(false)]
        public float Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        /// <summary>Gets or sets the size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
        /// <returns>A <see cref="T:System.Drawing.SizeF" /> that represents the width and height of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
        // Token: 0x17000222 RID: 546
        // (get) Token: 0x0600075D RID: 1885 RVA: 0x0001104D File Offset: 0x0000F24D
        // (set) Token: 0x0600075E RID: 1886 RVA: 0x00011060 File Offset: 0x0000F260
        [Browsable(false)]
        public SizeF Size
        {
            get
            {
                return new SizeF(this.width, this.height);
            }
            set
            {
                this.width = value.Width;
                this.height = value.Height;
            }
        }

        /// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.RectangleF" /> structure.</returns>
        // Token: 0x17000223 RID: 547
        // (get) Token: 0x0600075F RID: 1887 RVA: 0x0001107C File Offset: 0x0000F27C
        [Browsable(false)]
        public float Top
        {
            get
            {
                return this.Y;
            }
        }

        /// <summary>Gets or sets the width of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The width of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
        // Token: 0x17000224 RID: 548
        // (get) Token: 0x06000760 RID: 1888 RVA: 0x00011084 File Offset: 0x0000F284
        // (set) Token: 0x06000761 RID: 1889 RVA: 0x0001108C File Offset: 0x0000F28C
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

        /// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
        // Token: 0x17000225 RID: 549
        // (get) Token: 0x06000762 RID: 1890 RVA: 0x00011095 File Offset: 0x0000F295
        // (set) Token: 0x06000763 RID: 1891 RVA: 0x0001109D File Offset: 0x0000F29D
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

        /// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.RectangleF" /> structure. The default is 0.</returns>
        // Token: 0x17000226 RID: 550
        // (get) Token: 0x06000764 RID: 1892 RVA: 0x000110A6 File Offset: 0x0000F2A6
        // (set) Token: 0x06000765 RID: 1893 RVA: 0x000110AE File Offset: 0x0000F2AE
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

        /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <returns>This method returns <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise <see langword="false" />.</returns>
        // Token: 0x06000766 RID: 1894 RVA: 0x000110B7 File Offset: 0x0000F2B7
        public bool Contains(float x, float y) {
            return x >= this.Left && x < this.Right && y >= this.Top && y < this.Bottom;
        }

        /// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <param name="pt">The <see cref="T:System.Drawing.PointF" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if the point represented by the <paramref name="pt" /> parameter is contained within this <see cref="T:System.Drawing.RectangleF" /> structure; otherwise <see langword="false" />.</returns>
        // Token: 0x06000767 RID: 1895 RVA: 0x000110DF File Offset: 0x0000F2DF
        public bool Contains(PointF pt) {
            return this.Contains(pt.X, pt.Y);
        }

        /// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
        /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within the rectangular region represented by this <see cref="T:System.Drawing.RectangleF" />; otherwise <see langword="false" />.</returns>
        // Token: 0x06000768 RID: 1896 RVA: 0x000110F8 File Offset: 0x0000F2F8
        public bool Contains(RectangleF rect) {
            return this.X <= rect.X && this.Right >= rect.Right && this.Y <= rect.Y && this.Bottom >= rect.Bottom;
        }

        /// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> with the same location and size of this <see cref="T:System.Drawing.RectangleF" />.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.RectangleF" /> and its <see langword="X" />, <see langword="Y" />, <see langword="Width" />, and <see langword="Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.RectangleF" />; otherwise, <see langword="false" />.</returns>
        // Token: 0x06000769 RID: 1897 RVA: 0x00011146 File Offset: 0x0000F346
        public override bool Equals(object obj) {
            return obj is RectangleF && this == (RectangleF)obj;
        }

        /// <summary>Gets the hash code for this <see cref="T:System.Drawing.RectangleF" /> structure. For information about the use of hash codes, see <see langword="Object.GetHashCode" />.</summary>
        /// <returns>The hash code for this <see cref="T:System.Drawing.RectangleF" />.</returns>
        // Token: 0x0600076A RID: 1898 RVA: 0x00011163 File Offset: 0x0000F363
        public override int GetHashCode() {
            return (int)(this.x + this.y + this.width + this.height);
        }

        /// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
        /// <param name="rect">The rectangle to test. </param>
        /// <returns>This method returns <see langword="true" /> if there is any intersection.</returns>
        // Token: 0x0600076B RID: 1899 RVA: 0x00011181 File Offset: 0x0000F381
        public bool IntersectsWith(RectangleF rect) {
            return this.Left < rect.Right && this.Right > rect.Left && this.Top < rect.Bottom && this.Bottom > rect.Top;
        }

        // Token: 0x0600076C RID: 1900 RVA: 0x000111C4 File Offset: 0x0000F3C4
        private bool IntersectsWithInclusive(RectangleF r) {
            return this.Left <= r.Right && this.Right >= r.Left && this.Top <= r.Bottom && this.Bottom >= r.Top;
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="x">The amount to offset the location horizontally. </param>
        /// <param name="y">The amount to offset the location vertically. </param>
        // Token: 0x0600076D RID: 1901 RVA: 0x00011212 File Offset: 0x0000F412
        public void Offset(float x, float y) {
            this.X += x;
            this.Y += y;
        }

        /// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
        /// <param name="pos">The amount to offset the location. </param>
        // Token: 0x0600076E RID: 1902 RVA: 0x00011230 File Offset: 0x0000F430
        public void Offset(PointF pos) {
            this.Offset(pos.X, pos.Y);
        }

        /// <summary>Converts the <see langword="Location" /> and <see cref="T:System.Drawing.Size" /> of this <see cref="T:System.Drawing.RectangleF" /> to a human-readable string.</summary>
        /// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.RectangleF" /> structure. For example, "{X=20, Y=20, Width=100, Height=50}".</returns>
        // Token: 0x0600076F RID: 1903 RVA: 0x00011248 File Offset: 0x0000F448
        public override string ToString() {
            return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", new object[]
            {
                this.x,
                this.y,
                this.width,
                this.height
            });
        }

        // Token: 0x04000317 RID: 791
        private float x;

        // Token: 0x04000318 RID: 792
        private float y;

        // Token: 0x04000319 RID: 793
        private float width;

        // Token: 0x0400031A RID: 794
        private float height;

        /// <summary>Represents an instance of the <see cref="T:System.Drawing.RectangleF" /> class with its members uninitialized.</summary>
        // Token: 0x0400031B RID: 795
        public static readonly RectangleF Empty;
    }
}
