using System;
using System.IO;
class Program
{
    // MENUS
    static void TopHeader()
    {
        Console.WriteLine("**========== SMART CART =========**");
    }
    static void MainMenu()
    {
        Console.WriteLine("1. Sign Up");
        Console.WriteLine("2. Sign In");
        Console.WriteLine("3. Exit");
        Console.Write("Choice: ");
    }
    static void AdminMenu()
    {
        Console.WriteLine("************* ADMIN MENU ************");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View Products");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("5. User Management");
        Console.WriteLine("6. Logout");
        Console.Write("Choice: ");
    }
    static void CustomerMenu()
    {
        Console.WriteLine("=======--- CUSTOMER MENU ---=======");
        Console.WriteLine("1. View Products");
        Console.WriteLine("2. Search Product");
        Console.WriteLine("3. Logout");
        Console.Write("Choice: ");
    }
    // MAIN
    static void Main()
    {
        string[] usernames = new string[10];
        string[] passwords = new string[10];
        int userCount = LoadUsers(usernames, passwords);
        int loggedUser = -1;
        int[] productIDs = new int[50];
        string[] productNames = new string[50];
        int[] productPrices = new int[50];
        int[] productStock = new int[50];
        int productCount = LoadProducts(productIDs, productNames, productPrices, productStock);
        int choice = 0;
        while (choice != 3)
        {
            TopHeader();
            MainMenu();
            choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                userCount = SignUp(usernames, passwords, userCount);
                SaveUsers(usernames, passwords, userCount);
            }
            else if (choice == 2)
            {
                loggedUser = SignIn(usernames, passwords, userCount);
                if (loggedUser != -1)
                {
                    Console.WriteLine("1. Admin");
                    Console.WriteLine("2. Customer");
                    Console.Write("Choose Role: ");
                    int role = int.Parse(Console.ReadLine());
                    // ADMIN
                    if (role == 1)
                    {
                        int adminChoice = 0;
                        while (adminChoice != 6)
                        {
                            AdminMenu();
                            adminChoice = int.Parse(Console.ReadLine());
                            if (adminChoice == 1)
                            {
                                AddProduct(productIDs, productNames, productPrices, productStock, productCount);
                                productCount++;
                                SaveProducts(productIDs, productNames, productPrices, productStock, productCount);
                            }
                            else if (adminChoice == 2)
                                ViewProducts(productIDs, productNames, productPrices, productStock, productCount);
                            else if (adminChoice == 3)
                            {
                                UpdateProduct(productIDs, productNames, productPrices, productStock, productCount);
                                SaveProducts(productIDs, productNames, productPrices, productStock, productCount);
                            }
                         else if (adminChoice == 4)
                            {
                                productCount = DeleteProduct(productIDs, productNames, productPrices, productStock, productCount);
                                SaveProducts(productIDs, productNames, productPrices, productStock, productCount);
                            }
                            else if (adminChoice == 5)
                            {
                                int userChoice = 0;
                                while (userChoice != 5)
                                {
                                    Console.WriteLine("********** USER MANAGEMENT *********");
                                    Console.WriteLine("1. Add User");
                                    Console.WriteLine("2. View Users");
                                    Console.WriteLine("3. Update User");
                                    Console.WriteLine("4. Delete User");
                                    Console.WriteLine("5. Back");
                                    Console.Write("Choice: ");
                                    userChoice = int.Parse(Console.ReadLine());
                                    if (userChoice == 1)
                                    {
                                        AddUser(usernames, passwords, userCount);
                                        userCount++;
                                        SaveUsers(usernames, passwords, userCount);
                                    }
                                    else if (userChoice == 2)
                                        ViewUsers(usernames, passwords, userCount);
                                    else if (userChoice == 3)
                                    {
                                        UpdateUser(usernames, passwords, userCount);
                                        SaveUsers(usernames, passwords, userCount);
                                    }
                                    else if (userChoice == 4)
                                    {
                                        userCount = DeleteUser(usernames, passwords, userCount);
                                        SaveUsers(usernames, passwords, userCount);
                                    }
                                }
                            }
                        }
                    }
                    // CustmER
                    else if (role == 2)
                    {
                        int customerChoice = 0;
                        while (customerChoice != 3)
                        {
                            CustomerMenu();
                            customerChoice = int.Parse(Console.ReadLine());
                            if (customerChoice == 1)
                                ViewProducts(productIDs, productNames, productPrices, productStock, productCount);
                            else if (customerChoice == 2)
                            {
                                Console.Write("Enter Product ID: ");
                                int id = int.Parse(Console.ReadLine());
                                int index = SearchProduct(productIDs, productCount, id);
                                if (index != -1)
                                {
                                    Console.WriteLine("Name: " + productNames[index]);
                                    Console.WriteLine("Price: " + productPrices[index]);
                                    Console.WriteLine("Stock: " + productStock[index]);
                                }
                                else
                                    Console.WriteLine("Product not found");
                            }
                        }
                    }
                }
            }
            else if (choice == 3)
                Console.WriteLine("Program Closed");
        }
    }
    // USER FUNCTIONS
    static int SignUp(string[] usernames, string[] passwords, int count)
    {
        if (count >= 10) return count;
        Console.Write("Enter Username: ");
        usernames[count] = Console.ReadLine();
        Console.Write("Enter Password: ");
        passwords[count] = Console.ReadLine();
        Console.WriteLine("Account Created");
     return count + 1;
    }
    static int SignIn(string[] usernames, string[] passwords, int count)
    {
        Console.Write("Username: ");
        string u = Console.ReadLine();
        Console.Write("Password: ");
        string p = Console.ReadLine();
        for (int i = 0; i < count; i++)
        {
            if (usernames[i] == u && passwords[i] == p)
            {
                Console.WriteLine("Login Successful");
                return i;
            }
        }
        Console.WriteLine("Invalid Login");
        return -1;
    }
    static void AddUser(string[] usernames, string[] passwords, int count)
    {
        Console.Write("Username: ");
        usernames[count] = Console.ReadLine();
        Console.Write("Password: ");
        passwords[count] = Console.ReadLine();
        Console.WriteLine("User Added");
    }
    static void ViewUsers(string[] usernames, string[] passwords, int count)
    {
        for (int i = 0; i < count; i++)
           Console.WriteLine(usernames[i] + " " + passwords[i]);
    }
    static void UpdateUser(string[] usernames, string[] passwords, int count)
    {
        Console.Write("Username to update: ");
        string name = Console.ReadLine();
        for (int i = 0; i < count; i++)
        {
            if (usernames[i] == name)
            {
                Console.Write("New Password: ");
                passwords[i] = Console.ReadLine();
                Console.WriteLine("User Updated");
                return;
            }
        }
        Console.WriteLine("User not found");
    }
    static int DeleteUser(string[] usernames, string[] passwords, int count)
    {
        Console.Write("Username to delete: ");
        string name = Console.ReadLine();
        for (int i = 0; i < count; i++)
        {
            if (usernames[i] == name)
            {
                for (int j = i; j < count - 1; j++)
                {
                    usernames[j] = usernames[j + 1];
                    passwords[j] = passwords[j + 1];
                }
               Console.WriteLine("User Deleted");
                return count - 1;
            }
        }
        Console.WriteLine("User not found");
        return count;
    }
    // PRODUCT FUNCTIONS
    static void AddProduct(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.Write("Product ID: ");
        ids[count] = int.Parse(Console.ReadLine());
        Console.Write("Name: ");
        names[count] = Console.ReadLine();
        Console.Write("Price: ");
        prices[count] = int.Parse(Console.ReadLine());
        Console.Write("Stock: ");
        stock[count] = int.Parse(Console.ReadLine());
        Console.WriteLine("Product Added");
    }
    static void ViewProducts(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        for (int i = 0; i < count; i++)
            Console.WriteLine(ids[i] + " " + names[i] + " " + prices[i] + " " + stock[i]);
    }
    static int SearchProduct(int[] ids, int count, int id)
    {
        for (int i = 0; i < count; i++)
            if (ids[i] == id) return i;
        return -1;
    }
    static void UpdateProduct(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.Write("Product ID: ");
        int id = int.Parse(Console.ReadLine());
        int index = SearchProduct(ids, count, id);
        if (index == -1)
        {
            Console.WriteLine("Not found");
            return;
        }
        Console.Write("New Price: ");
        prices[index] = int.Parse(Console.ReadLine());
        Console.Write("New Stock: ");
        stock[index] = int.Parse(Console.ReadLine());
        Console.WriteLine("Product Updated");
    }
    static int DeleteProduct(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.Write("Product ID: ");
        int id = int.Parse(Console.ReadLine());
        int index = SearchProduct(ids, count, id);
        if (index == -1)
        {
            Console.WriteLine("Not found");
            return count;
        }
        for (int i = index; i < count - 1; i++)
        {
            ids[i] = ids[i + 1];
            names[i] = names[i + 1];
            prices[i] = prices[i + 1];
            stock[i] = stock[i + 1];
        }
        Console.WriteLine("Product Deleted");
        return count - 1;
    }
    // FILE HANDLING
    static int LoadUsers(string[] usernames, string[] passwords)
    {
        int count = 0;
        if (File.Exists("users.txt"))
        {
            foreach (string line in File.ReadAllLines("users.txt"))
            {
                string[] parts = line.Split(' ');
                usernames[count] = parts[0];
                passwords[count] = parts[1];
                count++;
            }
        }
        return count;
    }
    static void SaveUsers(string[] usernames, string[] passwords, int count)
    {
        using (StreamWriter file = new StreamWriter("users.txt"))
        {
            for (int i = 0; i < count; i++)
                file.WriteLine(usernames[i] + " " + passwords[i]);
        }
    }
    static int LoadProducts(int[] ids, string[] names, int[] prices, int[] stock)
    {
        int count = 0;
        if (File.Exists("products.txt"))
        {
            foreach (string line in File.ReadAllLines("products.txt"))
            {
                string[] parts = line.Split(' ');
                ids[count] = int.Parse(parts[0]);
                names[count] = parts[1];
                prices[count] = int.Parse(parts[2]);
                stock[count] = int.Parse(parts[3]);
                count++;
            }
        }

        return count;
    }
    static void SaveProducts(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        using (StreamWriter file = new StreamWriter("products.txt"))
        {
            for (int i = 0; i < count; i++)
                file.WriteLine(ids[i] + " " + names[i] + " " + prices[i] + " " + stock[i]);
        }
    }
}