
using Microsoft.Data.SqlClient;

namespace UsersDatabaseHomework;


public class User
{
   public SqlConnection sqlConnection = default!;

   static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Userss;Trusted_Connection=True;";

    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public bool Gender { get; set; }

    static public void SignUp()
    {
        User user = new User();

        Console.Write("Username: ");
        user.UserName = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand($"SELECT * FROM Users WHERE UserName = '{user.UserName}'", connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine("Username already exists!");
                    return;
                }
            }

            Console.Write("Password: ");
            user.Password = Console.ReadLine();

            Console.Write("First Name: ");
            user.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            user.LastName = Console.ReadLine();

            Console.Write("Age: ");
            user.Age = int.Parse(Console.ReadLine());

            Console.Write("Gender (M/F): ");
            string genderInput;
            do
            {
                genderInput = Console.ReadLine().ToUpper();
                if (genderInput != "M" && genderInput != "F")
                    Console.Write("Please enter only M or F: ");
            } while (genderInput != "M" && genderInput != "F");
            user.Gender = genderInput == "M";
            user.Gender = Console.ReadLine().ToUpper() == "M";

            string insertQuery = @$"INSERT INTO Users (UserName, Password, FirstName, LastName, Age, Gender) 
                                VALUES ('{user.UserName}', '{user.Password}', '{user.FirstName}', '{user.LastName}', {user.Age}, {(user.Gender ? 1 : 0)})";

            using (new SqlCommand(insertQuery, connection))
            {
                var command = new SqlCommand(insertQuery, connection);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("User registered successfully.");
                else
                    Console.WriteLine("Registration failed.");
            }
        }
    }

    static public void SignIn()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand($"SELECT * FROM Users", connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine();
                    Console.WriteLine($"Welcome, {reader["FirstName"]} {reader["LastName"]}!");
                    
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                }
            }
        }
    }
}
