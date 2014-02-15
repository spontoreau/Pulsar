using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Pulsar.Helpers
{
	/// <summary>
	/// Serializer helper.
	/// </summary>
	public static class SerializerHelper
	{
		/// <summary>
		/// The xml extension.
		/// </summary>
		private const string XmlExtension = ".xml";

		/// <summary>
		/// Load object T type from file path.
		/// </summary>
		/// <param name="filePath">File path.</param>
		public static T Load<T>(string filePath)
		{
			try
			{
				var obj = filePath.ToLower().EndsWith(XmlExtension) ? 
				             LoadXml(filePath, typeof(T)) :
				             LoadBinary(filePath);

				if(typeof(T) != typeof(object))
				{
					return (T)Convert.ChangeType(obj, typeof(T));
				}
				else
				{
					return (T)obj;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Failed to load file {0}", filePath), ex);
			}
		}

		/// <summary>
		/// Loads object from binary.
		/// </summary>
		/// <returns>Object</returns>
		/// <param name="filePath">File path.</param>
		private static object LoadBinary(string filePath)
		{
			using(var stream = File.Open(filePath, FileMode.Open))
			{
				var formatter = new BinaryFormatter();
				return formatter.Deserialize(stream);
			}
		}

		/// <summary>
		/// Loads object from xml.
		/// </summary>
		/// <returns>Object.</returns>
		/// <param name="filePath">File path.</param>
		/// <param name="type">Type.</param>
		private static object LoadXml(string filePath, Type type)
		{
			var serializer = new XmlSerializer(type);

			using(var reader = new StreamReader(filePath))
			{
				return serializer.Deserialize(reader);
			}
		}

		/// <summary>
		/// Save the specified obj at file path.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="obj">Object.</param>
		public static void Save(string filePath, object obj)
		{
			try
			{
				if (filePath.ToLower().EndsWith(XmlExtension))
					SaveXml(filePath, obj);
				else
					SaveBinary(filePath, obj);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("File {0} save failed" , filePath), ex);
			}
		}

		/// <summary>
		/// Saves object to binary.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="obj">Object.</param>
		private static void SaveBinary(string filePath, object obj)
		{
			using (var stream = File.Open (filePath, FileMode.OpenOrCreate)) 
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(stream, obj);
			}
		}

		/// <summary>
		/// Saves object to xml.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="obj">Object.</param>
		private static void SaveXml(string filePath, object obj)
		{
			var serializer = new XmlSerializer(obj.GetType());

			using (var writer = new StreamWriter (filePath)) 
			{
				serializer.Serialize(writer, obj);
			}
		}
	}
}