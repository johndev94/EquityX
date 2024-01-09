using EquityX.ViewModel;
using Microcharts;
using SkiaSharp;

namespace EquityX.View;

public partial class PortfolioPage : ContentPage
{
    public PortfolioPage()
    {
        InitializeComponent();
        this.BindingContext = new MainViewModel();
        LoadStocks();
    }

    private async void LoadStocks()
    {
        var viewModel = (MainViewModel)this.BindingContext;
        await viewModel.GetAllStocksAsync();
    }
}