using System;

namespace Pulsar
{
	/// <summary>
	/// Rectangle.
	/// </summary>
	[Serializable]
	public sealed class Rectangle : ICloneable, IEquatable<Rectangle>
	{
		/// <summary>
		/// Gets or sets the x position.
		/// </summary>
		/// <value>The x.</value>
		public float X { get; set; }

		/// <summary>
		/// Gets or sets the y position.
		/// </summary>
		/// <value>The y.</value>
		public float Y { get; set;}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public float Width { get; set;}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public float Height { get ; set; }

		/// <summary>
		/// Gets a empty rectangle.
		/// </summary>
		/// <value>The empty rectangle.</value>
		public static Rectangle Empty
		{
			get
			{
				return new Rectangle(0.0f, 0.0f, 0.0f, 0.0f);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty
		{
			get
			{
				return X == 0.0f && Y == 0.0f && Width == 0.0f && Height == 0.0f;
			}
		}

		/// <summary>
		/// Gets the rectangle position.
		/// </summary>
		/// <value>The position.</value>
		public Vector Position
		{
			get
			{
				return new Vector(X, Y);
			}
		}

		/// <summary>
		/// Gets the rectangle center.
		/// </summary>
		/// <value>The center.</value>
		public Vector Center
		{
			get
			{
				return new Vector(Right / 2, Bottom / 2);
			}
		}

		/// <summary>
		/// Gets the left.
		/// </summary>
		/// <value>The left.</value>
		public float Left
		{
			get
			{
				return X;
			}
		}

		/// <summary>
		/// Gets the right.
		/// </summary>
		/// <value>The right.</value>
		public float Right
		{
			get
			{
				return X + Width;
			}
		}

		/// <summary>
		/// Gets the top.
		/// </summary>
		/// <value>The top.</value>
		public float Top
		{
			get
			{
				return Y;
			}
		}

		/// <summary>
		/// Gets the bottom.
		/// </summary>
		/// <value>The bottom.</value>
		public float Bottom
		{
			get
			{
				return Y + Height;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Rectangle"/> class.
		/// </summary>
		public Rectangle()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Rectangle"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public Rectangle(float x, float y, float width, float height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Rectangle"/> class.
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="size">Size.</param>
		public Rectangle(Vector position, Vector size)
			: this(position.X, position.Y, size.X, size.Y)
		{

		}

		/// <summary>
		/// Contains the specified vector.
		/// </summary>
		/// <param name="v">Vector.</param>
		public bool Contains(Vector v)
		{
			return (v.X >= X) && (v.X < Right) && (v.Y >= Y) && (v.Y < Bottom);
		}

		/// <summary>
		/// Contains the specified rectangle.
		/// </summary>
		/// <param name="r">The rectangle.</param>
		public bool Contains(Rectangle r)
		{
			return (r.X >= X) && (r.Right < Right) && (r.Y >= Y) && (r.Bottom < Bottom);
		}

		/// <summary>
		/// Intersects the specified rectangle.
		/// </summary>
		/// <param name="r">The rectangle component.</param>
		public bool Intersects(Rectangle r)
		{
			return (r.X < Right) && (r.Right > X) && (r.Y < Bottom) && (r.Bottom > Y);
		}

		/// <summary>
		/// Union the specified r1 and r2.
		/// </summary>
		/// <param name="r1">R1.</param>
		/// <param name="r2">R2.</param>
		public static Rectangle Union(Rectangle r1, Rectangle r2)
		{
			var x = (r1.X < r2.X) ? r1.X : r2.X;
			var y = (r1.Y < r2.Y) ? r1.Y : r2.Y;
			var width = ((r1.Right > r2.Right) ? r1.Right : r2.Right) - x;
			var height = ((r1.Bottom > r2.Bottom) ? r1.Bottom : r2.Bottom) - y;
			return new Rectangle(x, y, width, height);
		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public object Clone()
		{
			return new Rectangle(X, Y, Width, Height);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Pulsar.Rectangle"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Pulsar.Rectangle"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Pulsar.Rectangle"/>;
		/// otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
		    var r = obj as Rectangle;
		    return r != null && Equals(r);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Pulsar.Rectangle"/> is equal to the current <see cref="Pulsar.Rectangle"/>.
		/// </summary>
		/// <param name="other">The <see cref="Pulsar.Rectangle"/> to compare with the current <see cref="Pulsar.Rectangle"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Pulsar.Rectangle"/> is equal to the current
		/// <see cref="Pulsar.Rectangle"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(Rectangle other)
	    {
	        if (this == other)
				return true;

	        return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
	    }

		/// <param name="r1">R1.</param>
		/// <param name="r2">R2.</param>
		public static bool operator ==(Rectangle r1, Rectangle r2)
		{
			return r1 != null && r1.Equals(r2);
		}

		/// <param name="r1">R1.</param>
		/// <param name="r2">R2.</param>
		public static bool operator !=(Rectangle r1, Rectangle r2)
		{
			return r1 != null && !r1.Equals(r2);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Pulsar.Rectangle"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			return Convert.ToInt32(X * Y * Width * Height);
		}
	}
}

