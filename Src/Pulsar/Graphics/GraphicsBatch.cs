using SFML.Graphics;
using SFML.Window;

namespace Pulsar.Graphics
{
	/// <summary>
	/// Graphics batcher.
	/// </summary>
	public abstract class GraphicsBatch
	{
		/// <summary>
		/// The _view.
		/// </summary>
		private readonly View _view = new View();

		/// <summary>
		/// The render target.
		/// </summary>
		protected RenderTarget RenderTarget;

		/// <summary>
		/// The states.
		/// </summary>
		protected RenderStates States = RenderStates.Default;

		/// <summary>
		/// Gets a value indicating whether this instance has begin.
		/// </summary>
		/// <value><c>true</c> if this instance has begin; otherwise, <c>false</c>.</value>
		public bool HasBegin { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Graphics.GraphicsBatch"/> class.
		/// </summary>
		/// <param name="renderTarget">Render target.</param>
		protected GraphicsBatch(RenderTarget renderTarget)
		{
			RenderTarget = renderTarget;
			_view = renderTarget.GetView();
			WindowContext.Created += WindowContext_Created;
		}

		/// <summary>
		/// Begin batching.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		/// <param name="bounds">Bounds.</param>
		/// <param name="center">Center.</param>
		/// <param name="size">Size.</param>
		/// <param name="rotation">Rotation.</param>
		public void Begin(BlendMode blendMode, FloatRect bounds, Vector2f center, Vector2f size, float rotation)
		{
			States.BlendMode = blendMode;
			_view.Reset(bounds);
			_view.Center = center;
			_view.Size = size;
			_view.Rotate(rotation);
			RenderTarget.SetView(_view);
			HasBegin = true;
		}

		/// <summary>
		/// Begin batching.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		/// <param name="view">View.</param>
		public void Begin(BlendMode blendMode, View view)
		{
			Begin(blendMode, view.Viewport, view.Center, view.Size, view.Rotation);
		}

		/// <summary>
		/// Begin batching.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		public void Begin(BlendMode blendMode)
		{
			Begin(blendMode, RenderTarget.GetView());
		}

		/// <summary>
		/// Begin batching.
		/// </summary>
		public void Begin()
		{
			Begin(BlendMode.Alpha);
		}

		/// <summary>
		/// End batching.
		/// </summary>
		public void End()
		{
			HasBegin = false;
		}

		/// <summary>
		/// Windows created.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void WindowContext_Created(object sender, System.EventArgs e)
		{
			RenderTarget = WindowContext.Window;
		}
	}
}

