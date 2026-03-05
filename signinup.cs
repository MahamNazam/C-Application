using System;
class Program
{
    static string[] userNames = new string[50];
    static string[] userPasswords = new string[50];
    static int userCount = 0;
    static int loggedInUser = -1;
    static void Main(string[] args)
    {
        int choice = 0;
        while (choice != 4)
        {
            TopHeader();
            MainMenu();
            Console.Write("Enter choice: ");
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input! Enter number between 1 and 4.");
                continue;
            }
            if (choice == 1)
            {
                SignUp();
            }
            else if (choice == 2)
            {
                loggedInUser = SignIn();
            }
            else if (choice == 3)
            {
                if (loggedInUser != -1)
                {
                    SignOut();
                    loggedInUser = -1;
                }
                else
                {
                    Console.WriteLine("No user is currently logged in.");
                }
            }
            else if (choice == 4)
            {
                Console.WriteLine("Exiting program");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            Console.WriteLine();
        }
    }
    static void TopHeader()
    {
        Console.WriteLine("===== LOGIN SYSTEM =====");
    }
    static void MainMenu()
    {
        Console.WriteLine("1. Sign Up");
        Console.WriteLine("2. Sign In");
        Console.WriteLine("3. Sign Out");
        Console.WriteLine("4. Exit");
    }
    static void SignUp()
    {
        if (userCount >= 10)
        {
            Console.WriteLine("User limit reached.");
            return;
        }
        Console.Write("Enter username: ");
        string name = Console.ReadLine();
        // Check if username already exists
        for (int i = 0; i < userCount; i++)
        {
            if (userNames[i] == name)
            {
                Console.WriteLine("Username already exists.");
                return;
            }
        }
        Console.Write("Enter password: ");
        string password = Console.ReadLine();
        userNames[userCount] = name;
        userPasswords[userCount] = password;
        userCount++;
        Console.WriteLine("Sign up successful.");
    }
    static int SignIn()
    {
        if (userCount == 0)
        {
            Console.WriteLine("No users registered.");
            return -1;
        }
        Console.Write("Enter username: ");
        string name = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();
        for (int i = 0; i < userCount; i++)
        {
            if (userNames[i] == name && userPasswords[i] == password)
            {
                Console.WriteLine("Welcome " + name);
                return i;
            }
        }
        Console.WriteLine("Invalid login details.");
        return -1;
    }
    static void SignOut()
    {
        Console.WriteLine("Signed out successfully.");
    }
}