using System;
using Pulsar.Helpers;

namespace Pulsar
{
	/// <summary>
	/// Vector.
	/// </summary>
	[Serializable]
	public sealed class Vector : ICloneable, IEquatable<Vector>
	{
		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>The x.</value>
		public float X { get; set; }

		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		/// <value>The y.</value>
		public float Y { get; set; }

		/// <summary>
		/// Gets the zero vector.
		/// </summary>
		/// <value>The zero vector.</value>
		public static Vector Zero
		{
			get
			{
				return new Vector(0.0f, 0.0f);
			}
		}

		/// <summary>
		/// Gets the one vector.
		/// </summary>
		/// <value>The one.</value>
		public static Vector One
		{
			get
			{
				return new Vector(1.0f, 1.0f);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Vector"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Vector(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Vector"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		public Vector(float value)
		{
			X = value;
			Y = value;
		}

		/// <param name="v">V.</param>
		public static Vector operator -(Vector v)
		{
			return new Vector(-v.X, -v.Y);
		}

		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static Vector operator -(Vector v1, Vector v2)
		{
			return new Vector(v1.X - v2.X, v1.Y - v2.Y);
		}

		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.X + v2.X, v1.Y + v2.Y);
		}

		/// <param name="v">V.</param>
		/// <param name="value">Value.</param>
		public static Vector operator *(Vector v, float value)
		{
			return new Vector(v.X * value, v.Y * value);
		}

		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static Vector operator *(Vector v1, Vector v2)
		{
			return new Vector(v1.X * v2.X, v1.Y * v2.Y);
		}

		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static Vector operator /(Vector v1, Vector v2)
		{
			return new Vector(v1.X / v2.X, v1.Y / v2.Y);
		}

		/// <param name="v">V.</param>
		/// <param name="value">Value.</param>
		public static Vector operator /(Vector v, float value)
		{
			return new Vector(v.X / value, v.Y / value);
		}

		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static bool operator ==(Vector v1, Vector v2)
		{
			return v1 != null && v1.Equals(v2);
		}

		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static bool operator !=(Vector v1, Vector v2)
		{
			return v1 != null && !v1.Equals(v2);
		}

		/// <summary>
		/// Clamp the specified vector.
		/// </summary>
		/// <param name="v1">Vector.</param>
		/// <param name="vMin">Vector minimum.</param>
		/// <param name="vMax">Vector max.</param>
		public static Vector Clamp(Vector v1, Vector vMin, Vector vMax)
		{
			return new Vector(MathHelper.Clamp(v1.X, vMin.X, vMax.X), MathHelper.Clamp(v1.Y, vMin.Y, vMax.Y));
		}

		/// <summary>
		/// Distance between specified v1 and v2.
		/// </summary>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		public static float Distance(Vector v1, Vector v2)
		{
			return MathHelper.Sqrt(MathHelper.Square(v1.X - v2.X) + MathHelper.Square(v1.Y - v2.Y));
		}

		/// <summary>
		/// Length of this instance.
		/// </summary>
		public float Length()
		{
			return MathHelper.Sqrt( MathHelper.Square(X) + MathHelper.Square(Y));
		}

		/// <summary>
		/// Lerp the specified v1, v2 with amount.
		/// </summary>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		/// <param name="amount">Amount.</param>
		public static Vector Lerp(Vector v1, Vector v2, float amount)
		{
			return new Vector(MathHelper.Lerp(v1.X, v2.X, amount), MathHelper.Lerp(v1.Y, v2.Y, amount));
		}

		/// <summary>
		/// Round this instance.
		/// </summary>
		public void Round()
		{
			X = MathHelper.Round(X);
			Y = MathHelper.Round(Y);
		}

		/// <summary>
		/// Normalize this instance.
		/// </summary>
		public void Normalize()
		{
			var value = 1.0f / MathHelper.Sqrt(MathHelper.Square(X) + MathHelper.Square(Y));
			X = X * value;
			Y = Y * value;
		}

		/// <summary>
		/// Normalize the specified v.
		/// </summary>
		/// <param name="v">V.</param>
		public static Vector Normalize(Vector v)
		{
			var value = 1.0f / MathHelper.Sqrt(MathHelper.Square(v.X) + MathHelper.Square(v.Y));
			return new Vector(v.X * value, v.Y * value);
		}

		/// <summary>
		/// Polar the specified lenght and angle.
		/// </summary>
		/// <param name="lenght">Lenght.</param>
		/// <param name="angle">Angle.</param>
		public static Vector Polar(float lenght, float angle)
		{
			return new Vector(lenght * MathHelper.Cos(angle), lenght * MathHelper.Sin(angle));
		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public object Clone()
		{
			return new Vector(X, Y);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Pulsar.Vector"/> is equal to the current <see cref="Pulsar.Vector"/>.
		/// </summary>
		/// <param name="other">The <see cref="Pulsar.Vector"/> to compare with the current <see cref="Pulsar.Vector"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Pulsar.Vector"/> is equal to the current <see cref="Pulsar.Vector"/>;
		/// otherwise, <c>false</c>.</returns>
		public bool Equals(Vector other)
		{
		    if (this == other)
				return true;

		    return X == other.X && Y == other.Y;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Pulsar.Vector"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Pulsar.Vector"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Pulsar.Vector"/>;
		/// otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
	    {
	        var v = obj as Vector;
	        return v != null & Equals(v);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Pulsar.Vector"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			return Convert.ToInt32(X * Y);
		}
	}
}
