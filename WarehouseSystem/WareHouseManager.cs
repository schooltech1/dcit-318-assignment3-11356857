using System;
using System.Collections.Generic;

public class WareHouseManager
{
    private readonly InventoryRepository<ElectronicItem> _electronics = new();
    private readonly InventoryRepository<GroceryItem> _groceries = new();

    // Seed 2–3 items of each type
    public void SeedData()
    {
        try
        {
            _electronics.AddItem(new ElectronicItem(1, "Laptop", 5, "Dell", 24));
            _electronics.AddItem(new ElectronicItem(2, "Smartphone", 10, "Samsung", 12));
            _electronics.AddItem(new ElectronicItem(3, "Headphones", 15, "Sony", 6));

            _groceries.AddItem(new GroceryItem(101, "Rice (5kg)", 20, DateTime.Today.AddMonths(12)));
            _groceries.AddItem(new GroceryItem(102, "Milk (1L)", 30, DateTime.Today.AddMonths(3)));
            _groceries.AddItem(new GroceryItem(103, "Eggs (Tray)", 12, DateTime.Today.AddDays(21)));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SeedData] {ex.Message}");
        }
    }

    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        var items = repo.GetAllItems();
        if (items.Count == 0)
        {
            Console.WriteLine("(none)");
            return;
        }
        foreach (var it in items)
            Console.WriteLine(it);
    }

    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            var item = repo.GetItemById(id); // throws if not found
            var newQty = item.Quantity + quantity;
            repo.UpdateQuantity(id, newQty); // throws if negative
            Console.WriteLine($"Stock increased. {item.Name} new Qty = {newQty}");
        }
        catch (Exception ex) when (ex is ItemNotFoundException || ex is InvalidQuantityException)
        {
            Console.WriteLine($"[IncreaseStock] {ex.Message}");
        }
    }

    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Removed item with Id {id}");
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"[RemoveItem] {ex.Message}");
        }
    }

    // Convenience accessors for printing
    public void PrintAllGroceries() => PrintAllItems(_groceries);
    public void PrintAllElectronics() => PrintAllItems(_electronics);

    // Interactive menu (optional, for exploration beyond the required demo)
    public void RunInteractive()
    {
        while (true)
        {
            Console.WriteLine("\n--- Warehouse Menu ---");
            Console.WriteLine("1) List Groceries");
            Console.WriteLine("2) List Electronics");
            Console.WriteLine("3) Add Grocery");
            Console.WriteLine("4) Add Electronic");
            Console.WriteLine("5) Increase Grocery Stock");
            Console.WriteLine("6) Increase Electronic Stock");
            Console.WriteLine("7) Remove Grocery");
            Console.WriteLine("8) Remove Electronic");
            Console.WriteLine("9) Exit");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        PrintAllGroceries();
                        break;
                    case "2":
                        PrintAllElectronics();
                        break;
                    case "3":
                        Console.Write("Id: ");
                        int gid = int.Parse(Console.ReadLine()!);
                        Console.Write("Name: ");
                        string gname = Console.ReadLine()!;
                        Console.Write("Qty: ");
                        int gqty = int.Parse(Console.ReadLine()!);
                        Console.Write("Expiry (yyyy-mm-dd): ");
                        DateTime gexp = DateTime.Parse(Console.ReadLine()!);
                        _groceries.AddItem(new GroceryItem(gid, gname, gqty, gexp));
                        Console.WriteLine("Grocery added.");
                        break;
                    case "4":
                        Console.Write("Id: ");
                        int eid = int.Parse(Console.ReadLine()!);
                        Console.Write("Name: ");
                        string ename = Console.ReadLine()!;
                        Console.Write("Qty: ");
                        int eqty = int.Parse(Console.ReadLine()!);
                        Console.Write("Brand: ");
                        string brand = Console.ReadLine()!;
                        Console.Write("Warranty months: ");
                        int warr = int.Parse(Console.ReadLine()!);
                        _electronics.AddItem(new ElectronicItem(eid, ename, eqty, brand, warr));
                        Console.WriteLine("Electronic added.");
                        break;
                    case "5":
                        Console.Write("Grocery Id: ");
                        int ig = int.Parse(Console.ReadLine()!);
                        Console.Write("Increase by: ");
                        int addg = int.Parse(Console.ReadLine()!);
                        IncreaseStock(_groceries, ig, addg);
                        break;
                    case "6":
                        Console.Write("Electronic Id: ");
                        int ie = int.Parse(Console.ReadLine()!);
                        Console.Write("Increase by: ");
                        int adde = int.Parse(Console.ReadLine()!);
                        IncreaseStock(_electronics, ie, adde);
                        break;
                    case "7":
                        Console.Write("Grocery Id: ");
                        int rg = int.Parse(Console.ReadLine()!);
                        RemoveItemById(_groceries, rg);
                        break;
                    case "8":
                        Console.Write("Electronic Id: ");
                        int re = int.Parse(Console.ReadLine()!);
                        RemoveItemById(_electronics, re);
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"[Add] {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format.");
            }
        }
    }
}
