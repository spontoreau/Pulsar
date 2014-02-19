using System;
using SFML.Graphics;

namespace Pulsar.Services
{
	/// <summary>
	/// Text batcher.
	/// </summary>
	public interface ITextBatch : IGraphicsBatch
	{
		// <summary>
		/// Draws the string.
		/// </summary>
		/// <param name="font">Font.</param>
		/// <param name="text">Text.</param>
		/// <param name="position">Position.</param>
		/// <param name="color">Color.</param>
		/// <param name="rotation">Rotation.</param>
		/// <param name="origin">Origin.</param>
		/// <param name="scale">Scale.</param>
		/// <param name="styles">Styles.</param>
		void DrawString(Font font, string text, Vector position, Color color, float rotation, Vector origin, float scale, Text.Styles styles = Text.Styles.Regular);
	}
}

