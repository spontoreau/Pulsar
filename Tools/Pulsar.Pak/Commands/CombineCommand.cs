using System;
using System.IO;
using SFML.Graphics;
using SFML.Audio;

namespace Pulsar.Pak
{
	/// <summary>
	/// Combine command.
	/// </summary>
	public class CombineCommand : AbstractCommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.CombineCommand"/> class.
		/// </summary>
		public CombineCommand ()
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

			var resDirectory = args [0];
			var pakName = args [1];

			if (!Directory.Exists (resDirectory)) 
			{
				Console.WriteLine ("Directory not exists");
				return;
			}

			var pak = new Package (pakName);

			foreach (var file in Directory.GetFiles(resDirectory)) 
			{
				var extensions = Path.GetExtension (file).ToLower();
				var key = Path.GetFileName (file);

				if (extensions == ".png" || extensions == ".jpg" || extensions == ".bmp") 
				{
					var isAdded = pak.Add (typeof(Texture), key, file);

					if(isAdded)
						Console.WriteLine (string.Format("Texture {0} added", file));

				} 
				else if (extensions == "ttf" || extensions == "otf") 
				{
					var isAdded = pak.Add (typeof(Font), key, file);

					if(isAdded)
						Console.WriteLine (string.Format("Font {0} added", file));
				} 
				else if (extensions == "wav") 
				{
					var isAdded = pak.Add (typeof(SoundBuffer), key, file);

					if(isAdded)
						Console.WriteLine (string.Format("Font {0} added", file));
				} 
				else 
				{
					Console.WriteLine (string.Format("Unsupport file type : {0}", file));
				}
			}

			Package.Save (pak, resDirectory);
			Console.WriteLine ("Resources added to pak {0}", pak.Name);
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name 
		{
			get 
			{
				return "COMBINE";
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
				return "[Resource directory] [Pak name] : Combine all resources from a directory into a pak"; 
			}
		}
	}
}

