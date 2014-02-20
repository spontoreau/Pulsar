using System;

namespace Pulsar
{
	/// <summary>
	/// Define a drawable game moduledrawable.
	/// </summary>
	public interface IDrawableGameModule : IGameModule
	{
		/// <summary>
		/// Draw this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		void Draw(GameTime gameTime);
	}
}

