using System;
using System.Linq;

namespace Pulsar.Content
{
	/// <summary>
	/// Resolve a content type
	/// </summary>
	public abstract class ContentResolver
	{
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		protected internal ContentManager Content { get; internal set; }

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		protected internal abstract Type Type { get; } 

		/// <summary>
		/// Gets the support file extensions.
		/// </summary>
		/// <value>The support file extensions.</value>
		protected internal abstract string[] SupportFileExtensions { get;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Content.ContentResolver"/> class.
		/// </summary>
		protected internal ContentResolver()
		{
		}

		/// <summary>
		/// Load the specified assetFile.
		/// </summary>
		/// <param name="assetFile">Asset file.</param>
		protected internal abstract object Load(string assetFile);

		/// <summary>
		/// Load the specified byteArray.
		/// </summary>
		/// <param name="byteArray">Byte array.</param>
		protected internal abstract object Load(byte[] byteArray);

		/// <summary>
		/// Determines whether this instance can resolve the specified file extension.
		/// </summary>
		/// <returns><c>true</c> if this instance can resolve the specified file extension; otherwise, <c>false</c>.</returns>
		/// <param name="fileExtension">File extension.</param>
		internal bool CanResolve(string fileExtension)
		{
			return SupportFileExtensions.Contains (fileExtension.ToUpper ());
		}
	}
}

