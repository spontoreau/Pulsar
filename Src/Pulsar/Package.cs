using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Pulsar.Helpers;

namespace Pulsar
{
	/// <summary>
	/// Package.
	/// </summary>
	[Serializable]
	public class Package
	{
		/// <summary>
		/// The package file extension.
		/// </summary>
		public const string PackageFileExtension = ".ppk";

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get 
			{
				return string.Format("{0}{1}", Name, PackageFileExtension);
			}
		}

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		internal List<PackageItem> Items { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Content.Package"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		public Package (string name)
		{
			var regex = new Regex ("^[a-zA-Z0-9]+$");

			if (!regex.IsMatch (name))
				throw new ArgumentException ("Invalid name");

			Name = name;
			Items = new List<PackageItem> ();
		}
			
		/// <summary>
		/// Add the specified type, key and assetFile.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="key">Key.</param>
		/// <param name="assetFile">Asset file.</param>
		public bool Add(Type type, string key, string assetFile)
		{
			if (Items.Any (x => x.Key == key))
				return false;

			Items.Add(new PackageItem (type, key, assetFile));
			return true;
		}

		/// <summary>
		/// Save the specified package and path.
		/// </summary>
		/// <param name="package">Package.</param>
		/// <param name="path">Path.</param>
		public static void Save(Package package, string path)
		{
			if (!Directory.Exists (path))
				throw new Exception ("Path not exist");

			var completeFilePath = Path.Combine(path, package.FileName); 

			using (var stream = new MemoryStream ()) 
			{
				var formatter = new BinaryFormatter ();
				formatter.Serialize (stream, package);

				var byteArray = stream.ToArray ();
				var compressByteArray = ZipHelper.Compress (ref byteArray);

				File.WriteAllBytes (completeFilePath, compressByteArray);
			}
		}

		/// <summary>
		/// Load the specified filePath.
		/// </summary>
		/// <param name="filePath">File path.</param>
		public static Package Load(string filePath)
		{
			if (!File.Exists (filePath))
				throw new Exception ("File not exist");

			var compressByteArray = File.ReadAllBytes(filePath);

			return Load(ref compressByteArray);
		}

		/// <summary>
		/// Load the specified byteArray.
		/// </summary>
		/// <param name="byteArray">Byte array.</param>
		public static Package Load(ref byte[] byteArray)
		{
			var uncompressByteArray = ZipHelper.Uncompress (ref byteArray);

			using (var stream = new MemoryStream (uncompressByteArray)) 
			{
				var formatter = new BinaryFormatter();
				return (Package)formatter.Deserialize(stream);
			}
		}
	}
}

