using EquityX.Services;

namespace EquityX.View;

public partial class AddMoneyPage : ContentPage
{
    
	public AddMoneyPage()
	{
		InitializeComponent();

        btnAddFunds.Clicked += OnbtnAddFunds_Clicked;

        Routing.RegisterRoute("home", typeof(HomePage));
        
    }

    private async void OnbtnAddFunds_Clicked(object sender, EventArgs e)
    {
        string test = entry.Text;
        double addedFunds;

        if (!double.TryParse(test, out addedFunds))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid amount entered.", "OK");
            return;
        }

        var user = UserSession.CurrentUserId;
        var success = false;
        if (int.TryParse(UserSession.CurrentUserId, out int userId))
        {
            var databaseContext = new DatabaseContext();
            success = await databaseContext.UpdateUserBalance(userId, addedFunds);
        }
        

        if (success)
        {
            await Application.Current.MainPage.DisplayAlert("Transaction Complete", "$" + addedFunds + " added!", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Could not update balance.", "OK");
        }

        await Shell.Current.GoToAsync("home");
    }

}