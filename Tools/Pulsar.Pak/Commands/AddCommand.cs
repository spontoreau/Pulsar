using System;
using System.IO;
using SFML.Graphics;
using SFML.Audio;

namespace Pulsar.Pak
{
	/// <summary>
	/// Add command.
	/// </summary>
	public class AddCommand : AbstractCommand
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
		public override void Execute (string[] args)
		{
			if (args.Length != 3) 
			{
				Console.WriteLine ("arguments unvalid number :");
				Process.Handle (new string[2] { "HELP", Name });
				return;
			}

			var resPath = args[0];

			if (!File.Exists (resPath)) 
			{
				Console.WriteLine ("Resource file not exists");
				return;
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
				
			var isAdded = false;
			var extensions = Path.GetExtension (resPath).ToLower();

			if (extensions == ".png" || extensions == ".jpg" || extensions == ".bmp") 
			{
				isAdded = pak.Add (typeof(Texture), resKey, resPath);

			} 
			else if (extensions == "ttf" || extensions == "otf") 
			{
				isAdded = pak.Add (typeof(Font), resKey, resPath);
			} 
			else if (extensions == "wav") 
			{
				isAdded = pak.Add (typeof(SoundBuffer), resKey, resPath);
			} 
			else 
			{
				Console.WriteLine (string.Format("Unsupport file type : {0}", resPath));
				return;
			}

			if (isAdded) 
			{
				Package.Save (pak, "output/");
				Console.WriteLine ("Resource {0} added to pak {1}", resPath, pakPath);
			} 
			else 
			{
				Console.WriteLine ("Key {0} is define in pak {1}", resKey, pakPath);
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
				return "ADD";
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
				return "[Resource Path] [Resource Key] [Pak name] : Add resource to a pak file.";
			}
		}
	}
}

