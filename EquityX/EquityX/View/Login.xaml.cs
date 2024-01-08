using EquityX.Services;
using Microsoft.Maui.ApplicationModel.Communication;

namespace EquityX.View;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text;
        var password = passwordEntry.Text;

        // Here, add your logic to verify the username and password
        // For example, check against a database or an authentication service

        if (await IsLoginValid(username, password))
        {
            await LoginUser(username, password);
        }
        else
        {
            Console.WriteLine("Login failed: Invalid username or password.");
        }
    }

    public async Task LoginUser(string email, string password)
    {
        var dataService = new DatabaseContext();
        bool isValidUser = await dataService.ValidateCredentialsAsync(email, password);

        if (isValidUser)
        {
            // Login successful
            // Assuming you have a method to get the user's ID
            var userId = await dataService.GetUserIdByEmail(email);
            UserSession.SetCurrentUser(userId.ToString(), email);
            var currentEmail = UserSession.CurrentUserEmail;
            await Application.Current.MainPage.DisplayAlert("Login Successful", "Welcome back "+currentEmail+"! You are now logged in.", "OK");

        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Login Successful", "Welcome back! You are now logged in.", "OK");
        }
    }

    private async Task<bool> IsLoginValid(string username, string password)
    {
        DatabaseContext db = new DatabaseContext(); 
        return await db.ValidateCredentialsAsync(username, password);
    }
}