using System;
using SFML.Graphics;
using Pulsar.Helpers;
using Pulsar.Services;

namespace Pulsar.Services.Implements.Graphics
{
	/// <summary>
	/// Shape batcher.
	/// </summary>
	public class ShapeBatchService : GraphicsBatchService, IShapeBatchService
	{
		/// <summary>
		/// The _rectangle.
		/// </summary>
		private readonly RectangleShape _rectangle = new RectangleShape();

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Graphics.ShapeBatch"/> class.
		/// </summary>
		/// <param name="renderTarget">Render target.</param>
		public ShapeBatchService()
		{

		}

		/// <summary>
		/// Draws the rectangle.
		/// </summary>
		/// <param name="rectangle">Rectangle.</param>
		/// <param name="position">Position.</param>
		/// <param name="fillColor">Fill color.</param>
		/// <param name="borderColor">Border color.</param>
		/// <param name="rotation">Rotation.</param>
		/// <param name="origin">Origin.</param>
		/// <param name="scale">Scale.</param>
		public void DrawRectangle(Rectangle rectangle, Vector position, Color fillColor, Color borderColor, float rotation, Vector origin, float scale)
		{
			var p = _rectangle.Position;
			p.X = position.X;
			p.Y = position.X;
			_rectangle.Position = p;

			var fc = _rectangle.FillColor;
			fc.A = fillColor.A;
			fc.B = fillColor.B;
			fc.G = fillColor.G;
			fc.R = fillColor.R;
			_rectangle.FillColor = fc;

			var oc = _rectangle.OutlineColor;
			oc.A = fillColor.A;
			oc.B = fillColor.B;
			oc.G = fillColor.G;
			oc.R = fillColor.R;
			_rectangle.FillColor = oc;

			_rectangle.Rotation = MathHelper.ToDegrees(rotation);

			var o = _rectangle.Origin;
			o.X = origin.X;
			o.Y = origin.Y;
			_rectangle.Origin = o;

			var s = _rectangle.Scale;
			s.X = scale;
			s.Y = scale;
			_rectangle.Scale = s;

			RenderTarget.Draw(_rectangle);
		}
	}
}

