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
        double addedFunds = Convert.ToDouble(test);

        // Send a message with the added funds
        try
        {
            await Application.Current.MainPage.DisplayAlert("Transaction Complete", addedFunds+"added!", "OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
        }

        await Shell.Current.GoToAsync("home");
    }
}