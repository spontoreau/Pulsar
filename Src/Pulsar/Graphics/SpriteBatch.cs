using System;
using SFML.Graphics;
using SFML.Window;
using Pulsar.Helpers;

namespace Pulsar.Graphics
{
	/// <summary>
	/// Sprite batcher.
	/// </summary>
	public sealed class SpriteBatch : GraphicsBatch
	{
		/// <summary>
		/// The _sprite.
		/// </summary>
		private readonly Sprite _sprite = new Sprite();

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Graphics.SpriteBatch"/> class.
		/// </summary>
		/// <param name="renderTarget">Render target.</param>
		internal SpriteBatch(RenderTarget renderTarget)
			: base(renderTarget)
		{

		}        

		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="source">Source.</param>
		/// <param name="color">Color.</param>
		/// <param name="rotation">Rotation.</param>
		/// <param name="origin">Origin.</param>
		public void Draw(Texture texture, Rectangle destination, Rectangle source, Color color, float rotation, Vector origin)
		{
			if (!HasBegin)
				throw new Exception ("SpriteBatch not start");

			var tr = _sprite.TextureRect;
			if (source != null) {
				tr.Left = (int)source.X;
				tr.Top = (int)source.Y;
				tr.Width = (int)source.Width;
				tr.Height = (int)source.Height;
			} 
			else 
			{
				tr.Left = 0;
				tr.Top = 0;
				tr.Width = (int)texture.Size.X;
				tr.Height = (int)texture.Size.Y;
			}
			_sprite.TextureRect = tr;

	        _sprite.Texture = texture;

	        var position = _sprite.Position;
	        position.X = destination.Position.X;
	        position.Y = destination.Position.X;
	        _sprite.Position = position;

	        var c = _sprite.Color;
	        c.A = color.A;
	        c.B = color.B;
	        c.G = color.G;
	        c.R = color.R;
	        _sprite.Color = c;

			_sprite.Rotation = MathHelper.ToDegrees(rotation);

	        var o = _sprite.Origin;
	        o.X = origin.X;
	        o.Y = origin.Y;
	        _sprite.Origin = o;

			var s = _sprite.Scale;
			s.X = destination.Width/_sprite.TextureRect.Width;
			s.Y = destination.Height / _sprite.TextureRect.Height;
			_sprite.Scale = s;

	        RenderTarget.Draw(_sprite);
		}

		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="source">Source.</param>
		/// <param name="color">Color.</param>
		public void Draw(Texture texture, Rectangle destination, Rectangle source, Color color)
		{
			Draw(texture, destination, source, color, 0f, Vector.Zero);
		}

		/// <summary>
		/// Draw the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="color">Color.</param>
		public void Draw(Texture texture, Rectangle destination, Color color)
		{
			Draw(texture, destination, null, color);
		}

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
		public void Draw(Texture texture, Vector position, Rectangle source, Color color, float rotation, Vector origin, float scale)
		{
			if (!HasBegin)
				throw new Exception ("SpriteBatch not start");

			var tr = _sprite.TextureRect;
			if (source != null) {
				tr.Left = (int)source.X;
				tr.Top = (int)source.Y;
				tr.Width = (int)source.Width;
				tr.Height = (int)source.Height;
			}
			else 
			{
				tr.Left = 0;
				tr.Top = 0;
				tr.Width = (int)texture.Size.X;
				tr.Height = (int)texture.Size.Y;
			}
			_sprite.TextureRect = tr;

	        _sprite.Texture = texture;

	        var p = _sprite.Position;
	        p.X = position.X;
	        p.Y = position.X;
	        _sprite.Position = p;

	        var c = _sprite.Color;
	        c.A = color.A;
	        c.B = color.B;
	        c.G = color.G;
	        c.R = color.R;
	        _sprite.Color = c;

	        _sprite.Rotation = MathHelper.ToDegrees(rotation);

	        var o = _sprite.Origin;
	        o.X = origin.X;
	        o.Y = origin.Y;
	        _sprite.Origin = o;

			var s = _sprite.Scale;
			s.X = scale;
			s.Y = scale;

			_sprite.Scale = s;

	        RenderTarget.Draw(_sprite);
		}

		/// <summary>
		/// Draw a texture
		/// </summary>
		/// <param name="texture">Texture to draw</param>
		/// <param name="position">Position of the texture</param>
		/// <param name="source">Source rectangle in the texture to draw</param>
		/// <param name="color">Global color</param>
		public void Draw(Texture texture, Vector position, Rectangle source, Color color)
		{
			Draw(texture, position, source, color, 0f, Vector.Zero, 1.0f); 
		}

		/// <summary>
		/// Draw a texture
		/// </summary>
		/// <param name="texture">Texture to draw</param>
		/// <param name="position">Position of the texture</param>
		/// <param name="color">Global color</param>
		public void Draw(Texture texture, Vector position, Color color)
		{
			Draw(texture, position, null, color, 0f, Vector.Zero, 1.0f); 
		}


	}
}

