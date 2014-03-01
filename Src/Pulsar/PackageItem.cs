using System;
using System.IO;

namespace Pulsar
{
	/// <summary>
	/// Package item.
	/// </summary>
	[Serializable]
	public class PackageItem
	{
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		public string Key { get; private set; }

		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName { get; private set;}

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets the byte array.
		/// </summary>
		/// <value>The byte array.</value>
		public byte[] ByteArray { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.PackageItem"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="key">Key.</param>
		/// <param name="assetName">Asset name.</param>
		internal PackageItem (Type type, string key, string assetName)
		{
			Type = type;
			Key = key;
			FileName = Path.GetFileName (assetName);
			ByteArray = File.ReadAllBytes(assetName);
		}
	}
}

