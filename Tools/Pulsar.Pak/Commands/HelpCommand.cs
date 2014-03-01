using System;
using System.Linq;

namespace Pulsar.Pak
{
	/// <summary>
	/// Help command.
	/// </summary>
	public class HelpCommand : AbstractCommand
	{
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
		public override void Execute (string[] args)
		{
			var queryable = Process.Commands
				.OrderBy (c => c.Name)
				.AsQueryable ();
				
			if (args.Length == 1) {
				var commandName = args[0];
				queryable = queryable.Where(c => c.Name == commandName);
			}

			queryable
				.ToList()
				.ForEach(c => Console.WriteLine(string.Format("- {0} => {1}", c.Name, c.Description)));
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name 
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
		public override string Description 
		{
			get 
			{
				return "List all commands";
			}
		}
	}
}

