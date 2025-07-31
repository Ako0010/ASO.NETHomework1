
using UsersDatabaseHomework;


while (true)
{

    Console.WriteLine("\n1. Sign Up");
    Console.WriteLine("2. Sign In");
    Console.WriteLine("3. Exit");
    Console.Write("Choose option: ");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        User.SignUp();
        break;
    }
    else if (choice == "2")
    {
        User.SignIn();
        break;
    }
    else if (choice == "3")
        break;
    else
        Console.WriteLine("Invalid choice.");
}

