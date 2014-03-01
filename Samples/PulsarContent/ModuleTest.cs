using System;
using Pulsar;
using Pulsar.Services;
using SFML.Graphics;
using Pulsar.Module;
using PulsarColor = Pulsar.Color;

namespace PulsarContent
{
	/// <summary>
	/// Module test.
	/// </summary>
	public class ModuleTest : IModule, IDrawable
	{
		private Texture texture;
		private Texture texture2;
		private Vector position;
		private Vector origin;
		private float rotation;
		private Rectangle source;


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
			texture2 = ContentService.Load<Texture>("pulsar_w");
			position = Vector.Zero;
			origin = new Vector(texture.Size.X / 2,texture.Size.Y / 2);
			source = new Rectangle (0, 0, 50, 50);

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
			rotation++;

			if (rotation > 360)
				rotation = 0;

			//var tmp = this.TempData();
			//var glb = this.GlobalData();
		}

		/// <summary>
		/// Draw this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Draw (GameTime gameTime)
		{
			SpriteBatchService.Begin();
			SpriteBatchService.Draw (texture, position, source, PulsarColor.White, rotation, origin, 0.5f);
			SpriteBatchService.Draw (texture2, Vector.Zero, PulsarColor.White);
			SpriteBatchService.End ();
		}
	}
}

