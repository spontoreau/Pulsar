using System;

namespace Pulsar
{
	/// <summary>
	/// drawable module.
	/// </summary>
	public interface IDrawable : IModule
	{
		/// <summary>
		/// Draw this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		void Draw(GameTime gameTime);
	}
}

