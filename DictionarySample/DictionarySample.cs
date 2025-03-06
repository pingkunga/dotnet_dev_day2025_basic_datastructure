using System;
using System.Collections.Generic;

public class StockMasterDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public StockMasterDTO(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create a dictionary to hold StockMasterDTO objects
        Dictionary<int, StockMasterDTO> stockDictionary = new Dictionary<int, StockMasterDTO>();

		
        // Add stock items to the dictionary
		Console.WriteLine($"Initial Dic");
        stockDictionary.Add(1, new StockMasterDTO(1, "AAPL", 150.00));
        stockDictionary.Add(2, new StockMasterDTO(2, "MSFT", 250.00));
        stockDictionary.Add(3, new StockMasterDTO(3, "GOOG", 200.00));

        // Access a stock item by key
		Console.WriteLine();
        int keyToAccess = 2;
        if (stockDictionary.TryGetValue(keyToAccess, out StockMasterDTO stockItem))
        {
            Console.WriteLine($"Accessed: {stockItem}");
        }
        else
        {
            Console.WriteLine($"Stock item with ID {keyToAccess} not found.");
        }
	
        // Remove a stock item by key
		Console.WriteLine();
        int keyToRemove = 1;
        if (stockDictionary.Remove(keyToRemove))
        {
            Console.WriteLine($"Removed stock item with ID {keyToRemove}.");
        }
        else
        {
            Console.WriteLine($"Stock item with ID {keyToRemove} not found.");
        }

        // Print all stock items in the dictionary
		Console.WriteLine();
        Console.WriteLine("All stock items in the dictionary:");
        foreach (var kvp in stockDictionary)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}