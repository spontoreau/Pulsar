using System;
using Pulsar;
using Pulsar.Services;
using SFML.Graphics;
using Pulsar.Module;
using PulsarColor = Pulsar.Color;
using SFML.Window;

namespace PulsarContent
{
	/// <summary>
	/// Module test.
	/// </summary>
	public class ModuleTest : GameModule, IDrawable
	{
		private Texture texture;
		private Texture texture2;
		private Font font;
		private Vector position;
		private Vector position2;
		private Vector textPosition;
		private Vector origin;
		private float rotation;
		private Rectangle source;
		private PulsarColor globalColor;


		/// <summary>
		/// Gets or sets the content service.
		/// </summary>
		/// <value>The content service.</value>
		public IContentService ContentService { get; set;}

		/// <summary>
		/// Gets or sets the sprite batch service.
		/// </summary>
		/// <value>The I sprite batch service.</value>
		public ISpriteBatchService SpriteBatchService { get; set; }

		/// <summary>
		/// Gets or sets the text batch service.
		/// </summary>
		/// <value>The text batch service.</value>
		public ITextBatchService TextBatchService { get; set; }

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
		public override void Initialize ()
		{
			ContentService.RootDirectory = "Content";
			ContentService.LazyLoading = true;
			texture = ContentService.Load<Texture> ("Test/vtek");
			texture2 = ContentService.Load<Texture>("pulsar_w");
			font = ContentService.Load<Font>("Font/OpenSans");
			position = Vector.Zero;
			position2 = new Vector (400, 300);
			textPosition = Vector.Zero;
			origin = new Vector(texture.Size.X / 2,texture.Size.Y / 2);
			source = new Rectangle (0, 0, 50, 50);
			globalColor = PulsarColor.White;

			if (!WindowService.IsCreated)
				throw new Exception ("Window must be created");

			SpriteBatchService.RenderTarget = WindowService.Window;
			TextBatchService.RenderTarget = WindowService.Window;

			WindowService.Window.MouseMoved += (object sender, SFML.Window.MouseMoveEventArgs e) => {
				var p = Mouse.GetPosition(WindowService.Window);
				position.X = p.X; 
				position.Y = p.Y;
			};
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
			rotation++;

			if (rotation > 360)
				rotation = 0;
		}

		/// <summary>
		/// Draw this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Draw (GameTime gameTime)
		{
			var fps = (int)GlobalData ["FPS"];

			SpriteBatchService.Begin();
			TextBatchService.Begin ();
			SpriteBatchService.Draw (texture, position, source, globalColor, rotation, origin, 0.5f);

			for(var i = 0; i < 100; i++)
				SpriteBatchService.Draw (texture2, position2, globalColor);

			TextBatchService.DrawString(font, fps.ToString(), textPosition, globalColor);
			SpriteBatchService.End ();
			TextBatchService.End ();
		}
	}
}

