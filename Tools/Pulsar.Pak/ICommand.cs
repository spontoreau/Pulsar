using System;

namespace Pulsar.Pak
{
	/// <summary>
	/// Command.
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// Gets or sets the process.
		/// </summary>
		/// <value>The process.</value>
		CommandProcess Process { get; set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		string Description { get; }

		/// <summary>
		/// Execute the specified args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		void Execute(string[] args);
	}
}

