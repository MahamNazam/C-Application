using System;

class Program
{
    // Function prototypes
    static void TopHeader() { Console.WriteLine("         SMART CART        "); }

    static void MainMenu()
    {
        Console.WriteLine("1. Sign Up");
        Console.WriteLine("2. Sign In");
        Console.WriteLine("4. Exit");
        Console.Write("Choice: ");
    }

    static void AdminMenu()
    {
        Console.WriteLine("      ADMIN MENU      ");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View Products");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("6. Logout");
        Console.Write("Choice: ");
    }

    static void CustomerMenu()
    {
        Console.WriteLine("       CUSTOMER MENU     ");
        Console.WriteLine("1. View Products");
        Console.WriteLine("2. Search Product");
        Console.WriteLine("3. Logout");
        Console.Write("Choice: ");
    }

    // Sign Up
    static int SignUp(string[] usernames, string[] passwords, int count)
    {
        if (count >= 10)
        {
            Console.WriteLine("User limit reached");
            return count;
        }

        Console.Write("Enter Username: ");
        string name = Console.ReadLine();

        for (int i = 0; i < count; i++)
        {
            if (usernames[i] == name)
            {
                Console.WriteLine("Username already exists");
                return count;
            }
        }

        Console.Write("Enter Password: ");
        string pass = Console.ReadLine();

        usernames[count] = name;
        passwords[count] = pass;

        Console.WriteLine("Account Created");
        return count + 1;
    }

    // Sign In
    static int SignIn(string[] usernames, string[] passwords, int count)
    {
        Console.Write("Username: ");
        string name = Console.ReadLine();

        Console.Write("Password: ");
        string pass = Console.ReadLine();

        for (int i = 0; i < count; i++)
        {
            if (usernames[i] == name && passwords[i] == pass)
            {
                Console.WriteLine("Login Successful");
                return i;
            }
        }

        Console.WriteLine("Invalid Login");
        return -1;
    }

    // Add Product
    static void AddProduct(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.Write("Enter Product ID: ");
        int id = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            if (ids[i] == id)
            {
                Console.WriteLine("Product ID already exists");
                return;
            }
        }

        ids[count] = id;

        Console.Write("Enter Name: ");
        names[count] = Console.ReadLine();

        Console.Write("Enter Price: ");
        prices[count] = int.Parse(Console.ReadLine());

        Console.Write("Enter Stock: ");
        stock[count] = int.Parse(Console.ReadLine());

        Console.WriteLine("Product Added");
    }

    // View Products
    static void ViewProducts(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.WriteLine("ID  Name  Price  Stock");

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(ids[i] + "  " + names[i] + "  " + prices[i] + "  " + stock[i]);
        }
    }

    // Search Product
    static int SearchProduct(int[] ids, int count, int id)
    {
        for (int i = 0; i < count; i++)
        {
            if (ids[i] == id)
                return i;
        }

        return -1;
    }

    // Update Product
    static void UpdateProduct(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.Write("Enter Product ID: ");
        int id = int.Parse(Console.ReadLine());

        int index = SearchProduct(ids, count, id);

        if (index == -1)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.Write("New Price: ");
        prices[index] = int.Parse(Console.ReadLine());

        Console.Write("New Stock: ");
        stock[index] = int.Parse(Console.ReadLine());

        Console.WriteLine("Product Updated");
    }

    // Delete Product
    static void DeleteProduct(int[] ids, string[] names, int[] prices, int[] stock, int count)
    {
        Console.Write("Enter Product ID: ");
        int id = int.Parse(Console.ReadLine());

        int index = SearchProduct(ids, count, id);

        if (index == -1)
        {
            Console.WriteLine("Product not found");
            return;
        }

        if (index == 0 && count > 1)
        {
            ids[0] = ids[1];
            names[0] = names[1];
            prices[0] = prices[1];
            stock[0] = stock[1];
        }
        else if (index == 1 && count > 2)
        {
            ids[1] = ids[2];
            names[1] = names[2];
            prices[1] = prices[2];
            stock[1] = stock[2];
        }

        Console.WriteLine("Product Deleted");
    }

    static void Main()
    {
        // User Data
        string[] usernames = new string[10];
        string[] passwords = new string[10];
        int userCount = 0;
        int loggedUser = -1;

        // Product Data
        int[] productIDs = new int[50];
        string[] productNames = new string[50];
        int[] productPrices = new int[50];
        int[] productStock = new int[50];
        int productCount = 0;

        int choice = 0;

        while (choice != 4)
        {
            TopHeader();
            MainMenu();
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                userCount = SignUp(usernames, passwords, userCount);
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

                    if (role == 1)
                    {
                        int aChoice = 0;

                        while (aChoice != 6)
                        {
                            AdminMenu();
                            aChoice = int.Parse(Console.ReadLine());

                            if (aChoice == 1)
                            {
                                AddProduct(productIDs, productNames, productPrices, productStock, productCount);
                                productCount++;
                            }
                            else if (aChoice == 2)
                                ViewProducts(productIDs, productNames, productPrices, productStock, productCount);

                            else if (aChoice == 3)
                                UpdateProduct(productIDs, productNames, productPrices, productStock, productCount);

                            else if (aChoice == 4)
                                DeleteProduct(productIDs, productNames, productPrices, productStock, productCount);

                            else if (aChoice == 6)
                                Console.WriteLine("Logging out");
                        }
                    }
                    else if (role == 2)
                    {
                        int cChoice = 0;

                        while (cChoice != 3)
                        {
                            CustomerMenu();
                            cChoice = int.Parse(Console.ReadLine());

                            if (cChoice == 1)
                                ViewProducts(productIDs, productNames, productPrices, productStock, productCount);

                            else if (cChoice == 2)
                            {
                                Console.Write("Enter Product ID: ");
                                int id = int.Parse(Console.ReadLine());

                                int index = SearchProduct(productIDs, productCount, id);

                                if (index != -1)
                                {
                                    Console.WriteLine("Product Name : " + productNames[index]);
                                    Console.WriteLine("Product Price: " + productPrices[index]);
                                    Console.WriteLine("Product Stock: " + productStock[index]);
                                }
                                else
                                {
                                    Console.WriteLine("Product not found");
                                }
                            }
                        }
                    }
                }
            }
            else if (choice == 4)
            {
                Console.WriteLine("Program Closed");
            }
        }
    }
}
