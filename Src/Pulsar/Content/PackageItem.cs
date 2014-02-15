using System;
using System.IO;

namespace Pulsar.Content
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
		public string Key { get; set; }

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
			ByteArray = File.ReadAllBytes(assetName);
		}
	}
}

