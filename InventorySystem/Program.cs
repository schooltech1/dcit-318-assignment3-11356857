using System;

class Program
{
    static void Main()
    {
        var app = new InventoryApp();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nInventory Menu:");
            Console.WriteLine("1. Display Items");
            Console.WriteLine("2. Add Item");
            Console.WriteLine("3. Save Inventory");
            Console.WriteLine("4. Load Inventory");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    app.Display();
                    break;
                case "2":
                    app.AddItemInteractive();
                    break;
                case "3":
                    app.Save();
                    break;
                case "4":
                    app.Load();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
