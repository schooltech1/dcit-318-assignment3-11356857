using System;
using System.Collections.Generic;

public class InventoryApp
{
    private List<InventoryItem> items;
    private readonly InventoryLogger<InventoryItem> logger;
    private readonly string filePath = "inventory.txt";

    public InventoryApp()
    {
        logger = new InventoryLogger<InventoryItem>(filePath);
        items = SeedSampleData();
    }

    private List<InventoryItem> SeedSampleData()
    {
        return new List<InventoryItem>
        {
            new(1, "Laptop", 5, DateTime.Now.AddDays(-10)),
            new(2, "Monitor", 3, DateTime.Now.AddDays(-5)),
            new(3, "Keyboard", 10, DateTime.Now.AddDays(-2))
        };
    }


    public void AddItemInteractive()
    {
        try
        {
            Console.Write("Enter Item ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine(" Invalid ID. Please enter a number.");
                return;
            }

            // Check for duplicate ID
            if (items.Exists(i => i.Id == id))
            {
                Console.WriteLine($" Item with ID {id} already exists.");
                return;
            }

            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine(" Name cannot be empty.");
                return;
            }

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine(" Invalid Quantity. Must be a positive number.");
                return;
            }

            items.Add(new InventoryItem(id, name, quantity, DateTime.Now));
            Console.WriteLine(" Item added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error: {ex.Message}");
        }
    }

    public void Save()
    {
        logger.SaveInventory(items);
        Console.WriteLine("Inventory saved to file.");
    }

    public void Load()
    {
        items = logger.LoadInventory(line =>
        {
            var parts = line.Split(',');
            return new InventoryItem(
                int.Parse(parts[0]),
                parts[1],
                int.Parse(parts[2]),
                DateTime.Parse(parts[3])
            );
        });
        Console.WriteLine("Inventory loaded from file.");
    }

    public void Display()
    {
        Console.WriteLine("\n--- Inventory List ---");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Id} | {item.Name} | Qty: {item.Quantity} | Added: {item.DateAdded}");
        }
    }
}
