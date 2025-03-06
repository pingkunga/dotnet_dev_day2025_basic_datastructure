using System;
using System.Collections.Generic;

namespace HashSetExample
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Override Equals method
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ItemDTO other = (ItemDTO)obj;
            return Id == other.Id && Name == other.Name;
        }

        // Override GetHashCode method
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a HashSet of ItemDTO
            HashSet<ItemDTO> items = new HashSet<ItemDTO>();

            // Insert elements into the HashSet
            items.Add(new ItemDTO { Id = 1, Name = "Item1" });
            items.Add(new ItemDTO { Id = 2, Name = "Item2" });
            items.Add(new ItemDTO { Id = 3, Name = "Item3" });
            items.Add(new ItemDTO { Id = 4, Name = "Item4" });
            items.Add(new ItemDTO { Id = 5, Name = "Item5" });
			items.Add(new ItemDTO { Id = 1, Name = "Item1" });

            // Access and display elements in the HashSet
			Console.WriteLine();
            Console.WriteLine("Elements in the HashSet:");
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }

            // Search for an element in the HashSet
			Console.WriteLine();
            var searchItem = new ItemDTO { Id = 3, Name = "Item3" };
            if (items.Contains(searchItem))
            {
                Console.WriteLine($"HashSet contains the item with Id {searchItem.Id} and Name {searchItem.Name}");
            }
            else
            {
                Console.WriteLine($"HashSet does not contain the item with Id {searchItem.Id} and Name {searchItem.Name}");
            }

            // Delete an element from the HashSet
			Console.WriteLine();
            var deleteItem = new ItemDTO { Id = 4, Name = "Item4" };
            if (items.Remove(deleteItem))
            {
                Console.WriteLine($"Item with Id {deleteItem.Id} and Name {deleteItem.Name} was removed from the HashSet");
            }
            else
            {
                Console.WriteLine($"Item with Id {deleteItem.Id} and Name {deleteItem.Name} was not found in the HashSet");
            }

            // Display elements after deletion
			Console.WriteLine();
            Console.WriteLine("Elements in the HashSet after deletion:");
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }
        }
    }
}