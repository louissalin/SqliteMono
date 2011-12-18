using System;
using System.Data;
using System.Data.Common;
using System.IO;

using Mono.Data.Sqlite;

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
			try
			{
				using (var connection = new SqliteConnection("URI=file:Users.db3")) 
				{
					ExecuteQuery(GetQuery(user));
				}
			}
			catch (Exception) 
			{
				return false;
			}
			
			return true;
		}
		
		private string GetQuery(User user)
		{
			string query = "";
			bool userFound = false;
			ExecuteReaderQuery(string.Format("SELECT COUNT(*) AS Count FROM Users WHERE Email='{0}'", user.Email),
			                   reader => 
			                   {
								   reader.Read(); 
								   userFound = int.Parse(reader["Count"].ToString()) > 0; 
							   });
			
			if (userFound)
				query = string.Format("UPDATE Users SET FirstName='{0}', LastName='{1}', Password='{2}' WHERE Email='{3}'",
				                      user.FirstName, user.LastName, user.Password, user.Email);
			else
				query = string.Format("INSERT INTO Users VALUES ('{0}', '{1}', '{2}', '{3}')", 
				                	  user.FirstName, user.LastName, user.Email, user.Password);
			
			return query;
		}
		
		private void ExecuteReaderQuery(string query, Action<IDataReader> action)
		{
			using (var connection = GetConnection()) 
			{				
				using (var cmd = connection.CreateCommand())
				{
					connection.Open();
                	cmd.CommandText = query;
					
					using (var reader = cmd.ExecuteReader())
                	{
						action(reader);						
						reader.Close();
					}
					
					connection.Close();
				}				
			}
		}
		
		private void ExecuteQuery(string query)
		{
			using (var connection = GetConnection()) 
			{				
				using (var cmd = connection.CreateCommand())
				{
					connection.Open();
                	cmd.CommandText = query;
					
					cmd.ExecuteNonQuery();               						
					connection.Close();
				}				
			}
		}
		
		private SqliteConnection GetConnection()
		{
			return new SqliteConnection("URI=file:Users.db3");
		}
	}
}

