using System;
using System.IO;
using System.IO.Compression;

namespace Pulsar.Helpers
{
	/// <summary>
	/// Zip helper.
	/// </summary>
	public static class ZipHelper
	{
		/// <summary>
		/// Compress the specified byteArray.
		/// </summary>
		/// <param name="byteArray">Byte array.</param>
		public static byte[] Compress(ref byte[] byteArray)
		{
			using(var stream = new MemoryStream(byteArray))
			{
				using (var outStream = new MemoryStream ()) 
				{
					var buffer = new byte[stream.Length];
					var bufferRead = 0;

					using (var gZipStream = new GZipStream (outStream, CompressionMode.Compress)) 
					{
						bufferRead = stream.Read(buffer, 0, buffer.Length);
						gZipStream.Write(buffer, 0, bufferRead);
					}

					return outStream.ToArray();
				}
			}
		}

		/// <summary>
		/// Uncompress a stream.
		/// </summary>
		/// <param name="stream">The stream to uncompress.</param>
		/// <returns>Byte array representation of the uncompressed stream.</returns>
		public static byte[] Uncompress(ref byte[] byteArray)
		{
			using(var stream = new MemoryStream(byteArray))
			{
				using (var decompressedStream = new MemoryStream ()) 
				{
					var bufferLen = 1024;
					var numberRead = 0;

					var decompressByteArray = new byte[bufferLen];

					using (var gZipStream = new GZipStream (stream, CompressionMode.Decompress)) 
					{
						do
						{
							numberRead = gZipStream.Read(decompressByteArray, 0, decompressByteArray.Length);
							decompressedStream.Write(decompressByteArray, 0, numberRead);
						}
						while (numberRead > 0);

						gZipStream.Close();

						return decompressedStream.ToArray();
					}
				}
			}
		}
	}
}

