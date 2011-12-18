using System;

namespace Amplifier
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Amplifier User Manager.");
			Console.WriteLine ("usage: amp-user add -fn:Louis -ln:Salin -email:louis.phil@gmail.com -p:pass123");
			
			var parser = new ArgsParser();
			var arguments = parser.Parse(args);
			
			if (arguments == null)
				return;
			
			var app = new App();
			app.Run(arguments);
			
			Console.WriteLine ("Done!");
		}
	}	
}
