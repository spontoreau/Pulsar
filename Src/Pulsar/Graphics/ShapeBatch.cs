using System;
using SFML.Graphics;

//TODO complete implementation for ShapeBatcher
namespace Pulsar.Graphics
{
	/// <summary>
	/// Shape batcher.
	/// </summary>
	public class ShapeBatch : GraphicsBatch
	{
		/// <summary>
		/// The _rectangle.
		/// </summary>
		private readonly RectangleShape _rectangle = new RectangleShape();

		/// <summary>
		/// The _circle.
		/// </summary>
		private readonly CircleShape _circle = new CircleShape();

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Graphics.ShapeBatch"/> class.
		/// </summary>
		/// <param name="renderTarget">Render target.</param>
		public ShapeBatch(RenderTarget renderTarget)
			: base(renderTarget)
		{

		}        
	}
}

