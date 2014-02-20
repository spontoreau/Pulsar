using System;

namespace Pulsar.Services.Implements.Content
{
	/// <summary>
	/// Content load exception.
	/// </summary>
	public sealed class ContentLoadException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Content.ContentLoadException"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		public ContentLoadException(string message)
			: base(message)
		{

		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Content.ContentLoadException"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="innerException">Inner exception.</param>
		public ContentLoadException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
	}
}

