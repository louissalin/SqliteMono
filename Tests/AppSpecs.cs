using System;
using System.Dynamic;
using System.Collections.Generic;

using Amplifier;
using SpecUnit;

namespace Tests
{		
	[Concern(typeof(ArgsParser))]
	public class when_running_the_app_without_required_arguments : AppSpecBase
	{		
		[Spec]
		public void should_output_a_missing_first_name_error ()
		{
			var properties = new Dictionary<string, string>();
			properties["ln"] = "Salin";
			properties["email"] = "louis.phil@gmail.com";
			properties["p"] = "pass123";
			
			var arguments = new AppArguments { Command = "add", Properties = properties };
			
			sut.Run(arguments);
			((OutputStub)output).Contains("error: no first name specified").ShouldBeTrue();
		}
		
		[Spec]
		public void should_output_a_missing_last_name_error ()
		{
			var properties = new Dictionary<string, string>();
			properties["fn"] = "Louis";
			properties["email"] = "louis.phil@gmail.com";
			properties["p"] = "pass123";
			
			var arguments = new AppArguments { Command = "add", Properties = properties };
			
			sut.Run(arguments);
			((OutputStub)output).Contains("error: no last name specified").ShouldBeTrue();
		}
		
		[Spec]
		public void should_output_a_missing_email_error ()
		{
			var properties = new Dictionary<string, string>();
			properties["fn"] = "Louis";
			properties["ln"] = "Salin";
			properties["p"] = "pass123";
			
			var arguments = new AppArguments { Command = "add", Properties = properties };
			
			sut.Run(arguments);
			((OutputStub)output).Contains("error: no email specified").ShouldBeTrue();
		}
		
		[Spec]
		public void should_output_a_missing_password_error ()
		{
			var properties = new Dictionary<string, string>();
			properties["fn"] = "Louis";
			properties["ln"] = "Salin";
			properties["email"] = "louis.phil@gmail.com";
			
			var arguments = new AppArguments { Command = "add", Properties = properties };
			
			sut.Run(arguments);
			((OutputStub)output).Contains("error: no password specified").ShouldBeTrue();
		}
	}	
		
	public class AppSpecBase : ContextSpecification
	{		
		protected override void Context ()
		{
			output = new OutputStub();
			sut = new App(output);
		}
		
		protected App sut;
		protected IOutput output;
	}
			
	
}

