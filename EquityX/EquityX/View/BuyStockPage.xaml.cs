using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.Messaging;
using EquityX.Messages;
using EquityX.Model;
using EquityX.ViewModel;
using System.Web;

namespace EquityX.View;

public partial class BuyStockPage : ContentPage
{

	public BuyStockPage()
	{
		InitializeComponent();
      //  MainViewModel _viewModel = new MainViewModel(); // Initialize your ViewModel here
       
        BindingContext = new BuyStockViewModel();
       
    }



}