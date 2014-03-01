using System;
using System.IO;

namespace Pulsar.Pak
{
	/// <summary>
	/// List command.
	/// </summary>
	public class ListCommand : AbstractCommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.ListCommand"/> class.
		/// </summary>
		public ListCommand ()
		{

		}
			
		/// <summary>
		/// Execute the specified args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public override void Execute (string[] args)
		{
			if (args.Length != 1) 
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

			if (pak.Items.Count == 0)
				Console.WriteLine ("Empty package");

			foreach (var item in pak.Items) 
			{
				Console.WriteLine (string.Format("Resource -> [Key={0}];[File={1}];[Size={2}]", item.Key, item.FileName, item.ByteArray.Length));
			}

			Console.WriteLine ("List done");
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name 
		{
			get 
			{
				return "LIST";
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
				return "[Pak Path] : List resources from a pak file.";
			}
		}
	}
}

