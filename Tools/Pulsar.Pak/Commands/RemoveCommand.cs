using System;
using System.Linq;
using System.IO;

namespace Pulsar.Pak
{
	/// <summary>
	/// Remove command.
	/// </summary>
	public class RemoveCommand : AbstractCommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.RemoveCommand"/> class.
		/// </summary>
		public RemoveCommand ()
		{
		}

		/// <summary>
		/// Execute the specified args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public override void Execute (string[] args)
		{
			if (args.Length != 2) 
			{
				Console.WriteLine ("arguments unvalid number :");
				Process.Handle (new string[2] { "HELP", Name });
				return;
			}

			var pakPath = args [0];

			if (!File.Exists(pakPath)) 
			{
				Console.WriteLine ("Pak file not exists");
				return;
			}

			var pak = Package.Load(pakPath);
			var key = args [1];
			var item = pak.Items.Where (c => c.Key == key).FirstOrDefault ();

			if (item != null) {
				pak.Items.Remove (item);
				var path = pakPath.Replace(pak.FileName, string.Empty);
				Package.Save (pak, path);

				Console.WriteLine (string.Format ("Resource {0} removed", item.Key));
			}
			else 
			{
				Console.WriteLine (string.Format ("Resource {0} not found", key));
			}
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name 
		{
			get 
			{
				return "REMOVE";
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
				return "[Pak Path] [Resource Key] : Remove a resource from a pak file.";
			}
		}
	}
}

