using Microcharts;
using SkiaSharp;
using EquityX.Services;
using System.Security.Cryptography.X509Certificates;

namespace EquityX.View;

public partial class HomePage : ContentPage
{

    private double _balance;
    public double Balance
    {
        get { return _balance; }
        set
        {
            _balance = value;
            OnPropertyChanged(nameof(Balance));
        }
    }

    private double _portfollio;
    public double Portfollio
    {
        get { return _portfollio; }
        set
        {
            _portfollio = Math.Round(value, 2);
            OnPropertyChanged(nameof(Portfollio));
        }
    }

    ChartEntry[] entries = new[]
        {
            new ChartEntry(212)
            {
                Label = "Amazon",
                ValueLabel = "112",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(300)
            {
                Label = "Microsoft",
                ValueLabel = "300",
                Color = SKColor.Parse("#2c0050")
            },
            new ChartEntry(250)
            {
                Label = "Google",
                ValueLabel = "112",
                Color = SKColor.Parse("#2c1150")
            },
            new ChartEntry(112)
            {
                Label = "Tesla",
                ValueLabel = "222",
                Color = SKColor.Parse("#2c5550")
            }
        };
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeBalance();
        await InitializePortfollio();
    }
    public HomePage()
	{
		InitializeComponent();

        var databaseContext = new DatabaseContext();
        InitializeBalance();
        InitializePortfollio();
        this.BindingContext = this;

        

        btnAddMoney.Clicked += OnbtnAddMoney_Clicked;
		btnWithdraw.Clicked += OnbtnWitdraw_Clicked;

		Routing.RegisterRoute("add", typeof(AddMoneyPage));
        Routing.RegisterRoute("withdraw", typeof(WithdrawPage));

        chartView.Chart = new BarChart
        {
            Entries = entries
        };
        chartView1.Chart = new BarChart
        {
            Entries = entries
        };

    }
    private async Task InitializeBalance()
    {
      
        
        if (int.TryParse(UserSession.CurrentUserId, out int userId))
        {
            var databaseContext = new DatabaseContext();
            Balance = await databaseContext.GetBalanceByUserId(userId);
        }
        else
        {
            Console.WriteLine("Could not get user");
        }
    }
    private async Task InitializePortfollio()
    {


        if (int.TryParse(UserSession.CurrentUserId, out int userId))
        {
            var databaseContext = new DatabaseContext();
            Portfollio = await databaseContext.GetPortfollioByUserId(userId);
        }
        else
        {
            Console.WriteLine("Could not get user");
        }
    }

    private async void OnbtnAddMoney_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("add");
	}

	private async void OnbtnWitdraw_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("withdraw");
    }





}