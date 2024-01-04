using EquityX.Model;
using System.Text.Json;
using WebAPITest;

namespace EquityX.Services;

public class StockService
{ 
    // List of Stock objects
    List<Stocks> _stocksLists = new List<Stocks>();
    public StockService()
    {

    }
    // The GetStockFileAsync task reads a list of stocks from a local JSON file and returns a list of people objects
    public async Task<List<Result>> GetStocks(string stocks)
    {
        WebDataManager webDataManager = new WebDataManager();
        List<Result> StockData = await webDataManager.GetQuote(stocks);
        return StockData;
    }
}

