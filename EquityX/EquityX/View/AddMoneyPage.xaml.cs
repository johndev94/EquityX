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

        double test1 = Convert.ToDouble(test);
        Console.WriteLine("test" + test1);
        await Shell.Current.GoToAsync("home");
    }
}