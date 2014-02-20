using System;
using SFML.Graphics;
using SFML.Window;
using Pulsar.Services;

namespace Pulsar.Services.Implements.Graphics
{
	/// <summary>
	/// Text batcher.
	/// </summary>
	public sealed class TextBatchService : GraphicsBatchService, ITextBatchService
	{
		/// <summary>
		/// The _text.
		/// </summary>
		private readonly Text _text = new Text();

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Graphics.TextBatch"/> class.
		/// </summary>
		/// <param name="renderTarget">Render target.</param>
		internal TextBatchService()
		{

		} 
			
		/// <summary>
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
		public void DrawString(Font font, string text, Vector position, Color color, float rotation, Vector origin, float scale, Text.Styles styles = Text.Styles.Regular)
		{
			if (!HasBegin)
				throw new Exception ("TextBatch not start");

			_text.Font = font;
			_text.DisplayedString = text;

			var p = _text.Position;
			p.X = position.X;
			p.Y = position.Y;
			_text.Position = p;

			_text.Rotation = rotation;
			_text.Scale = new Vector2f(scale, scale);

			var c = _text.Color;
			c.A = color.A;
			c.B = color.B;
			c.G = color.G;
			c.R = color.R;
			_text.Color = c;

			_text.Style = styles;
			RenderTarget.Draw(_text);
		}
	}
}

