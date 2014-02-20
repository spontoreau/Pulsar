using SFML.Graphics;
using SFML.Window;
using Pulsar.Services;

namespace Pulsar.Services.Implements.Graphics
{
	/// <summary>
	/// Graphics batcher.
	/// </summary>
	public abstract class GraphicsBatchService : IGraphicsBatchService
	{
		/// <summary>
		/// The _view.
		/// </summary>
		private View _view = new View();

		private RenderTarget _renderTarget;

		/// <summary>
		/// The render target.
		/// </summary>
		public RenderTarget RenderTarget
		{
			get
			{
				return _renderTarget;
			}
			set
			{
				_renderTarget = value;
				_view = _renderTarget.GetView();
				//WindowContext.Created += WindowContext_Created;
			}
		}

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
		protected GraphicsBatchService()
		{

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
			//RenderTarget = WindowContext.Window;
		}
	}
}

