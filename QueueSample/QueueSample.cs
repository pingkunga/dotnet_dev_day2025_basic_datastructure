using System;
using System.Collections.Generic;

public class PortfolioDTO
{
    public int PortfolioId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}

public class PortfolioQueue
{
    private Queue<PortfolioDTO> portfolioQueue = new Queue<PortfolioDTO>();

    // Enqueue (Add a new portfolio to the queue)
    public void EnqueuePortfolio(PortfolioDTO portfolio)
    {
        portfolioQueue.Enqueue(portfolio);
    }

    // Dequeue (Remove and return the portfolio at the front of the queue)
    public PortfolioDTO DequeuePortfolio()
    {
        if (portfolioQueue.Count > 0)
        {
            return portfolioQueue.Dequeue();
        }
        else
        {
            throw new InvalidOperationException("Queue is empty");
        }
    }

    // Access (Retrieve all portfolios in the queue)
    public List<PortfolioDTO> GetAllPortfolios()
    {
        return new List<PortfolioDTO>(portfolioQueue);
    }

    // Search (Retrieve a portfolio by ID)
    public PortfolioDTO GetPortfolioById(int id)
    {
        foreach (var portfolio in portfolioQueue)
        {
            if (portfolio.PortfolioId == id)
            {
                return portfolio;
            }
        }
        return null;
    }

    // Get the count of portfolios in the queue
    public int GetPortfolioCount()
    {
        return portfolioQueue.Count;
    }
}

public class Program
{
    public static void Main()
    {
        PortfolioQueue portfolioQueue = new PortfolioQueue();

        portfolioQueue.EnqueuePortfolio(new PortfolioDTO { PortfolioId = 1, Name = "Portfolio 1", Description = "Description 1", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now });
        portfolioQueue.EnqueuePortfolio(new PortfolioDTO { PortfolioId = 2, Name = "Portfolio 2", Description = "Description 2", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now });
        
		Console.WriteLine();
        Console.WriteLine("All Portfolios in Queue:");
        foreach (var portfolio in portfolioQueue.GetAllPortfolios())
        {
            Console.WriteLine($"ID: {portfolio.PortfolioId}, Name: {portfolio.Name}, Description: {portfolio.Description}");
        }
       
        var searchResult = portfolioQueue.GetPortfolioById(1);
		Console.WriteLine();
        Console.WriteLine($"\nSearch Result: ID: {searchResult.PortfolioId}, Name: {searchResult.Name}, Description: {searchResult.Description}");

        var dequeuedPortfolio = portfolioQueue.DequeuePortfolio();
		Console.WriteLine();
        Console.WriteLine($"\nDequeued Portfolio: ID: {dequeuedPortfolio.PortfolioId}, Name: {dequeuedPortfolio.Name}, Description: {dequeuedPortfolio.Description}");

        Console.WriteLine("\nPortfolios left in Queue:");
        foreach (var portfolio in portfolioQueue.GetAllPortfolios())
        {
            Console.WriteLine($"ID: {portfolio.PortfolioId}, Name: {portfolio.Name}, Description: {portfolio.Description}");
        }

        Console.WriteLine($"\nTotal Portfolios in Queue: {portfolioQueue.GetPortfolioCount()}");
    }
}