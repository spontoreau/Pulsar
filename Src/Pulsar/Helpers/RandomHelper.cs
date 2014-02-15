using System;

namespace Pulsar.Helpers
{
	/// <summary>
	/// Random helper.
	/// </summary>
	public static class RandomHelper
	{
		/// <summary>
		/// The random instance.
		/// </summary>
		private static readonly Random _random = new Random();

		/// <summary>
		/// Nexts int.
		/// </summary>
		/// <returns>The int.</returns>
		public static int NextInt()
		{
			return _random.Next();
		}

		/// <summary>
		/// Nexts int.
		/// </summary>
		/// <returns>The int.</returns>
		/// <param name="maxValue">Max value.</param>
		public static int NextInt(int maxValue)
		{
			return _random.Next(maxValue);
		}

		/// <summary>
		/// Nexts int.
		/// </summary>
		/// <returns>The int.</returns>
		/// <param name="minValue">Minimum value.</param>
		/// <param name="maxValue">Max value.</param>
		public static int NextInt(int minValue, int maxValue)
		{
			return _random.Next(minValue, maxValue);
		}

		/// <summary>
		/// Nexts bool.
		/// </summary>
		/// <returns><c>true</c>, if bool was nexted, <c>false</c> otherwise.</returns>
		public static bool NextBool()
		{
			return NextInt(2) == 1;
		}

		/// <summary>
		/// Nexts float.
		/// </summary>
		/// <returns>The float.</returns>
		public static float NextFloat()
		{
			return (float)_random.NextDouble();
		}

		/// <summary>
		/// Nexts float.
		/// </summary>
		/// <returns>The float.</returns>
		/// <param name="maxValue">Max value.</param>
		public static float NextFloat(float maxValue)
		{
			return maxValue * NextFloat();
		}

		/// <summary>
		/// Nexts float.
		/// </summary>
		/// <returns>The float.</returns>
		/// <param name="minValue">Minimum value.</param>
		/// <param name="maxValue">Max value.</param>
		public static float NextFloat(float minValue, float maxValue)
		{
			return (maxValue - minValue) * NextFloat() + minValue;
		}

		/// <summary>
		/// Nexts radians angle.
		/// </summary>
		/// <returns>The radians angle.</returns>
		public static float NextRadiansAngle()
		{
			return NextFloat(-MathHelper.Pi, MathHelper.Pi);
		}

		/// <summary>
		/// Nexts vector.
		/// </summary>
		/// <returns>The vector.</returns>
		public static Vector NextVector()
		{
			return Vector.Polar(1.0f, NextRadiansAngle());
		}

		/// <summary>
		/// Choose a specified T in values.
		/// </summary>
		/// <param name="values">Values.</param>
		public static T Choose<T>(params T[] values)
		{
			var index = NextInt(values.Length);

			return values[index];
		}
	}		
}