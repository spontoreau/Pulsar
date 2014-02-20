using System;
using SFML.Graphics;

namespace Pulsar.Services
{
	/// <summary>
	/// Sprite batcher.
	/// </summary>
	public interface ISpriteBatchService : IGraphicsBatchService
	{
		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="source">Source.</param>
		/// <param name="color">Color.</param>
		/// <param name="rotation">Rotation.</param>
		/// <param name="origin">Origin.</param>
		void Draw(Texture texture, Rectangle destination, Rectangle source, Color color, float rotation, Vector origin);

		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="position">Position.</param>
		/// <param name="source">Source.</param>
		/// <param name="color">Color.</param>
		/// <param name="rotation">Rotation.</param>
		/// <param name="origin">Origin.</param>
		/// <param name="scale">Scale.</param>
		void Draw(Texture texture, Vector position, Rectangle source, Color color, float rotation, Vector origin, float scale);
	}
}

