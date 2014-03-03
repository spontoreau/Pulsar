using System;
using Pulsar.Module;

namespace Pulsar
{
	/// <summary>
	/// Game module.
	/// </summary>
	public abstract class GameModule : IModule
	{
		/// <summary>
		/// Gets or sets the global data.
		/// </summary>
		/// <value>The global data.</value>
		public GameData GlobalData { get; internal set; }

		/// <summary>
		/// Gets or sets the temp data.
		/// </summary>
		/// <value>The temp data.</value>
		public GameData TempData { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.GameModule"/> class.
		/// </summary>
		public GameModule ()
		{
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public abstract void Initialize ();

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public abstract void Update (GameTime gameTime);
	}
}

