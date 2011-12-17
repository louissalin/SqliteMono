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
	
	[Concern(typeof(ArgsParser))]
	public class when_running_the_app_with_proper_arguments : AppSpecBase
	{		
		[Spec]
		public void should_output_a_missing_first_name_error ()
		{
			var properties = new Dictionary<string, string>();
			properties["fn"] = "Louis";
			properties["ln"] = "Salin";
			properties["email"] = "louis.phil@gmail.com";
			properties["p"] = "pass123";
			
			var arguments = new AppArguments { Command = "add", Properties = properties };
			
			sut.Run(arguments);
			persister.SavedUser.FirstName.ShouldEqual("Louis");
			persister.SavedUser.LastName.ShouldEqual("Salin");
			persister.SavedUser.Email.ShouldEqual("louis.phil@gmail.com");
			persister.SavedUser.Password.ShouldEqual("pass123");
		}
	}
		
	public class AppSpecBase : ContextSpecification
	{		
		protected override void Context ()
		{
			output = new OutputStub();
			persister = new PersisterStub();
			sut = new App(output, persister);
		}
		
		protected App sut;
		protected IOutput output;
		protected PersisterStub persister;
	}
			
	public class PersisterStub : IUserPersister
	{
		public User SavedUser { get; private set; }
		
		public bool Save(User user)
		{
			SavedUser = user;
			return true;
		}
	}
}

