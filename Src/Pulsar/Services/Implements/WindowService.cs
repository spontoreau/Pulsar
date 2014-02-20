using System;
using System.Reflection;
using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Services.Implements
{
	/// <summary>
	/// Window service.
	/// </summary>
	public class WindowService : IWindowService
	{
		/// <summary>
		/// Occurs when creating.
		/// </summary>
		public event EventHandler<EventArgs> Creating;

		/// <summary>
		/// Occurs when created.
		/// </summary>
		public event EventHandler<EventArgs> Created;

		/// <summary>
		/// Gets a value indicating whether this instance is created.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsCreated { get; private set; }

		/// <summary>
		/// Gets the window.
		/// </summary>
		/// <value>The window.</value>
		public RenderWindow Window { get; private set; }


		/// <summary>
		/// Create the RenderWindow
		/// </summary>
		/// <param name="videoMode">VideoMode to apply</param>
		/// <param name="title">Title of the RenderWindow</param>
		/// <param name="styles">Styles of the RenderWindow</param>
		public void Create(VideoMode videoMode, string title, Styles styles)
		{
			IsCreated = false;
			OnCreating(EventArgs.Empty);
			View view = null;

			if (Window != null)
			{
				view = Window.GetView();
				Window.Close();
			}

			Window = new RenderWindow(videoMode, title, styles);

			if(view != null)
				Window.SetView(view);

			Window.SetActive(true);

			IsCreated = true;
			OnCreated(EventArgs.Empty);
		}

		/// <summary>
		/// Called when the WindowContext start to create the RenderWindow. Raises the Creating event.
		/// </summary>
		/// <param name="eventArgs">Arguments to the Creating event.</param>
		private void OnCreating(EventArgs eventArgs)
		{
			var tmp = Creating;

			if (tmp != null)
				Creating(null, eventArgs);
		}

		/// <summary>
		/// Called when the WindowContext created the RenderWindow. Raises the Create event.
		/// </summary>
		/// <param name="eventArgs">Arguments to the Create event.</param>
		private void OnCreated(EventArgs eventArgs)
		{
			var tmp = Created;

			if (tmp != null)
				Created(null, eventArgs);
		}
	}
}
