using System;

public class ElectronicItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public string Brand { get; }
    public int WarrantyMonths { get; }

    public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }

    public override string ToString()
        => $"[Electronic] Id={Id}, Name={Name}, Brand={Brand}, Qty={Quantity}, Warranty={WarrantyMonths} months";
}