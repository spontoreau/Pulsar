using System;

namespace Pulsar
{
	/// <summary>
	/// Game module.
	/// </summary>
	public interface IGameModule
	{
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		void Update(GameTime gameTime);
	}
}

