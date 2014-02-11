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
using System.IO;
using System.Linq;

namespace Pulsar.Content
{
    /// <summary>
	/// The ContentManager is the run-time component which loads managed objects from support file format.
	/// </summary>
	public sealed class ContentManager : IDisposable
	{
		/// <summary>
		/// Resolvers
		/// </summary>
		private Dictionary<Type, ContentResolver> Resolvers { get; set; }

		/// <summary>
		/// Asset Dictionary use to store 
		/// </summary>
		private Dictionary<string, object> Assets { get; set; }

		/// <summary>
		/// Gets or sets the root directory associated with this ContentManager
		/// </summary>
		public string RootDirectory { get; set; }

		/// <summary>
		///  Releases the resources used by the ContentManager class. 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///  Initializes a new instance of ContentManager
		/// </summary>
		public ContentManager()
		{
			Assets = new Dictionary<string, object>();
			Resolvers = new Dictionary<Type, ContentResolver>();
			LoadResolvers();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the ContentManager and optionally releases the managed resources. 
		/// </summary>
		/// <param name="isDisposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		public void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				Unload();
			}
		}

        ~ContentManager()
		{
			Dispose(false);
		}

		/// <summary>
		/// Disposes all data that was loaded by this ContentManager.
		/// </summary>
		private void Unload()
		{
		    foreach (var d in Assets.Select(kvp => kvp.Value).OfType<IDisposable>())
		    {
		        d.Dispose();
		    }

		    Assets.Clear();
		}

        /// <summary>
		/// Loads an asset. 
		/// </summary>
		/// <typeparam name="T">The type of asset to load.</typeparam>
		/// <param name="assetFileName">Asset name, relative to the loader root directory, and including the file extension.</param>
		/// <param name="caching">Optional. True by default, if false content manager don't keep in memory the T type object</param>
		/// <returns>The loaded asset.</returns>
		public T Load<T>(string assetFileName, bool caching = true)
        {
            if (string.IsNullOrEmpty(assetFileName))
            {
                throw new ContentLoadException(""); //TODO explicit exception
            }

            object obj;

            if (!Assets.TryGetValue(assetFileName, out obj))
            {
                //Get the Resolver for T type
                if (Resolvers.ContainsKey(typeof (T)))
                {
                    ContentResolver resolver;
                    if (Resolvers.TryGetValue(typeof (T), out resolver))
                    {
                        try
                        {
                            obj = resolver.Load(RootDirectory + Path.DirectorySeparatorChar + assetFileName);
                        }
                        catch (Exception ex)
                        {
                            throw new ContentLoadException("", ex); //TODO explicit exception
                        }
                    }
                    else
                    {
                        throw new ContentLoadException(""); //TODO explicit exception
                    }
                }
                else
                {
                    throw new ContentLoadException(""); //TODO explicit exception
                }

                if (caching)
                    Assets.Add(assetFileName, obj);
            }
            return (T) Convert.ChangeType(obj, typeof (T));
        }

        /// <summary>
		/// Add a resolver to the content manager
		/// </summary>
		/// <param name="type">Type to resolve</param>
		/// <param name="resolver">Resolver dedicate to the type</param>
		public void AddResolver(Type type, ContentResolver resolver)
		{
			resolver.Content = this;
			Resolvers.Add(type, resolver);
		}

		/// <summary>
		/// Set default resolvers to the content manager
		/// </summary>
		private void LoadResolvers()
		{
			//TODO auto load resolver from app domain
		}
	}
}

