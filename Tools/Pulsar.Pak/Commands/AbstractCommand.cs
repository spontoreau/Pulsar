using System;

namespace Pulsar.Pak
{
	/// <summary>
	/// Abstract command.
	/// </summary>
	public abstract class AbstractCommand : ICommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.AbstractCommand"/> class.
		/// </summary>
		public AbstractCommand ()
		{
		}

		/// <summary>
		/// Execute the specified args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public abstract void Execute (string[] args);

		/// <summary>
		/// Gets or sets the process.
		/// </summary>
		/// <value>The process.</value>
		public CommandProcess Process { get; set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public abstract string Name { get ; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public abstract string Description { get; }
	}
}

