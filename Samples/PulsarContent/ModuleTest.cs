using System;
using Pulsar;
using Pulsar.Services;
using SFML.Graphics;
using Pulsar.Module;

namespace PulsarContent
{
	/// <summary>
	/// Module test.
	/// </summary>
	public class ModuleTest : IModule, IDrawable
	{
		private Texture texture;
		private Vector position;

		/// <summary>
		/// Gets or sets the content service.
		/// </summary>
		/// <value>The content service.</value>
		public IContentService ContentService { get; set;}

		/// <summary>
		/// Gets or sets the sprite batch service.
		/// </summary>
		/// <value>The I sprite batch service.</value>
		public ISpriteBatchService SpriteBatchService { get; set;}

		/// <summary>
		/// Gets or sets the window service.
		/// </summary>
		/// <value>The window service.</value>
		public IWindowService WindowService { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PulsarContent.ModuleTest"/> class.
		/// </summary>
		public ModuleTest ()
		{
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize ()
		{
			ContentService.RootDirectory = "Content";
			ContentService.LazyLoading = true;
			texture = ContentService.Load<Texture> ("Test/vtek");
			position = Vector.Zero;

			if (!WindowService.IsCreated)
				throw new Exception ("Window must be created");

			SpriteBatchService.RenderTarget = WindowService.Window;

			WindowService.Window.MouseMoved += (object sender, SFML.Window.MouseMoveEventArgs e) => {
				position.X = e.X; 
				position.Y = e.Y;
			};
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Update (GameTime gameTime)
		{
			//var tmp = this.TempData();
			//var glb = this.GlobalData();


		}

		/// <summary>
		/// Draw this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Draw (GameTime gameTime)
		{
			SpriteBatchService.Begin ();
			SpriteBatchService.Draw (texture, position, Pulsar.Color.White);
			SpriteBatchService.End ();
		}
	}
}

