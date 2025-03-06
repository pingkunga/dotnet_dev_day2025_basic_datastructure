using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class TransactionDTO
{
    public int TransactionId { get; set; }
    public int PortfolioId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

public class PortfolioDTO
{
    public int PortfolioId { get; set; }
    public string PortfolioName { get; set; }
}

public class PortPriceSouceDTO
{
	public int PortfolioId { get; set; }
	public IList<String> EquitySources { get; set; }
	public IList<String> FixedIncomeSources { get; set; }
	public IList<String> FxRateSources { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
		int portfolio_num = 5000; //500, 1000 , 1500, 2000
        // Mocking 1 million transactions
        List<TransactionDTO> transactions = new List<TransactionDTO>();
        for (int i = 0; i < 1000000; i++)
        {
            transactions.Add(new TransactionDTO
            {
                TransactionId = i,
                PortfolioId = i % portfolio_num,
                Amount = new Random().Next(1000, 10000),
                Date = DateTime.Now
            });
        }

        // Mocking portfolios
        List<PortfolioDTO> portfolios = new List<PortfolioDTO>();
        for (int i = 0; i < portfolio_num; i++)
        {
            portfolios.Add(new PortfolioDTO
            {
                PortfolioId = i,
                PortfolioName = "Portfolio " + i
            });
        }
		
		//Mocking priceSources
		List<PortPriceSouceDTO> portPriceSources = new List<PortPriceSouceDTO>();
		for (int i = 0; i < portfolio_num; i++)
        {
            portPriceSources.Add(new PortPriceSouceDTO
            {
                PortfolioId = i,
                EquitySources = ["SET", "MARKETMAKER"],
				FixedIncomeSources = ["TBMA", "BOT_REPO", "BLOOMBERG"],
				FxRateSources =  ["REUTERS", "BLOOMBERG", "BOT_FX"]
            });
        }

		// Start the stopwatch to measure the process time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        ProcessTransactionWithOnlyList(transactions, portfolios, portPriceSources);
        // Stop the stopwatch and print the elapsed time
        stopwatch.Stop();
        Console.WriteLine($"ProcessUseOnlyList Time: {stopwatch.ElapsedMilliseconds} ms");
		
		
		IDictionary<int,  PortfolioDTO> portDic = portfolios.ToDictionary(x => x.PortfolioId, x => x);
		IDictionary<int,  PortPriceSouceDTO> portPriceSourceDic = portPriceSources.ToDictionary(x => x.PortfolioId, x => x);
		stopwatch.Reset();
		stopwatch.Start();
		ProcessTransactionWithListAndDic(transactions, portDic, portPriceSourceDic);
		stopwatch.Stop();
        Console.WriteLine($"ProcessTransactionWithListAndDic Time: {stopwatch.ElapsedMilliseconds} ms");
    }
	
    public static void ProcessTransactionWithOnlyList(IList<TransactionDTO> pTransactions, IList<PortfolioDTO> pPortfolios, IList<PortPriceSouceDTO> pPortPriceSources)
	{
		//PortfolioList.ToDictionary(x => x.PortfolioId.GetValueOrDefault(0), x => x);
        // Iterate through transactions and find corresponding portfolio
        foreach (var transaction in pTransactions)
        {
            // Use LINQ to find the portfolio with the matching PortfolioId
            var portfolio = pPortfolios.FirstOrDefault(p => p.PortfolioId == transaction.PortfolioId);
            if (portfolio != null)
            {
                // You can save or process the transaction with the portfolio here
                //....
            }
			
			var portPriceSource = pPortPriceSources.FirstOrDefault(p => p.PortfolioId == transaction.PortfolioId);
            if (portPriceSource != null)
            {
                // You can save or process the transaction with the portfolio here
                //....
            }
        }
		
	}
	
	public static void ProcessTransactionWithListAndDic(IList<TransactionDTO> pTransactions, IDictionary<int,  PortfolioDTO> pPortDic, IDictionary<int,  PortPriceSouceDTO> pPortPriceSourceDic)
	{
		foreach (var transaction in pTransactions)
        {
			var portfolio = pPortDic.ContainsKey(transaction.PortfolioId) ? pPortDic[transaction.PortfolioId] : null;
            if (portfolio != null)
            {
                // You can save or process the transaction with the portfolio here
                //....
            }
			
			var portPriceSource = pPortPriceSourceDic.ContainsKey(transaction.PortfolioId) ? pPortPriceSourceDic[transaction.PortfolioId] : null;
            if (portPriceSource != null)
            {
                // You can save or process the transaction with the portfolio here
                //....
            }
        }
	}
}