/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2014, Sylvain PONTOREAU (pontoreau.sylvain@gmail.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

namespace Pulsar
{
	/// <summary>
	/// Rectangle.
	/// </summary>
	[Serializable()]
	public sealed class Rectangle : ICloneable, IEquatable<Rectangle>
	{
		/// <summary>
		/// Gets or sets x coordinate of the Rectangle.
		/// </summary>
		/// <value>The x.</value>
		public float X { get; set; }

		/// <summary>
		/// Gets or sets y coordinate of the Rectangle.
		/// </summary>
		/// <value>The y.</value>
		public float Y { get; set;}

		/// <summary>
		/// Gets or sets width of the Rectangle.
		/// </summary>
		/// <value>The width.</value>
		public float Width { get; set;}

		/// <summary>
		/// Gets or sets height of the Rectangle.
		/// </summary>
		/// <value>The height.</value>
		public float Height { get ; set; }

		/// <summary>
		/// get an empty Rectangle
		/// </summary>
		public static Rectangle Empty
		{
			get
			{
				return new Rectangle(0.0f, 0.0f, 0.0f, 0.0f);
			}
		}

		/// <summary>
		/// True if the rectangle is Empty
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return X == 0.0f && Y == 0.0f && Width == 0.0f && Height == 0.0f;
			}
		}

		/// <summary>
		/// Get the position of the Rectangle
		/// </summary>
		public Vector Position
		{
			get
			{
				return new Vector(X, Y);
			}
		}

		/// <summary>
		/// Get the centrer
		/// </summary>
		public Vector Center
		{
			get
			{
				return new Vector(Right / 2, Bottom / 2);
			}
		}

		/// <summary>
		/// Get X coordinate of the Rectangle Left
		/// </summary>
		public float Left
		{
			get
			{
				return X;
			}
		}

		/// <summary>
		/// Get X coordinate of the Rectangle Right
		/// </summary>
		public float Right
		{
			get
			{
				return X + Width;
			}
		}

		/// <summary>
		/// Get Y coordinate of the Rectangle Top
		/// </summary>
		public float Top
		{
			get
			{
				return Y;
			}
		}

		/// <summary>
		/// Get Y coordinate of the Rectangle Bottom
		/// </summary>
		public float Bottom
		{
			get
			{
				return Y + Height;
			}
		}

		/// <summary>
		/// Create a new instance of Rectangle.
		/// </summary>
		public Rectangle()
		{

		}

		/// <summary>
		/// Create a new instance of Rectangle.
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="width">Width size</param>
		/// <param name="height">Height size</param>
		public Rectangle(float x, float y, float width, float height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Create a new instance of Rectangle
		/// </summary>
		/// <param name="position">Position vector</param>
		/// <param name="size">Size vector</param>
		public Rectangle(Vector position, Vector size)
			: this(position.X, position.Y, size.X, size.Y)
		{

		}

		/// <summary>
		/// True if the Rectangle contains a vector.
		/// </summary>
		/// <param name="v">The vector.</param>
		/// <returns>True if the Rectangle contains a vector.</returns>
		public bool Contains(Vector v)
		{
			return (v.X >= X) && (v.X < Right) && (v.Y >= Y) && (v.Y < Bottom);
		}

		/// <summary>
		/// True if the Rectangle contains an other Rectangle.
		/// </summary>
		/// <param name="r">Other Rectangle.</param>
		/// <returns>True if the Rectangle contains an other Rectangle.</returns>
		public bool Contains(Rectangle r)
		{
			return (r.X >= X) && (r.Right < Right) && (r.Y >= Y) && (r.Bottom < Bottom);
		}

		/// <summary>
		/// True if Rectangle intersects an other Rectangle.
		/// </summary>
		/// <param name="r">Other Rectangle.</param>
		/// <returns>True if a Rectangle intersects this Rectangle.</returns>
		public bool Intersects(Rectangle r)
		{
			return (r.X < Right) && (r.Right > X) && (r.Y < Bottom) && (r.Bottom > Y);
		}

		/// <summary>
		/// Get the union representation of 2 rectangles.
		/// </summary>
		/// <param name="r1">Rectangle 1.</param>
		/// <param name="r2">Rectangle 2.</param>
		/// <returns>Rectangle representation of the union.</returns>
		public static Rectangle Union(Rectangle r1, Rectangle r2)
		{
			var x = (r1.X < r2.X) ? r1.X : r2.X;
			var y = (r1.Y < r2.Y) ? r1.Y : r2.Y;
			var width = ((r1.Right > r2.Right) ? r1.Right : r2.Right) - x;
			var height = ((r1.Bottom > r2.Bottom) ? r1.Bottom : r2.Bottom) - y;
			return new Rectangle(x, y, width, height);
		}

		/// <summary>
		/// Clone the Rectangle.
		/// </summary>
		/// <returns>Cloned Rectangle.</returns>
		public object Clone()
		{
			return new Rectangle(X, Y, Width, Height);
		}

		/// <summary>
		/// Returns a value that indicates whether the current instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">Object to make the comparison with.</param>
		/// <returns>true if the current instance is equal to the specified object; false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Rectangle)
				return Equals((Rectangle)obj);
			else
				return false;
		}

		/// <summary>
		/// True if the passing Rectangle is equal to this Rectangle.
		/// </summary>
		/// <param name="other">The other Rectangle.</param>
		/// <returns>True if the passing Rectangle is equal to this Rectangle.</returns>
		public bool Equals(Rectangle other)
		{
			if (this == other)
				return true;
			else
				return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
		}

		/// <summary>
		/// True if Rectangle 2 is equal to Rectangle 1.
		/// </summary>
		/// <param name="r1">Rectangle 1.</param>
		/// <param name="r2">Rectangle 2.</param>
		/// <returns>True if Rectangle 2 is equal to Rectangle 1.</returns>
		public static bool operator ==(Rectangle r1, Rectangle r2)
		{
			return r1.Equals(r2);
		}

		/// <summary>
		/// True if Rectangle 2 isn't equal to Rectangle 1.
		/// </summary>
		/// <param name="r1">Rectangle 1.</param>
		/// <param name="r2">Rectangle 2.</param>
		/// <returns>True if Rectangle 2 isn't equal to Rectangle 1.</returns>
		public static bool operator !=(Rectangle r1, Rectangle r2)
		{
			return !r1.Equals(r2);
		}

		/// <summary>
		/// Gets the hash code for this object. 
		/// </summary>
		/// <returns>Hash code for this object.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}

