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

namespace Pulsar.Content.Resolvers
{
	/// <summary>
	/// Package resolver.
	/// </summary>
	public class PackageResolver : ContentResolver
	{
		/// <summary>
		/// Load a content from a asset file name
		/// </summary>
		/// <param name="assetFileName">Asset name, relative to the loader root directory, and including the file extension.</param>
		/// <returns>Return a Package instance corresponding to the asset file name</returns>
		public override object Load(string assetFileName)
		{
			try
			{
				return Package.Load(assetFileName);
			}
			catch(Exception ex) 
			{
				throw new ContentLoadException ("Unsupport file format", ex);
			}
		}

		/// <summary>
		/// Type manage by the resolver
		/// </summary>
		/// <value>The type.</value>
		protected internal override Type Type 
		{
			get 
			{
				return typeof(Package);
			}
		}
	}
}

