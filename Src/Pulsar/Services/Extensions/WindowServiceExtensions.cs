using System;
using SFML.Window;
using System.Reflection;

namespace Pulsar.Services
{
	/// <summary>
	/// Window service extensions.
	/// </summary>
	public static class WindowServiceExtensions
	{

		/// <summary>
		/// Default Window VideoMode
		/// </summary>
		private static readonly VideoMode DefaultWindowVideoMode = new VideoMode(800, 600, 32);

		/// <summary>
		/// Default Window Title
		/// </summary>
		private static readonly string DefaultWindowTitle = Assembly.GetExecutingAssembly().GetName().Name;

		/// <summary>
		/// Default Window Styles
		/// </summary>
		private const Styles DefaultWindowStyle = Styles.Default;

		/// <summary>
		/// Default Create method
		/// </summary>
		internal static void Create(this IWindowService windowService)
		{
			windowService.Create(DefaultWindowVideoMode, DefaultWindowTitle, DefaultWindowStyle);
		}
	}
}

