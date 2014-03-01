using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Pulsar.Pak
{
	/// <summary>
	/// Command process.
	/// </summary>
	public class CommandProcess
	{
		/// <summary>
		/// Gets or sets the commands.
		/// </summary>
		/// <value>The commands.</value>
		public List<ICommand> Commands { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.CommandProcess"/> class.
		/// </summary>
		public CommandProcess ()
		{
			Commands = new List<ICommand>();
			LoadCommands();
		}

		/// <summary>
		/// Handle a command
		/// </summary>
		/// <param name="command">Command to handle</param>
		/// <returns>Command process response</returns>
		public void Handle(string[] args)
		{
			if (args.Length == 0) 
			{
				Console.WriteLine("Empty command");
				Console.WriteLine();

				var help = (from c in Commands
				                where c is HelpCommand
				                select c).FirstOrDefault ();

				if (help != null)
					help.Execute (args);

				return;
			}

			string name = args[0];

			var iCommand = (from c in Commands where c.Name.ToUpper() == name.ToUpper() select c).FirstOrDefault();

			if (iCommand == null)
			{
				var help = (from c in Commands
					where c is HelpCommand
					select c).FirstOrDefault ();

				if (help != null)
					help.Execute (args);

				return;
			}

			var commandArgs = new string[args.Length - 1];
			Array.Copy (args, 1, commandArgs, 0, args.Length - 1);
			iCommand.Execute(commandArgs);
		}

		/// <summary>
		/// Loads the commands.
		/// </summary>
		private void LoadCommands()
		{
			var type = typeof(ICommand);

			foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (!t.IsAbstract && type.IsAssignableFrom(t))
				{
					var c = (ICommand)Activator.CreateInstance(t);
					c.Process = this;
					Commands.Add (c);
				}
			}
		}
	}
}

