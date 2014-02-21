using System;
using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Services
{
	/// <summary>
	/// Graphics batcher.
	/// </summary>
	public interface IGraphicsBatchService
	{
		/// <summary>
		/// Gets or sets the render target.
		/// </summary>
		/// <value>The render target.</value>
		RenderTarget RenderTarget { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance has begin.
		/// </summary>
		/// <value><c>true</c> if this instance has begin; otherwise, <c>false</c>.</value>
		bool HasBegin { get; }

		/// <summary>
		/// Begin batching.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		/// <param name="bounds">Bounds.</param>
		/// <param name="center">Center.</param>
		/// <param name="size">Size.</param>
		/// <param name="rotation">Rotation.</param>
		void Begin(BlendMode blendMode, FloatRect bounds, Vector2f center, Vector2f size, float rotation);

		/// <summary>
		/// End batching.
		/// </summary>
		void End ();
	}
}

