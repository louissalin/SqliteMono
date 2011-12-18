using System;
using System.Data;
using System.Data.Common;
using System.IO;

using Amplifier;
using Mono.Data.Sqlite;
using NUnit.Framework;
using SpecUnit;

namespace IntegrationTest
{
	[TestFixture]
	public class UserPersisterTest : ContextSpecification
	{
		[Test]
		public void SaveUserTest()
		{
			var sut = new UserPersister();
			var user = new User { Email = "louis.phil@gmail.com", FirstName = "Louis",
								  LastName = "Salin", Password = "pass123" };
			
			sut.Save(user);
			
			using (var connection = new SqliteConnection("URI=file:Users.db3")) 
			{
				var query = "SELECT * FROM Users WHERE Email='louis.phil@gmail.com';";
				
				using (var cmd = connection.CreateCommand())
				{
					connection.Open();
                	cmd.CommandText = query;
					
					using (var reader = cmd.ExecuteReader())
                	{
						reader.Read();
						
						reader["FirstName"].ShouldEqual("Louis");
						reader["LastName"].ShouldEqual("Salin");
						reader["Email"].ShouldEqual("louis.phil@gmail.com");
						reader["Password"].ShouldEqual("pass123");
						
						reader.Close();
					}
					
					connection.Close();
				}				
			}
		}
		
		[Test]
		public void UpdateUserTest()
		{
			var sut = new UserPersister();
			var user1 = new User { Email = "louis.phil@gmail.com", FirstName = "Louis",
								   LastName = "Salin", Password = "pass123" };
			
			var user2 = new User { Email = "louis.phil@gmail.com", FirstName = "NewFirstName",
								   LastName = "NewLastName", Password = "NewPassword" };
			
			sut.Save(user1);
			sut.Save(user2);
			
			using (var connection = new SqliteConnection("URI=file:Users.db3")) 
			{
				var query = "SELECT * FROM Users WHERE Email='louis.phil@gmail.com';";
				
				using (var cmd = connection.CreateCommand())
				{
					connection.Open();
                	cmd.CommandText = query;
					
					using (var reader = cmd.ExecuteReader())
                	{
						reader.Read();
						
						reader["FirstName"].ShouldEqual("NewFirstName");
						reader["LastName"].ShouldEqual("NewLastName");
						reader["Email"].ShouldEqual("louis.phil@gmail.com");
						reader["Password"].ShouldEqual("NewPassword");
						
						reader.Close();
					}
					
					connection.Close();
				}				
			}
		}
		
		protected override void Context ()
		{
			File.Copy("../../../DB/Users.db3", "./Users.db3", true);
		}
		
		protected override void CleanUpContext ()
		{
			//File.Delete("./User.db3");
		}
	}
}

