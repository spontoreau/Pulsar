using System;
using System.Linq;

namespace Pulsar.Pak
{
	/// <summary>
	/// Help command.
	/// </summary>
	public class HelpCommand : ICommand
	{
		/// <summary>
		/// Gets or sets the process.
		/// </summary>
		/// <value>The process.</value>
		public CommandProcess Process { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.HelpCommand"/> class.
		/// </summary>
		public HelpCommand ()
		{
		}

		/// <summary>
		/// Execute the specified args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void Execute (string[] args)
		{
			Process.Commands
				.OrderBy(c => c.Name)
				.ToList()
				.ForEach(c => Console.WriteLine(string.Format("- {0} => {1}", c.Name, c.Description)));
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name 
		{
			get 
			{
				return "HELP";
			}
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description 
		{
			get 
			{
				return "List all commands";
			}
		}
	}
}

