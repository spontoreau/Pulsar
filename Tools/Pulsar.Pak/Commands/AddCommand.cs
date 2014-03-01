using System;
using System.IO;
using SFML.Graphics;

namespace Pulsar.Pak
{
	/// <summary>
	/// Add command.
	/// </summary>
	public class AddCommand : ICommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.AddCommand"/> class.
		/// </summary>
		public AddCommand ()
		{
		}
			
		/// <summary>
		/// Execute the specified args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void Execute (string[] args)
		{
			if (args.Length != 3) 
			{
				Console.WriteLine ("arguments unvalid number");
				Console.WriteLine ();
				Process.Handle (new string[1] { "HELP" });
				return;
			}

			var resPath = args [0];


			if (!File.Exists (resPath)) 
			{
				Console.WriteLine ("Resource file not exists");
				Console.WriteLine ();
				Process.Handle (new string[1] { "HELP" });
				return;
			}

			if (!(resPath.Contains (".png") || resPath.Contains ("jpg") || resPath.Contains ("bmp"))) 
			{
				Console.WriteLine ("Unsupport file type");
				Console.WriteLine ();
				Process.Handle (new string[1] { "HELP" });
			}

			var resKey = args [1];
			var pakName = args[2];
			var pakPath = "output/" + args[2] + Package.PackageFileExtension;

			Package pak;

			if (File.Exists (pakPath)) 
			{
				pak = Package.Load (pakPath);
			}
			else 
			{
				pak = new Package(pakName);
			}

			var isAdded = pak.Add (typeof(Texture), resKey, resPath);

			if (isAdded) 
			{
				Package.Save (pak, "output/");
				Console.WriteLine ("Resource {0} added to pak {1}", resPath, pakPath);
			} else 
			{
				Console.WriteLine ("Key {0} is define in pak {1}", resKey, pakPath);
			}
		}

		/// <summary>
		/// Gets or sets the process.
		/// </summary>
		/// <value>The process.</value>
		public CommandProcess Process { get; set;}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name 
		{
			get 
			{
				return "ADD";
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
				return "[Resource Path] [Resource Key] [Pak name] : Add resource to a pak file.";
			}
		}
	}
}

