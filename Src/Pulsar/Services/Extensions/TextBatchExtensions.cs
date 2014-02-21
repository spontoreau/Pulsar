using System;
using SFML.Graphics;
using Pulsar.Services.Implements.Graphics;

namespace Pulsar.Services
{
	/// <summary>
	/// Text batch extensions.
	/// </summary>
	public static class TextBatchExtensions
	{
		/// <summary>
		/// Draws the string.
		/// </summary>
		/// <param name="font">Font.</param>
		/// <param name="text">Text.</param>
		/// <param name="position">Position.</param>
		/// <param name="color">Color.</param>
		public static void DrawString(this ITextBatchService batch, Font font, string text, Vector position, Color color)
		{
			batch.DrawString(font, text, position, color, 0.0f, Vector.Zero, 1.0f);
		}
	}
}

