using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class InventoryLogger<T> where T : IInventoryEntity
{
    private readonly string filePath;

    public InventoryLogger(string filePath)
    {
        this.filePath = filePath;
    }

  

    public void SaveInventory(List<T> items)
    {
        try
        {
            using var writer = new StreamWriter(filePath);
            foreach (var item in items)
            {
                writer.WriteLine($"{item.Id},{item.Name},{item.Quantity},{item.DateAdded}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to save inventory: {ex.Message}");
        }
    }

    public List<T> LoadInventory(Func<string, T> converter)
    {
        var items = new List<T>();
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("⚠ No saved file found.");
                return items;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    items.Add(converter(line));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to load inventory: {ex.Message}");
        }
        return items;
    }

}
