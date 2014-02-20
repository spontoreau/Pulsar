using System;
using System.IO;
using SFML.Graphics;

namespace Pulsar.Services.Implements.Content.Resolvers
{
	/// <summary>
	/// Font resolver.
	/// </summary>
	public class FontResolver : ContentResolver
	{
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		protected internal override Type Type 
		{
			get 
			{
				return typeof(Font);
			}
		}

		/// <summary>
		/// The _extensions.
		/// </summary>
		private string[] _extensions = new string[] { "TTF", "OTF" };

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
		protected internal override object Load(string assetFile)
		{
			return new Font(assetFile);
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
				return new Font(stream);
			}
		}
	}
}

