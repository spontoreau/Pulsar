using System;
using Pulsar;

namespace PulsarContent
{
	/// <summary>
	/// Fps module.
	/// </summary>
	public class FpsModule : GameModule
	{
		private double elapsedTmp;

		/// <summary>
		/// Initializes a new instance of the <see cref="PulsarContent.FpsModule"/> class.
		/// </summary>
		public FpsModule ()
		{
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public override void Initialize ()
		{
			GlobalData.Add("FPS", 0);
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
			elapsedTmp += gameTime.ElapsedGameTime.TotalMilliseconds;

			if (elapsedTmp >= 1000.0)
			{
				GlobalData["FPS"] = (int)(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);
				elapsedTmp = 0.0;
			}
		}
	}
}

