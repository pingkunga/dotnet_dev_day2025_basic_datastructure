using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class CompanyDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is CompanyDTO other)
        {
            return Id == other.Id && Name == other.Name;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}

public class Program
{
	private static List<CompanyDTO> Sourcels;
    private static List<CompanyDTO> Destinationls;
	
	private static HashSet<CompanyDTO> SourceSet;
	private static HashSet<CompanyDTO> DestinationSet;
	
	//this simulate moverbox 
	//change parammeter 
	private static int n_data = 200000;
	private static int n_select = 190000;
	
    public static void Main(string[] args)
    {
		Sourcels = new List<CompanyDTO>();
        Destinationls = new List<CompanyDTO>();
		InitializeSourceList();
		
		SourceSet = new HashSet<CompanyDTO>(Sourcels);
		DestinationSet = new HashSet<CompanyDTO>();
        
		List<CompanyDTO> SelectItem = SelectNItem(n_select);
		
		// Start the stopwatch to measure the process time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
		MoveSourceList2DestinationList(SelectItem);
		stopwatch.Stop();
        Console.WriteLine($"Process_List Time: {stopwatch.ElapsedMilliseconds} ms");
		
		Console.WriteLine($"Source List: {Sourcels.Count}");
		Console.WriteLine($"Dest List: {Destinationls.Count}");
		
		Console.WriteLine();
		
		stopwatch.Reset();
		stopwatch.Start();
		MoveSourceSet2DestinationSet(SelectItem);
		
		stopwatch.Stop();
        Console.WriteLine($"Process Set Time: {stopwatch.ElapsedMilliseconds} ms");
		Console.WriteLine($"Source Set: {SourceSet.Count}");
		Console.WriteLine($"Dest Set: {DestinationSet.Count}");
		
    }
	
	public static void InitializeSourceList()
    {
        for (int i = 1; i <= n_data; i++)
        {
            Sourcels.Add(new CompanyDTO { Id = i, Name = $"Company {i}" });
        }
    }
	
	public static List<CompanyDTO> SelectNItem(int pSelect)
	{
		return Sourcels.Take(pSelect).ToList();
	}
	
	//simluate in user select < >
	public static void MoveSourceList2DestinationList(List<CompanyDTO> pSelectedItem)
	{
		foreach (var item in pSelectedItem)
        {
            Sourcels.Remove(item);
			Destinationls.Add(item);
        }
		
		//fast way if move all 
		//secondList.AddRange(firstList);
        //firstList.Clear();
	}
	
    public static void MoveSourceSet2DestinationSet(List<CompanyDTO> pSelectedItem)
	{
	    foreach (var item in pSelectedItem)
        {
            SourceSet.Remove(item);
			DestinationSet.Add(item);
        }
	}
}