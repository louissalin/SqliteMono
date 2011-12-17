using System;

namespace Amplifier
{
	public interface IUserPersister 
	{
		bool Save(User user);
	}
	
	public class UserPersister : IUserPersister
	{
		public UserPersister ()
		{
		}
		
		public bool Save(User user)
		{
			return true;
		}
	}
}

