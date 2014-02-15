using System;
using Pulsar;
using SFML.Graphics;

namespace PulsarContent
{
	public class GameTest : Game
	{
		public GameTest ()
			:base()
		{
			Content.RootDirectory = "Content";
		}

		protected override void LoadContent ()
		{
			var texture = Content.Load<Texture> ("pulsar.png");


			base.LoadContent ();
		}

		protected override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);
		}

	}
}

