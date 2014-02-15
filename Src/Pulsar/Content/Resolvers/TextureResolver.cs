using System;
using System.IO;
using SFML.Graphics;
using Pulsar.Content;

namespace Pulsar.Content.Resolvers
{
	/// <summary>
	/// Texture resolver.
	/// </summary>
	public class TextureResolver : ContentResolver
	{
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		protected internal override Type Type 
		{
			get 
			{
				return typeof(Texture);
			}
		}

		/// <summary>
		/// The _extensions.
		/// </summary>
		private string[] _extensions = new string[] { "PNG", "JPG", "JPEG", "BMP" };

		/// <summary>
		/// Gets the support file extensions.
		/// </summary>
		/// <value>The support file extensions.</value>
		protected internal override string[] SupportFileExtensions 
		{
			get 
			{
				return _extensions;
			}
		}

		/// <summary>
		/// Load the specified assetFile.
		/// </summary>
		/// <param name="assetFile">Asset file.</param>
		/// <param name="assetFileName">Asset file name.</param>
		protected internal override object Load(string assetFile)
		{
			return new Texture (assetFile);
		}

		/// <summary>
		/// Load the specified assetFile.
		/// </summary>
		/// <param name="assetFile">Asset file.</param>
		/// <param name="byteArray">Byte array.</param>
		protected internal override object Load (byte[] byteArray)
		{
			using(var stream = new MemoryStream(byteArray))
			{
				return new Texture(stream);
			}
		}
	}
}

