using System;
using System.IO;
using NUnit.Framework;
using Pulsar.Helpers;

namespace Pulsar.Helpers
{
	[TestFixture]
	public class ZipHelperTest
	{
		[Test]
		public void CompressTest()
		{
			var bytes = new byte[1] { 12 };
			var bytesExpected = new byte[21] { 31, 139, 8, 0, 0, 0, 0, 0, 0, 3, 227, 1, 0, 166, 163, 180, 219, 1, 0, 0, 0 };
			var compressBytes = ZipHelper.Compress(ref bytes);

			Assert.AreEqual(bytesExpected.Length, compressBytes.Length);

			for(var i = 0; i < bytesExpected.Length; i++)
				Assert.AreEqual(bytesExpected[i], compressBytes[i]);
		}

		[Test]
		public void UncompressTest()
		{
			var bytes = new byte[21] { 31, 139, 8, 0, 0, 0, 0, 0, 0, 3, 227, 1, 0, 166, 163, 180, 219, 1, 0, 0, 0 };
			var bytesExpected = new byte[1] { 12 };
			var unCompressBytes = ZipHelper.Uncompress(ref bytes);

			Assert.AreEqual(bytesExpected.Length, unCompressBytes.Length);

			for (var i = 0; i < bytesExpected.Length; i++)
				Assert.AreEqual(bytesExpected[i], unCompressBytes[i]);
		}
	}
}

