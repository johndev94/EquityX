namespace EquityX.View;
using EquityX.ViewModel;
using System;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
		NoBitches();
    }


	public void NoBitches()
	{
		while (true)
		{
            Console.WriteLine("You get 0");
        }
		
	}
}