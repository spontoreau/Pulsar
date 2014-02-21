using System;
using Pulsar.Services.Implements.Graphics;

namespace Pulsar.Services
{
	/// <summary>
	/// Shape batch extensions.
	/// </summary>
	public static class ShapeBatchExtensions
	{
		/// <summary>
		/// Draws rectangle.
		/// </summary>
		/// <param name="rectangle">Rectangle.</param>
		/// <param name="position">Position.</param>
		/// <param name="fillColor">Fill color.</param>
		/// <param name="borderColor">Border color.</param>
		public static void DrawRectangle(this IShapeBatchService batch, Rectangle rectangle, Vector position, Color fillColor, Color borderColor)
		{
			batch.DrawRectangle (rectangle, position, fillColor, borderColor, 0, Vector.Zero, 0);
		}
	}
}

