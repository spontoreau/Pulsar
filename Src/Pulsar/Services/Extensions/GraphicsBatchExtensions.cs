using System;
using SFML.Graphics;

namespace Pulsar.Services
{
	/// <summary>
	/// Graphics batch extension
	/// </summary>
	public static class GraphicsBatchExtensions
	{
		/// <summary>
		/// Begin batching.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		/// <param name="view">View.</param>
		public static void Begin(this IGraphicsBatch batch, BlendMode blendMode, View view)
		{
			batch.Begin(blendMode, view.Viewport, view.Center, view.Size, view.Rotation);
		}

		/// <summary>
		/// Begin batching.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		public static void Begin(this IGraphicsBatch batch, BlendMode blendMode)
		{
			batch.Begin(blendMode, batch.RenderTarget.GetView());
		}

		/// <summary>
		/// Begin batching.
		/// </summary>
		public static void Begin(this IGraphicsBatch batch)
		{
			batch.Begin(BlendMode.Alpha);
		}
	}
}

