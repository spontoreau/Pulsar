using System;

namespace Pulsar
{
	/// <summary>
	/// Game time.
	/// </summary>
	public sealed class GameTime
	{
		/// <summary>
		/// Gets the total game time.
		/// </summary>
		/// <value>The total game time.</value>
		public TimeSpan TotalGameTime { get; internal set; }

		/// <summary>
		/// Gets the elapsed game time.
		/// </summary>
		/// <value>The elapsed game time.</value>
		public TimeSpan ElapsedGameTime { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.GameTime"/> class.
		/// </summary>
		public GameTime()
			:this(TimeSpan.Zero, TimeSpan.Zero)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.GameTime"/> class.
		/// </summary>
		/// <param name="totalGameTime">Total game time.</param>
		/// <param name="elapsedGameTime">Elapsed game time.</param>
		public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
		{
			this.TotalGameTime = totalGameTime;
			this.ElapsedGameTime = elapsedGameTime;
		}
	}
}

