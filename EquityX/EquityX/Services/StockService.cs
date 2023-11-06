using EquityX.Model;
using System.Text.Json;

namespace EquityX.Services;

public class StockService
{ 
    // List of Stock objects
    List<Stocks> _stocksLists = new List<Stocks>();
    public StockService()
    {

    }
    // The GetStockFileAsync task reads a list of stocks from a local JSON file and returns a list of people objects
    public async Task<List<Stocks>> GetStockFileAsync()
    {
        if(_stocksLists.Count > 0)
        {
            return _stocksLists;
        }
        // Load the JSON data from file
        using var stream = await FileSystem.OpenAppPackageFileAsync("sample-stocks-data.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        _stocksLists = JsonSerializer.Deserialize<List<Stocks>>(contents);

        // Return the list of People
        return _stocksLists;
    }
}

