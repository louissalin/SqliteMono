using System;

namespace Amplifier
{
	public class App
	{
		private IOutput output;
		private IUserPersister persister;
		
		public App() : this(new Output(), new UserPersister())
		{}
		
		public App(IOutput output, IUserPersister persister)
		{
			this.output = output;
			this.persister = persister;
		}
		
		public void Run(AppArguments arguments)
		{
			var firstname = GetFirstName(arguments);
			var lastname = GetLastName(arguments);
			var email = GetEmail(arguments);
			var password = GetPassword(arguments);
			
			var user = new User {FirstName = firstname,
								 LastName = lastname,
								 Email = email,
								 Password = password};
			
			persister.Save(user);
		}
		
		private string GetFirstName(AppArguments arguments)
		{
			var value = arguments.GetProperty("fn");
			if (string.IsNullOrEmpty(value))
			    output.WriteLine("error: no first name specified");
			
			return value;
		}
		
		private string GetLastName(AppArguments arguments)
		{
			var value = arguments.GetProperty("ln");
			if (string.IsNullOrEmpty(value))
			    output.WriteLine("error: no last name specified");
			
			return value;
		}
		
		private string GetEmail(AppArguments arguments)
		{
			var value = arguments.GetProperty("email");
			if (string.IsNullOrEmpty(value))
			    output.WriteLine("error: no email specified");
			
			return value;
		}
		
		private string GetPassword(AppArguments arguments)
		{
			var value = arguments.GetProperty("p");
			if (string.IsNullOrEmpty(value))
			    output.WriteLine("error: no password specified");
			
			return value;
		}
	}
	
	public class User 
	{
		public string FirstName { get; set;}
		public string LastName { get; set;}
		public string Email { get; set;}
		public string Password { get; set; }		
	}
}

