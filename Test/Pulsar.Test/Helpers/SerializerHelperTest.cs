using System;
using System.IO;
using NUnit.Framework;
using Pulsar.Helpers;

namespace Pulsar.Helpers
{
	[Serializable]
	public class Trace
	{
		public string Source { get; set; }
		public string Message { get; set; }
	}

	[TestFixture]
	public class SerializerHelperTest
	{
		[TestFixtureSetUp]
		public void Clear()
		{
			if(!Directory.Exists("Serialize"))
			{
				Directory.CreateDirectory("Serialize");
			}
			else
			{
				string [] files = Directory.GetFiles("Serialize");
				foreach(string file in files)
					File.Delete(file);
			}
		}

		[Test]
		public void TestSaveBinary()
		{
			var trace = new Trace
			{
				Message = "TraceMsg",
				Source = "TraceSrc"
			};

			SerializerHelper.Save("Serialize/test.bin", trace);

			var actual = File.Exists ("Serialize/test.bin");
			Assert.IsTrue(actual);
		}

		[Test]
		public void TestSaveXml()
		{
			var trace = new Trace
			{
				Message = "TraceMsg",
				Source = "TraceSrc"
			};

			SerializerHelper.Save("Serialize/test.xml", trace);
			var actual = File.Exists("Serialize/test.xml");
			Assert.IsTrue(actual);
		}

		[Test]
		public void TestLoadBinary()
		{
			var expected = new Trace
			{
				Message = "TraceMsg",
				Source = "TraceSrc"
			};

			SerializerHelper.Save("Serialize/test.bin", expected);
			var actual = SerializerHelper.Load<Trace>("Serialize/test.bin");

			Assert.AreEqual(expected.Source, actual.Source);
			Assert.AreEqual(expected.Message, actual.Message);
		}

		[Test]
		public void TestLoadXml()
		{
			var expected = new Trace
			{
				Message = "TraceMsg",
				Source = "TraceSrc"
			};
			SerializerHelper.Save("Serialize/test.xml", expected);
			var actual = SerializerHelper.Load<Trace>("Serialize/test.xml");

			Assert.AreEqual(expected.Source, actual.Source);
			Assert.AreEqual(expected.Message, actual.Message);
		}
	}
}

