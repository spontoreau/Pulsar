/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2014, Sylvain PONTOREAU (pontoreau.sylvain@gmail.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Pulsar.Helpers;

namespace Pulsar.Content
{
	/// <summary>
	/// Define a compress package of texture
	/// </summary>
	[Serializable]
	public class Package
	{
		private const string PackageFileExtension = ".ppk";

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
		/// <param name="name">Name. Must contains only numeric and char</param>
		public Package (string name)
		{
			var regex = new Regex ("^[a-zA-Z0-9]+$");

			if (!regex.IsMatch (name))
				throw new ArgumentException ("Invalid name");

			Items = new List<PackageItem> ();
		}

		/// <summary>
		/// Add the specified assetFileName.
		/// </summary>
		/// <param name="assetFileName">Asset file name.</param>
		public void Add(Type type, string key, string assetFileName)
		{
			Items.Add(new PackageItem (type, key, assetFileName));
		}

		/// <summary>
		/// Save package at specific path.
		/// </summary>
		/// <param name="package">Package.</param>
		/// <param name="path">Path.</param>
		public static void Save(Package package, string path)
		{
			if (!Directory.Exists (path))
				throw new Exception ("Path not exist");

			var completeFilePath = Path.Combine(path, FileName); 

			using (var stream = new MemoryStream ()) 
			{
				var formatter = new BinaryFormatter ();
				formatter.Serialize (stream, package);

				var compressByteArray = ZipHelper.Compress (stream);

				File.WriteAllBytes (completeFilePath, compressByteArray);
			}
		}

		/// <summary>
		/// Load the specified package from file path.
		/// </summary>
		/// <param name="filePath">File Path.</param>
		public static Package Load(string filePath)
		{
			if (!File.Exists (path))
				throw new Exception ("Path not exist");

			var compressByteArray = File.ReadAllBytes(path);

			var uncompressByteArray = ZipHelper.Uncompress (compressByteArray);

			using (var stream = new MemoryStream (uncompressByteArray)) 
			{
				var formatter = new BinaryFormatter();
				//Deserialize the storable entity.
				return (Package)formatter.Deserialize(stream);
			}
		}
	}
}

