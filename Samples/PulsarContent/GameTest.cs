using System;
using Pulsar;
using SFML.Graphics;
using Pulsar.Graphics;

namespace PulsarContent
{
	public class GameTest : Game
	{
		private Texture Texture;

		public GameTest ()
			:base()
		{
			Content.RootDirectory = "Content";
		}

		protected override void LoadContent ()
		{
			var texture = Content.Load<Texture> ("pulsar.png");
			var texture2 = Content.Load<Texture> ("Test/vtek.png");

			Content.LazyLoading = true;
			var texture3 = Content.Load<Texture>("pulsar");
			Texture = Content.Load<Texture>("Test/vtek");

			base.LoadContent ();
		}

		protected override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);
		}

	}
}

