using System;
using System.Collections.Generic;

namespace Amplifier
{
	public class ArgsParser
	{
		private IOutput output;
		
		public ArgsParser(IOutput output)
		{
			this.output = output;
		}
		
		public AppArguments Parse(string[] args)
		{
			if (args.Length == 0 || args[0].StartsWith("-")) 
			{
				output.WriteLine("error: no command specified");
				return null;
			}
			
			var arguments = new AppArguments { Command = args[0], Properties = new Dictionary<string, string>() };
			
			for (var i = 1; i < args.Length; i++)
			{
				if (!UpdateProperties(args[i].Substring(1), arguments.Properties))
					return null;
			}			
			
			return arguments;
		}		
		
		private bool UpdateProperties(string arg, Dictionary<string, string> properties)
		{
			var propertyName = GetPropertyName(arg);
			var value = GetValue(arg);
			
			if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(value))
			{
				output.WriteLine(string.Format("error: can't read argument {0}", arg));
				return false;
			}
			
			properties[propertyName] = value;
			return true;
		}
			    
		private string GetPropertyName(string arg)
		{
			var parts = arg.Split(':');
			if (parts.Length < 2) return "";
			
			return parts[0];
		}
		
		private string GetValue(string arg)
		{
			var parts = arg.Split(':');
			if (parts.Length < 2) return "";
			
			return parts[1];
		}
	}
	
	public class AppArguments
	{
		public string Command { get; set; }
		public Dictionary<string, string> Properties { get; set; }		
		
		public string GetProperty(string property)
		{
			return Properties.ContainsKey(property)
				? Properties[property]
				: "";
		}
	}	
}

