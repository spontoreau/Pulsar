using System;
using Pulsar.Helpers;

namespace Pulsar
{
	/// <summary>
	/// Color.
	/// </summary>
	[Serializable]
	public sealed class Color : ICloneable, IEquatable<Color>
	{
		/// <summary>
		/// Gets or sets alpha.
		/// </summary>
		/// <value>Alpha.</value>
		public byte A { get; set;}

		/// <summary>
		/// Gets or sets the red.
		/// </summary>
		/// <value>The red.</value>
		public byte R { get; set;}

		/// <summary>
		/// Gets or sets the green.
		/// </summary>
		/// <value>The green.</value>
		public byte G { get; set;}

		/// <summary>
		/// Gets or sets the blue.
		/// </summary>
		/// <value>The blue.</value>
		public byte B { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Color"/> class.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		public Color(byte r, byte g, byte b)
			: this(r, g, b, 255)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Color"/> class.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="a">The alpha component.</param>
		public Color(byte r, byte g, byte b, byte a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		/// <summary>
		/// Gets the black color.
		/// </summary>
		/// <value>The black color.</value>
		public static Color Black
		{
			get
			{
				return new Color(0, 0, 0);
			}
		}

		/// <summary>
		/// Gets the white color.
		/// </summary>
		/// <value>The white color.</value>
		public static Color White
		{
			get
			{
				return new Color(255, 255, 255);
			}
		}

		/// <summary>
		/// Gets the red color.
		/// </summary>
		/// <value>The red color.</value>
		public static Color Red
		{
			get
			{
				return new Color(255, 0, 0);
			}
		}

		/// <summary>
		/// Gets the green color.
		/// </summary>
		/// <value>The green color.</value>
		public static Color Green
		{
			get
			{
				return new Color(255, 0, 0);
			}
		}

		/// <summary>
		/// Gets the blue color.
		/// </summary>
		/// <value>The blue color.</value>
		public static Color Blue
		{
			get
			{
				return new Color(0, 0, 255);
			}
		}

		/// <summary>
		/// Gets the yellow color.
		/// </summary>
		/// <value>The yellow color.</value>
		public static Color Yellow
		{
			get
			{
				return new Color(255, 255, 0);
			}
		}

		/// <summary>
		/// Gets the magenta color.
		/// </summary>
		/// <value>The magenta color.</value>
		public static Color Magenta
		{
			get
			{
				return new Color(255, 0, 255);
			}
		}

		/// <summary>
		/// Gets the cyan color.
		/// </summary>
		/// <value>The cyan color.</value>
		public static Color Cyan
		{
			get
			{
				return new Color(0, 255, 255);
			}
		}

		/// <summary>
		/// Gets the transparent color.
		/// </summary>
		/// <value>The transparent color.</value>
		public static Color Transparent
		{
			get
			{
				return new Color(0, 0, 0, 0);
			}
		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public object Clone()
		{
			return new Color(R, G, B, A);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Pulsar.Color"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Pulsar.Color"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Pulsar.Color"/>;
		/// otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
		    var a = obj as Color;
		    return a != null && Equals(a);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Pulsar.Color"/> is equal to the current <see cref="Pulsar.Color"/>.
		/// </summary>
		/// <param name="other">The <see cref="Pulsar.Color"/> to compare with the current <see cref="Pulsar.Color"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Pulsar.Color"/> is equal to the current <see cref="Pulsar.Color"/>;
		/// otherwise, <c>false</c>.</returns>
		public bool Equals(Color other)
		{
			if (this == other)
				return true;
			
			return A == other.A && R == other.R && G == other.G && B == other.B;
		}

		/// <summary>
		/// Lerp the color on rgb.
		/// </summary>
		/// <returns>The rgb lerp color.</returns>
		/// <param name="value1">Value1.</param>
		/// <param name="value2">Value2.</param>
		/// <param name="amount">Amount.</param>
		public static Color LerpRgb(Color value1, Color value2, float amount)
		{
			return new Color(
				(byte)MathHelper.Lerp(value1.R, value2.R, amount),
				(byte)MathHelper.Lerp(value1.G, value2.G, amount),
				(byte)MathHelper.Lerp(value1.B, value2.B, amount));
		}

		/// <summary>
		/// Lerp the color on argb.
		/// </summary>
		/// <returns>The argb color.</returns>
		/// <param name="value1">Value1.</param>
		/// <param name="value2">Value2.</param>
		/// <param name="amount">Amount.</param>
		public static Color LerpArgb(Color value1, Color value2, float amount)
		{
			return new Color(
				(byte)MathHelper.Lerp(value1.R, value2.R, amount),
				(byte)MathHelper.Lerp(value1.G, value2.G, amount),
				(byte)MathHelper.Lerp(value1.B, value2.B, amount),
				(byte)MathHelper.Lerp(value1.A, value2.A, amount));
		}

		/// <param name="c1">C1.</param>
		/// <param name="c2">C2.</param>
		public static bool operator ==(Color c1, Color c2)
		{
			return c1 != null && c1.Equals(c2);
		}

		/// <param name="c1">C1.</param>
		/// <param name="c2">C2.</param>
		public static bool operator !=(Color c1, Color c2)
		{
			return c1 != null && !c1.Equals(c2);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Pulsar.Color"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			return A * R * G * B;
		}
	}
}

