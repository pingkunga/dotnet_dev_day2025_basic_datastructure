using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Perfolizer.Mathematics.SignificanceTesting;
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
    /// <summary>
    ///  run with dotnet run -c Release
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<BenchmarkListVSDictionaryTest>();
    }
}

public class BenchmarkListVSDictionaryTest
{
    private List<TransactionDTO> transactions;
    private List<PortfolioDTO> portfolios;
    private List<PortPriceSouceDTO> portPriceSources;
    private	IDictionary<int,  PortfolioDTO> portDic;
	private	IDictionary<int,  PortPriceSouceDTO> portPriceSourceDic;
    
    [Params(500, 1000, 5000, 10000)]
    public int portfolio_num = 500; //500, 1000 , 1500, 2000

    [GlobalSetup]
    public void Setup()
    {
        // Mocking 1 million transactions
        transactions = new List<TransactionDTO>();
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
        portfolios = new List<PortfolioDTO>();
        for (int i = 0; i < portfolio_num; i++)
        {
            portfolios.Add(new PortfolioDTO
            {
                PortfolioId = i,
                PortfolioName = "Portfolio " + i
            });
        }
		
		//Mocking priceSources
		portPriceSources = new List<PortPriceSouceDTO>();
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

        // Create dictionary from list
        portDic = portfolios.ToDictionary(p => p.PortfolioId);
        portPriceSourceDic = portPriceSources.ToDictionary(p => p.PortfolioId);
    }

    [Benchmark]
    public IList<TransactionDTO> TestProcessTransactionWithOnlyList () => ProcessTransactionWithOnlyList(transactions, portfolios, portPriceSources);
    [Benchmark]
    public IList<TransactionDTO> TestProcessTransactionWithListAndDic () => ProcessTransactionWithListAndDic(transactions, portDic, portPriceSourceDic);

    public static IList<TransactionDTO> ProcessTransactionWithOnlyList(IList<TransactionDTO> pTransactions, IList<PortfolioDTO> pPortfolios, IList<PortPriceSouceDTO> pPortPriceSources)
	{
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

        return pTransactions;
		
	}

	public IList<TransactionDTO> ProcessTransactionWithListAndDic(IList<TransactionDTO> pTransactions, IDictionary<int,  PortfolioDTO> pPortDic, IDictionary<int,  PortPriceSouceDTO> pPortPriceSourceDic)
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

        return pTransactions;
	}
}