using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.Messaging;
using EquityX.Messages;
using EquityX.Model;
using EquityX.Services;
using EquityX.ViewModel;
using System.Web;
using static Java.Util.Jar.Attributes;

namespace EquityX.View;

public partial class BuyStockPage : ContentPage
{

	public BuyStockPage()
	{
		InitializeComponent();
      //  MainViewModel _viewModel = new MainViewModel(); // Initialize your ViewModel here
       
        BindingContext = new BuyStockViewModel();
        btnBuyStock.Clicked += btnBuyStock_Clicked;
    }

    private async void btnBuyStock_Clicked(object sender, EventArgs e)
    {
        string longName = LongName.Text;
        double minusFunds = double.Parse(Price.Text) * -1;
        string shortName = ShortName.Text;



        var user = UserSession.CurrentUserId;
        var success = false;
        if (int.TryParse(UserSession.CurrentUserId, out int userId))
        {
            var databaseContext = new DatabaseContext();
            if (await databaseContext.GetBalanceByUserId(userId) > (minusFunds * -1))
            {
                success = await databaseContext.UpdateUserBalance(userId, minusFunds);
                await databaseContext.UpdatePortfollio(userId, (minusFunds * -1));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Balance too low", "OK");
            }
        }


        if (success)
        {
            await Application.Current.MainPage.DisplayAlert("Transaction Complete", "" + minusFunds*-1 + " "+ longName+ " added!", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Could not update balance.", "OK");
        }

        //await Shell.Current.GoToAsync("home");
    }




}