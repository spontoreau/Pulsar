using System;
using SFML.Graphics;
using SFML.Window;
using Pulsar.Helpers;
using Pulsar.Services;
using SfmlColor = SFML.Graphics.Color;

namespace Pulsar.Services.Implements.Graphics
{
	/// <summary>
	/// Sprite batcher.
	/// </summary>
	public sealed class SpriteBatchService : GraphicsBatchService, ISpriteBatchService
	{
		/// <summary>
		/// The _sprite.
		/// </summary>
		private readonly Sprite _sprite = new Sprite();

		private Vector2f _position = new Vector2f(0,0);
		private Vector2f _origin = new Vector2f(0,0);
		private Vector2f _scale = new Vector2f(0,0);
		private SfmlColor _color = SfmlColor.White;
		private IntRect _source = new IntRect(0, 0, 0, 0);

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Graphics.SpriteBatch"/> class.
		/// </summary>
		/// <param name="renderTarget">Render target.</param>
		public SpriteBatchService()
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

			if (source != null) 
			{
				_source.Left = (int)source.X;
				_source.Top = (int)source.Y;
				_source.Width = (int)source.Width;
				_source.Height = (int)source.Height;
			} 
			else 
			{
				_source.Left = 0;
				_source.Top = 0;
				_source.Width = (int)texture.Size.X;
				_source.Height = (int)texture.Size.Y;
			}
			_sprite.TextureRect = _source;

	        _sprite.Texture = texture;

			_position.X = destination.Position.X;
			_position.Y = destination.Position.Y;
			_sprite.Position = _position;

			_color.A = color.A;
			_color.B = color.B;
			_color.G = color.G;
			_color.R = color.R;
			_sprite.Color = _color;

			_sprite.Rotation = rotation;

			_origin.X = origin.X;
			_origin.Y = origin.Y;
			_sprite.Origin = _origin;

			_scale.X = destination.Width/_source.Width;
			_scale.Y = destination.Height/_source.Height;
			_sprite.Scale = _scale;

	        RenderTarget.Draw(_sprite);
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

			if (source != null) 
			{
				_source.Left = (int)source.X;
				_source.Top = (int)source.Y;
				_source.Width = (int)source.Width;
				_source.Height = (int)source.Height;
			}
			else 
			{
				_source.Left = 0;
				_source.Top = 0;
				_source.Width = (int)texture.Size.X;
				_source.Height = (int)texture.Size.Y;
			}
			_sprite.TextureRect = _source;

	        _sprite.Texture = texture;

			_position.X = position.X;
			_position.Y = position.Y;
			_sprite.Position = _position;

			_color.A = color.A;
			_color.B = color.B;
			_color.G = color.G;
			_color.R = color.R;
			_sprite.Color = _color;

	        _sprite.Rotation = rotation;

			_origin.X = origin.X;
			_origin.Y = origin.Y;
			_sprite.Origin = _origin;


			_scale.X = scale;
			_scale.Y = scale;
			_sprite.Scale = _scale;

	        RenderTarget.Draw(_sprite);
		}
	}
}

