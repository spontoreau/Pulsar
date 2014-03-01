using System;
using System.Reflection;
using System.IO;

namespace Pulsar.Pak
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if(!Directory.Exists("output/"))
				Directory.CreateDirectory("output/");

			Console.WriteLine (string.Format("Pulsar Pak - v{0}", Assembly.GetExecutingAssembly().GetName().Version));
			Console.WriteLine ();

			var process = new CommandProcess ();
			process.Handle (args);

			Console.WriteLine ();
			Console.WriteLine ("Press any key to continue...");
			Console.ReadKey (true);
		}
	}
}
