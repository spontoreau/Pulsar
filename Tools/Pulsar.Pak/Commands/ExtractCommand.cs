using System;
using System.IO;

namespace Pulsar.Pak
{
	/// <summary>
	/// Extract command.
	/// </summary>
	public class ExtractCommand : AbstractCommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Pak.ExtractCommand"/> class.
		/// </summary>
		public ExtractCommand ()
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

			if (Directory.Exists (pak.Name)) 
			{
				Directory.Delete (pak.Name, true);
			}

			Directory.CreateDirectory (pak.Name);

			foreach (var item in pak.Items) 
			{
				Console.WriteLine (string.Format("Extract file {0}", item.FileName));

				var filePath = string.Format("{0}/{1}", pak.Name, item.FileName);
				File.WriteAllBytes (filePath, item.ByteArray);
			}

			Console.WriteLine ("Extract done");
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name 
		{
			get 
			{
				return "EXTRACT";
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
				return "[Pak Path] : Extract resources from a pak file.";
			}
		}
	}
}

