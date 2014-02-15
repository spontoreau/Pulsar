using System;
using NUnit.Framework;
using Pulsar.Helpers;

namespace Pulsar.Helpers
{
	[TestFixture]
	public class MathHelperTest
	{
		[Test]
		public void TestPi()
		{
			var actual = MathHelper.Pi;
			var expected = (float)Math.PI;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestTwoPi()
		{
			var actual = MathHelper.TwoPi;
			var expected = (float)Math.PI * 2;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestE()
		{
			var actual = MathHelper.E;
			var expected = (float)Math.E;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestAbs()
		{
			var actual = MathHelper.Abs(-1f);
			var expected = 1f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestSqrt()
		{
			var actual = MathHelper.Sqrt(9f);
			var expected = 3f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestSquare()
		{
			var actual = MathHelper.Square(3f);
			var expected = 9f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestPow()
		{
			var actual = MathHelper.Pow(3f, 2f);
			var expected = 9f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestAcos()
		{
			var actual = MathHelper.Round(MathHelper.Acos(0f), 6);
			var expected = 1.570796f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestAsin()
		{
			var actual = MathHelper.Round(MathHelper.Asin(1f), 6);
			var expected = 1.570796f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestAtan()
		{
			var actual = MathHelper.Round(MathHelper.Atan(1f), 6);
			var expected = 0.785398f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestAtan2()
		{
			var actual = MathHelper.Round(MathHelper.Atan2(1f, 1f), 6);
			var expected = 0.785398f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestCos()
		{
			var actual = MathHelper.Round(MathHelper.Cos(MathHelper.Pi / 2f), 6);
			var expected = 0f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestSin()
		{
			var actual = MathHelper.Round(MathHelper.Sin(MathHelper.Pi / 2f), 6);
			var expected = 1f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestTan()
		{
			var actual = MathHelper.Round(MathHelper.Tan(MathHelper.Pi / 4f), 6);
			var expected = 1f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestExp()
		{
			var actual = MathHelper.Round(MathHelper.Exp(1.0f), 6);
			var expected = MathHelper.Round(MathHelper.E, 6);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestLog()
		{
			var actual = MathHelper.Round(MathHelper.Log(MathHelper.E), 6);
			var expected = 1f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestLog10()
		{
			var actual = MathHelper.Log10(10f);
			var expected = 1f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestLerp()
		{
			var actual = MathHelper.Lerp(1f, 2f, 0.5f);
			var expected = 1.5f;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestClamp()
		{
			var expected = 3f;
			var expectedMin = 4f;
			var expectedMax = 2f;
			var actual = MathHelper.Clamp (expected, 2f, 4f);
			var actualMin = MathHelper.Clamp (expected, 4f, 5f);
			var actualMax = MathHelper.Clamp (expected, 1f, 2f);

			Assert.AreEqual (expected, actual);
			Assert.AreEqual (expectedMin, actualMin);
			Assert.AreEqual (expectedMax, actualMax);
		}

		[Test]
		public void TestMax()
		{
			var expected = 3f;
			var actual = MathHelper.Max(1f, 3f);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMin()
		{
			var expected = 1f;
			var actual = MathHelper.Min(1f, 3f);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestRound()
		{
			var expectedMin = 1f;
			var expectedMax = 2f;
			var expectedMiddle = 2f;

			var actualMin = MathHelper.Round (1.2f);
			var actualMax = MathHelper.Round (1.7f);
			var actualMiddle = MathHelper.Round (1.5f);

			Assert.AreEqual (expectedMin, actualMin);
			Assert.AreEqual (expectedMax, actualMax);
			Assert.AreEqual (expectedMiddle, actualMiddle);
		}

		[Test]
		public void TestDistance()
		{
			var expectedX = 1f;
			var actualX = MathHelper.Distance (1f, 0f, 2f, 0f);

			var expectedY = 1f;
			var actualY = MathHelper.Distance (0f, 1f, 0f, 2f);

			Assert.AreEqual (expectedY, actualY);
			Assert.AreEqual (expectedX, actualX);
		}

		[Test]
		public void TestRadiansAngle()
		{
			var expected = MathHelper.Pi;
			var actual = MathHelper.RadiansAngle(0f, 0f, 0f, -1f);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestToDegrees()
		{
			var expected = 180f;
			var actual = MathHelper.ToDegrees (MathHelper.Pi);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestToRadians()
		{
			var expected = MathHelper.Pi;
			var actual = MathHelper.ToRadians (180f);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestClockWiseDirection()
		{
			var clockWise = MathHelper.ClockWiseDirection (90f, 180f);
			var unClockWise = MathHelper.ClockWiseDirection (180f, 90f);

			Assert.IsTrue (clockWise);
			Assert.IsFalse (unClockWise);
		}
	}
}