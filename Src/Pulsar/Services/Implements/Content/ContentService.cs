using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Pulsar.Services;

namespace Pulsar.Services.Implements.Content
{
	/// <summary>
	/// Content manager.
	/// </summary>
	public sealed class ContentService : IContentService
	{
		/// <summary>
		/// Gets or sets the resolvers.
		/// </summary>
		/// <value>The resolvers.</value>
		private Dictionary<Type, ContentResolver> Resolvers { get; set; }

		/// <summary>
		/// Gets or sets the assets.
		/// </summary>
		/// <value>The assets.</value>
		private Dictionary<string, object> Assets { get; set; }

		/// <summary>
		/// Gets or sets the root directory.
		/// </summary>
		/// <value>The root directory.</value>
		public string RootDirectory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Pulsar.Content.ContentManager"/> lazy loading.
		/// </summary>
		/// <value><c>true</c> if lazy loading; otherwise, <c>false</c>.</value>
		public bool LazyLoading { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Content.ContentManager"/> class.
		/// </summary>
		internal ContentService()
		{
			Assets = new Dictionary<string, object>();
			Resolvers = new Dictionary<Type, ContentResolver>();
			LoadResolvers();
		}

		/// <summary>
		/// Unload this instance.
		/// </summary>
		internal void Unload()
		{
		    foreach (var d in Assets.Select(kvp => kvp.Value).OfType<IDisposable>())
		    {
		        d.Dispose();
		    }

		    Assets.Clear();
		}

		/// <summary>
		/// Load the specified assetFileKey and caching.
		/// </summary>
		/// <param name="assetFileKey">Asset file key.</param>
		/// <param name="caching">If set to <c>true</c> caching.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Load<T>(string assetFileKey, bool caching = true)
        {
			try
			{
				if (string.IsNullOrEmpty(assetFileKey))
					throw new ArgumentException("Asset file name can't be empty or null");

				var assetType = typeof(T);
				object obj;

				var hasAsset = Assets.Keys.Any(a => String.Compare(assetFileKey, a, StringComparison.OrdinalIgnoreCase) == 0);

				if (hasAsset) 
					obj = Assets[assetFileKey];

				if(!CanResolve(assetType))
					throw new ContentLoadException(string.Format("Can't find a resolver for resource {0}", assetFileKey));

				var resolver = FindResolver(assetType);

				var assetFileName = string.Format ("{0}{1}{2}", RootDirectory, Path.DirectorySeparatorChar, assetFileKey);

				if (LazyLoading)//auto find extension
				{
					foreach (var extension in resolver.SupportFileExtensions)
					{
						var lazyAssetFileName = string.Format ("{0}.{1}", assetFileName, extension);
						if (File.Exists (lazyAssetFileName)) 
						{
							assetFileName = lazyAssetFileName;
							break;
						}
					}
				}

				obj = resolver.Load(assetFileName);

				if (caching)
					Assets.Add (assetFileKey, obj);

				return (T)Convert.ChangeType (obj, assetType);
			}
			catch(Exception ex)
			{
				throw new ContentLoadException(string.Format("Failed to load {0}", assetFileKey), ex);
			}
        }

		/// <summary>
		/// Adds the content.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="o">Object.</param>
		internal void AddContent(string key, object o)
		{
			if(!CanResolve(o.GetType()))
				throw new ContentLoadException(string.Format("Can't find a resolver for resource {0}", key));

			Assets.Add(key, o);
		}

		/// <summary>
		/// Finds the resolver.
		/// </summary>
		/// <returns>The resolver.</returns>
		/// <param name="type">Type.</param>
		internal ContentResolver FindResolver(Type type)
		{
			return Resolvers.FirstOrDefault(r => r.Key.Equals(type)).Value;
		}

		/// <summary>
		/// Determines whether this instance can resolve the specified type.
		/// </summary>
		/// <returns><c>true</c> if this instance can resolve the specified type; otherwise, <c>false</c>.</returns>
		/// <param name="type">Type.</param>
		public bool CanResolve(Type type)
		{
			return Resolvers.Any (r => r.Key.Equals(type));
		}

		/// <summary>
		/// Adds the resolver.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="resolver">Resolver.</param>
		internal void AddResolver(Type type, ContentResolver resolver)
		{
			resolver.Content = this;
			Resolvers.Add(type, resolver);
		}

		/// <summary>
		/// Loads the resolvers.
		/// </summary>
		private void LoadResolvers()
		{
			var type = typeof(ContentResolver);

			foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (!t.IsAbstract && type.IsAssignableFrom(t))
				{
					var r = (ContentResolver)Activator.CreateInstance(t);
					AddResolver(r.Type, r);
				}
			}
		}
	}
}

