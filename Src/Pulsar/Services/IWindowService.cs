using System;
using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Services
{
	/// <summary>
	/// The Window context
	/// </summary>
	public interface IWindowService
	{
		/// <summary>
		/// Occurs when creating.
		/// </summary>
		event EventHandler<EventArgs> Creating;

		/// <summary>
		/// Occurs when created.
		/// </summary>
		event EventHandler<EventArgs> Created;

		/// <summary>
		/// Gets a value indicating whether this instance is created.
		/// </summary>
		/// <value><c>true</c> if this instance is created; otherwise, <c>false</c>.</value>
		bool IsCreated { get; }

		/// <summary>
		/// Gets the window.
		/// </summary>
		/// <value>The window.</value>
		RenderWindow Window { get; }

		/// <summary>
		/// Create the specified window.
		/// </summary>
		/// <param name="videoMode">Video mode.</param>
		/// <param name="title">Title.</param>
		/// <param name="styles">Styles.</param>
		void Create (VideoMode videoMode, string title, Styles styles);
	}
}

