using System;

namespace Pulsar.Services
{
	/// <summary>
	/// Shape batch.
	/// </summary>
	public interface IShapeBatchService : IGraphicsBatchService
	{
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
		void DrawRectangle (Rectangle rectangle, Vector position, Color fillColor, Color borderColor, float rotation, Vector origin, float scale);
	}
}

