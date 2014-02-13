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
using System.IO;

namespace Pulsar.Content
{
	/// <summary>
	/// Define an item of a package
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

