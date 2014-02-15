using System;

namespace Pulsar.Helpers
{
	/// <summary>
	/// Math helper.
	/// </summary>
	public static class MathHelper
	{
		/// <summary>
		/// The pi.
		/// </summary>
		public const float Pi = (float)Math.PI;

		/// <summary>
		/// The two pi.
		/// </summary>
		public const float TwoPi = Pi * 2;

		/// <summary>
		/// The e.
		/// </summary>
		public const float E = (float)(Math.E);

		/// <summary>
		/// Absolute the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Abs(float value)
		{
			return Math.Abs(value);
		}

		/// <summary>
		/// Square root the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Sqrt(float value)
		{
			return (float)Math.Sqrt(value);
		}

		/// <summary>
		/// Square the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Square(float value)
		{
			return value * value;
		}

		/// <summary>
		/// Pow the specified x by y.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow(x, y);
		}

		/// <summary>
		/// Acos the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Acos(float value)
		{
			return (float)Math.Acos(value);
		}

		/// <summary>
		/// Asin the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Asin(float value)
		{
			return (float)Math.Asin(value);
		}

		/// <summary>
		/// Atan the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Atan(float value)
		{
			return (float)Math.Atan(value);
		}

		/// <summary>
		/// Atan2 the specified y and x.
		/// </summary>
		/// <param name="y">The y coordinate.</param>
		/// <param name="x">The x coordinate.</param>
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2(y, x);
		}

		/// <summary>
		/// Sin the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Sin(float value)
		{
			return (float)Math.Sin(value);
		}

		/// <summary>
		/// Tan the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Tan(float value)
		{
			return (float)Math.Tan(value);
		}

		/// <summary>
		/// Cos the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Cos(float value)
		{
			return (float)Math.Cos(value);
		}

		/// <summary>
		/// E pow the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Exp(float value)
		{
			return (float)Math.Exp(value);
		}

		/// <summary>
		/// Log E the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Log(float value)
		{
			return (float)Math.Log(value);
		}

		/// <summary>
		/// Log 10 the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public static float Log10(float value)
		{
			return (float)Math.Log10(value);
		}

		/// <summary>
		/// Linear interpolate the specified value1 and value2 by amount.
		/// </summary>
		/// <param name="value1">Value1.</param>
		/// <param name="value2">Value2.</param>
		/// <param name="amount">Amount.</param>
		public static float Lerp(float value1, float value2, float amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		/// <summary>
		/// Max value between value1 and value2.
		/// </summary>
		/// <param name="value1">Value1.</param>
		/// <param name="value2">Value2.</param>
		public static float Max(float value1, float value2)
		{
			return (value1 > value2) ? value1 : value2;
		}

		/// <summary>
		/// Minimum value between value1 and value2.
		/// </summary>
		/// <param name="value1">Value1.</param>
		/// <param name="value2">Value2.</param>
		public static float Min(float value1, float value2)
		{
			return (value1 < value2) ? value1 : value2;
		}

		/// <summary>
		/// Clamp the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static float Clamp(float value, float min, float max)
		{
			if (value > max)
				return max;
			else if (value < min)
				return min;
			else
				return value;
		}

		/// <summary>
		/// Round the specified f.
		/// </summary>
		/// <param name="f">F.</param>
		public static float Round(float f)
		{
			return (float)Math.Round(f);
		}

		/// <summary>
		/// Round the specified f and i.
		/// </summary>
		/// <param name="f">F.</param>
		/// <param name="i">decimal next coma to round.</param>
		public static float Round(float f, int i)
		{
			return (float)Math.Round(f, i);
		}

		/// <summary>
		/// Distance between point 1 and 2.
		/// </summary>
		/// <param name="x1">The first x value.</param>
		/// <param name="y1">The first y value.</param>
		/// <param name="x2">The second x value.</param>
		/// <param name="y2">The second y value.</param>
		public static float Distance(float x1, float y1, float x2, float y2)
		{
			return Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
		}

		/// <summary>
		/// Radians angle between point 1 and 2.
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="x1">The first x value.</param>
		/// <param name="y1">The first y value.</param>
		/// <param name="x2">The second x value.</param>
		/// <param name="y2">The second y value.</param>
		public static float RadiansAngle(float x1, float y1, float x2, float y2)
		{
			return Atan2(x1 - x2, y2 - y1);
		}

		/// <summary>
		/// Convert radians to the degrees.
		/// </summary>
		/// <returns>The degrees.</returns>
		/// <param name="radians">Radians.</param>
		public static float ToDegrees(float radians)
		{
			return radians * (180.0f / Pi);
		}

		/// <summary>
		/// Convert radians to degrees.
		/// </summary>
		/// <returns>The radians.</returns>
		/// <param name="degrees">Degrees.</param>
		public static float ToRadians(float degrees)
		{
			return degrees * (Pi / 180.0f);
		}

		/// <summary>
		/// ClockWise direction.
		/// </summary>
		/// <returns><c>true</c>, if ClockWise, <c>false</c> otherwise.</returns>
		/// <param name="currentAngle">Current angle.</param>
		/// <param name="angleToReach">Angle to reach.</param>
		public static bool ClockWiseDirection(float currentAngle, float angleToReach)
		{
			var clockWiseDirection = angleToReach - currentAngle;

			if (clockWiseDirection > 0f && Abs(clockWiseDirection) <= 180f) return true;
			if (clockWiseDirection > 0f && Abs(clockWiseDirection) > 180f) return false;
			if (clockWiseDirection < 0f && Abs(clockWiseDirection) <= 180f) return false;
			if (clockWiseDirection < 0f && Abs(clockWiseDirection) > 180f) return true;

			return true;
		}
	}
}

