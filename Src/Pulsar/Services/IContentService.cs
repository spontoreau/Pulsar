using System;

namespace Pulsar.Services
{
	/// <summary>
	/// Content manager.
	/// </summary>
	public interface IContentService
	{
		/// <summary>
		/// Gets or sets the root directory.
		/// </summary>
		/// <value>The root directory.</value>
		string RootDirectory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Pulsar.Content.ContentManager"/> lazy loading.
		/// </summary>
		/// <value><c>true</c> if lazy loading; otherwise, <c>false</c>.</value>
		bool LazyLoading { get; set; }

		/// <summary>
		/// Load the specified assetFileKey and caching.
		/// </summary>
		/// <param name="assetFileKey">Asset file key.</param>
		/// <param name="caching">If set to <c>true</c> caching.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		T Load<T>(string assetFileKey, bool caching = true);

		/// <summary>
		/// Determines whether this instance can resolve the specified type.
		/// </summary>
		/// <returns><c>true</c> if this instance can resolve the specified type; otherwise, <c>false</c>.</returns>
		/// <param name="type">Type.</param>
		bool CanResolve(Type type);
	}
}

