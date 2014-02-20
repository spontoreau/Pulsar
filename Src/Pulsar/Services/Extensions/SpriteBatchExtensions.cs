using System;
using SFML.Graphics;
using Pulsar.Services.Implements.Graphics;

namespace Pulsar.Services
{
	/// <summary>
	/// Sprite batch extensions.
	/// </summary>
	public static class SpriteBatchExtensions
	{
		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="source">Source.</param>
		/// <param name="color">Color.</param>
		public static void Draw(this SpriteBatchService batch, Texture texture, Rectangle destination, Rectangle source, Color color)
		{
			batch.Draw(texture, destination, source, color, 0f, Vector.Zero);
		}

		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="color">Color.</param>
		public static void Draw(this SpriteBatchService batch, Texture texture, Rectangle destination, Color color)
		{
			batch.Draw(texture, destination, null, color);
		}


		/// <summary>
		/// Draw a texture
		/// </summary>
		/// <param name="texture">Texture to draw</param>
		/// <param name="position">Position of the texture</param>
		/// <param name="source">Source rectangle in the texture to draw</param>
		/// <param name="color">Global color</param>
		public static void Draw(this SpriteBatchService batch, Texture texture, Vector position, Rectangle source, Color color)
		{
			batch.Draw(texture, position, source, color, 0f, Vector.Zero, 1.0f); 
		}

		/// <summary>
		/// Draw a texture
		/// </summary>
		/// <param name="texture">Texture to draw</param>
		/// <param name="position">Position of the texture</param>
		/// <param name="color">Global color</param>
		public static void Draw(this SpriteBatchService batch, Texture texture, Vector position, Color color)
		{
			batch.Draw(texture, position, null, color, 0f, Vector.Zero, 1.0f); 
		}

	}
}

