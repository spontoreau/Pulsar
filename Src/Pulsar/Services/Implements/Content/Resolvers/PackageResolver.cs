using System;

namespace Pulsar.Services.Implements.Content.Resolvers
{
	/// <summary>
	/// Package resolver.
	/// </summary>
	public class PackageResolver : ContentResolver
	{
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		protected internal override Type Type 
		{
			get 
			{
				return typeof(Package);
			}
		}

		/// <summary>
		/// The _extensions.
		/// </summary>
		private string[] _extensions = new string[] { "PPK" };
			
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
			var ppk = Package.Load(assetFile);

			foreach(var item in ppk.Items)
				Load(item);

			return ppk;
		}
			
		/// <summary>
		/// Load the specified assetFile.
		/// </summary>
		/// <param name="assetFile">Asset file.</param>
		/// <param name="byteArray">Byte array.</param>
		protected internal override object Load (byte[] byteArray)
		{
			var ppk = Package.Load(ref byteArray);

			foreach(var item in ppk.Items)
				Load(item);

			return ppk;
		}

		/// <summary>
		/// Load the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		private void Load(PackageItem item)
		{
			if(!Content.CanResolve(item.Type))
				throw new ContentLoadException(string.Format("No resolver for type : {0}", item.Type.FullName));

			var resolver = Content.FindResolver(item.Type);
			var obj = resolver.Load (item.ByteArray);

			if (obj == null)
				throw new ContentLoadException (string.Format("Failed to load an item of the package : {0} - {1}", item.Key, item.Type.FullName));

			Content.AddContent (item.Key, obj);
		}
	}
}

