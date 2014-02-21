using System;
using Pulsar;
using Pulsar.Services;
using SFML.Graphics;

namespace PulsarContent
{
	/// <summary>
	/// Module test.
	/// </summary>
	public class ModuleTest : IModule, IDrawable
	{
		private Texture texture;

		/// <summary>
		/// Gets or sets the content service.
		/// </summary>
		/// <value>The content service.</value>
		public IContentService ContentService { get; set;}

		/// <summary>
		/// Gets or sets the I sprite batch service.
		/// </summary>
		/// <value>The I sprite batch service.</value>
		public ISpriteBatchService SpriteBatchService { get; set;}

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
			texture = ContentService.Load<Texture> ("Test/vtek");
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Update (GameTime gameTime)
		{

		}

		/// <summary>
		/// Draw this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Draw (GameTime gameTime)
		{
			SpriteBatchService.Begin ();
			SpriteBatchService.Draw (texture, Vector.Zero, Pulsar.Color.White);
			SpriteBatchService.End ();
		}
	}
}

