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
	/// Four value color
	/// </summary>
	[Serializable()]
	public sealed class Color : ICloneable, IEquatable<Color>
	{
		/// <summary>
		/// Alpha value
		/// </summary>
		public byte A { get; set;}

		/// <summary>
		/// Red value
		/// </summary>
		public byte R { get; set;}

		/// <summary>
		/// Green value
		/// </summary>
		public byte G { get; set;}

		/// <summary>
		/// Blue value
		/// </summary>
		public byte B { get; set;}

		/// <summary>
		/// Create a new instance of Color
		/// </summary>
		/// <param name="r">Red value</param>
		/// <param name="g">Green value</param>
		/// <param name="b">Blue value</param>
		public Color(byte r, byte g, byte b)
			: this(r, g, b, 255)
		{

		}

		/// <summary>
		/// Create a new instance of Color
		/// </summary>
		/// <param name="r">Red value</param>
		/// <param name="g">Green value</param>
		/// <param name="b">Blue value</param>
		/// <param name="a">Alpha value</param>
		public Color(byte r, byte g, byte b, byte a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		/// <summary>
		/// Black color
		/// </summary>
		public static Color Black
		{
			get
			{
				return new Color(0, 0, 0);
			}
		}

		/// <summary>
		/// White color
		/// </summary>
		public static Color White
		{
			get
			{
				return new Color(255, 255, 255);
			}
		}

		/// <summary>
		/// Red color
		/// </summary>
		public static Color Red
		{
			get
			{
				return new Color(255, 0, 0);
			}
		}

		/// <summary>
		/// Green color
		/// </summary>
		public static Color Green
		{
			get
			{
				return new Color(255, 0, 0);
			}
		}

		/// <summary>
		/// Blue color
		/// </summary>
		public static Color Blue
		{
			get
			{
				return new Color(0, 0, 255);
			}
		}

		/// <summary>
		/// Yellow color
		/// </summary>
		public static Color Yellow
		{
			get
			{
				return new Color(255, 255, 0);
			}
		}

		/// <summary>
		/// Magenta color
		/// </summary>
		public static Color Magenta
		{
			get
			{
				return new Color(255, 0, 255);
			}
		}

		/// <summary>
		/// Cyan color
		/// </summary>
		public static Color Cyan
		{
			get
			{
				return new Color(0, 255, 255);
			}
		}

		/// <summary>
		/// Transparent color
		/// </summary>
		public static Color Transparent
		{
			get
			{
				return new Color(0, 0, 0, 0);
			}
		}

		/// <summary>
		/// Clone the color
		/// </summary>
		/// <returns>Cloned color</returns>
		public object Clone()
		{
			return new Color(R, G, B, A);
		}


		/// <summary>
		/// Returns a value that indicates whether the current instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">Object to make the comparison with.</param>
		/// <returns>true if the current instance is equal to the specified object; false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Color)
				return Equals((Color)obj);
			else
				return false;
		}

		/// <summary>
		/// True if the passing color is equal to this color.
		/// </summary>
		/// <param name="other">The other color.</param>
		/// <returns>True if the passing color is equal to this color.</returns>
		public bool Equals(Color other)
		{
			if (this == other)
				return true;
			else
				return A == other.A && R == other.R && G == other.G && B == other.B;
		}

		/// <summary>
		/// Linear interpolation between two color on the RGB channel
		/// </summary>
		/// <param name="value1">Color 1</param>
		/// <param name="value2">Color 2</param>
		/// <param name="amount">Amout to interpolate between the two colors</param>
		/// <returns>Color corresponding to the lerp operation</returns>
		public static Color LerpRGB(Color value1, Color value2, float amount)
		{
			return new Color(
				(byte)MathHelper.Lerp(value1.R, value2.R, amount),
				(byte)MathHelper.Lerp(value1.G, value2.G, amount),
				(byte)MathHelper.Lerp(value1.B, value2.B, amount));
		}

		/// <summary>
		/// Linear interpolation between two color on the ARGB channel
		/// </summary>
		/// <param name="value1">Color 1</param>
		/// <param name="value2">Color 2</param>
		/// <param name="amount">Amout to interpolate between the two colors</param>
		/// <returns>Color corresponding to the lerp operation</returns>
		public static Color LerpARGB(Color value1, Color value2, float amount)
		{
			return new Color(
				(byte)MathHelper.Lerp(value1.R, value2.R, amount),
				(byte)MathHelper.Lerp(value1.G, value2.G, amount),
				(byte)MathHelper.Lerp(value1.B, value2.B, amount),
				(byte)MathHelper.Lerp(value1.A, value2.A, amount));
		}

		/// <summary>
		/// True if Color 2 is equal to Color 1
		/// </summary>
		/// <param name="c1">Color 1</param>
		/// <param name="c2">Color 2</param>
		/// <returns>True if Color 2 is equal to Vector 1</returns>
		public static bool operator ==(Color c1, Color c2)
		{
			return c1.Equals(c2);
		}

		/// <summary>
		/// True if Color 2 isn't equal to Color 1
		/// </summary>
		/// <param name="c1">Color 1</param>
		/// <param name="c2">Color 2</param>
		/// <returns>True if Color 2 isn't equal to Color 1</returns>
		public static bool operator !=(Color c1, Color c2)
		{
			return !c1.Equals(c2);
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

