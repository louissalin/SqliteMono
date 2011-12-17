using System;
using System.Collections.Generic;

using Amplifier;
using SpecUnit;

namespace Tests
{	
	[Concern(typeof(ArgsParser))]
	public class when_no_command_line_arguments_are_given : ArgsParserSpecBase
	{
		[Spec]
		public void should_output_an_error ()
		{
			var args = new List<string>().ToArray();
			sut.Parse(args);
			((OutputStub)output).Contains("error: no command specified").ShouldBeTrue();
		}
	}
	
	[Concern(typeof(ArgsParser))]
	public class when_missing_some_arguments : ArgsParserSpecBase
	{
		[Spec]
		public void should_output_a_missing_command_error ()
		{
			var args = new List<string>
			{
				"-fn:Louis",
				"-ln:Salin",
				"-email:louis.phil@gmail.com",
				"-p:pass123"
			};
			
			sut.Parse(args.ToArray());
			((OutputStub)output).Contains("error: no command specified").ShouldBeTrue();
		}
	}
	
	[Concern(typeof(ArgsParser))]
	public class when_arguments_are_not_well_formatted : ArgsParserSpecBase
	{
		[Spec]
		public void should_output_a_syntax_error_if_missing_colon ()
		{
			var args = new List<string>
			{
				"add",
				"-fnLouis"				
			};
			
			sut.Parse(args.ToArray());
			((OutputStub)output).Contains("error: can't read argument fnLouis").ShouldBeTrue();
		}
		
		[Spec]
		public void should_output_a_syntax_error_if_missing_value ()
		{
			var args = new List<string>
			{
				"add",
				"-fn:"				
			};
			
			sut.Parse(args.ToArray());
			((OutputStub)output).Contains("error: can't read argument fn:").ShouldBeTrue();
		}
	}
	
	[Concern(typeof(ArgsParser))]
	public class when_parsing_arguments : ArgsParserSpecBase
	{
		[Spec]
		public void should_return_proper_app_arguments ()
		{
			var args = new List<string>
			{
				"add",
				"-fn:Louis",
				"-ln:Salin",
				"-email:louis.phil@gmail.com",
				"-p:pass123"
			};
			
			var appArgs = sut.Parse(args.ToArray());
			appArgs.Command.ShouldEqual("add");
			appArgs.GetProperty("fn").ShouldEqual("Louis");
			appArgs.GetProperty("ln").ShouldEqual("Salin");
			appArgs.GetProperty("email").ShouldEqual("louis.phil@gmail.com");
			appArgs.GetProperty("p").ShouldEqual("pass123");
		}
	}
		
	public class ArgsParserSpecBase : ContextSpecification
	{		
		protected override void Context ()
		{
			output = new OutputStub();
			sut = new ArgsParser(output);
		}
		
		protected ArgsParser sut;
		protected IOutput output;
	}
			
	public class OutputStub : IOutput
	{
		public List<string> LinesWritten { get; private set; }
		
		public OutputStub()
		{
			LinesWritten = new List<string>();
		}
		
		public void WriteLine(string message)
		{
			LinesWritten.Add(message);
		}
		
		public bool Contains(string message)
		{		
			foreach (var line in LinesWritten)
			{
				if (line == message)
					return true;
			}
			
			return false;
		}
	}
}

