using System;
using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Services
{
	/// <summary>
	/// Graphics batcher.
	/// </summary>
	public interface IGraphicsBatch
	{
		/// <summary>
		/// Gets the render traget.
		/// </summary>
		/// <value>The render traget.</value>
		RenderTarget RenderTarget { get; }

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

