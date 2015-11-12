using SQLite;

namespace TreeWatch
{
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public User (string firstName, string lastName, string username, string password, string email)
		{
			FirstName = firstName;
			LastName = lastName;
			UserName = username;
			Password = password;
			Email = email;
		}

		public User()
		{
		}
	}
}

