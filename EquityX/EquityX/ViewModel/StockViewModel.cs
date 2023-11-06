using EquityX.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.ViewModel;

public class StockViewModel : BaseViewModel
{
    // A stock service that retrieves a list of stocks from somewhere
    StockService _stockService;

    // A collection to store the stock
    public ObservableCollection<Model.Stocks> Stocks { get; } = new();

    // Create a GetStocksCommand that the view can use to get stocks
    public Command GetStocksCommand { get; }
    
    // Constructor for the StockViewModel initialises the StockService and the GetStockCommand
    public StockViewModel(StockService stockService)
    {
        _stockService = stockService;

        // Initialise the command telling it to call the GetStocks Task
        GetStocksCommand = new Command(async () => await GetStocksAsync());
    }

    // The GetStocksAsync task retrieves a list of people from the stockService and adds them to the stocks collection

    async Task GetStocksAsync()
    {
        if(IsBusy) 
            return;
        else
            try
            {
               IsBusy = true;
                var stocks = await _stockService.GetStockFileAsync();
                if(stocks.Count != 0)        
                    Stocks.Clear();
                foreach(var stock in stocks)
                {
                    Stocks.Add(stock);
                }
                
            }
            catch (Exception ex)
            {
                // Display an alert if an exception is raised
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to retrieve stocks", "OK");
            }
            finally
            {
                // Finish/clean up code in here
                IsBusy = false;
            }
    }
}
