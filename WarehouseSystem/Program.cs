using System;

public static class Program
{
    public static void Main()
    {
        var manager = new WareHouseManager();
        manager.SeedData();


        manager.RunInteractive();
    }
}
