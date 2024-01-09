using EquityX.Services;
using Microsoft.Maui.ApplicationModel.Communication;

namespace EquityX.View;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        Routing.RegisterRoute("register", typeof(RegisterPage));
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text;
        var password = passwordEntry.Text;

        // Check if login is valid

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
        UserSession.ClearUserSession();
        var dataService = new DatabaseContext();
        bool isValidUser = await dataService.ValidateCredentialsAsync(email, password);

        if (isValidUser)
        {
            // Login successful
            // Assuming you have a method to get the user's ID
            var userId = await dataService.GetUserIdByEmail(email);
            UserSession.SetCurrentUser(userId.ToString(), email);
            var currentEmail = UserSession.CurrentUserEmail;
            UserSession.SetCurrentUser(userId.ToString(), currentEmail);
            await UserSession.SaveSessionAsync();
            await Application.Current.MainPage.DisplayAlert("Login Successful", "Welcome back "+currentEmail+"! You are now logged in.", "OK");
            await Shell.Current.GoToAsync("//MainPage");

        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Login Unsuccessful", "Try again", "OK");
        }
    }

    private async Task<bool> IsLoginValid(string username, string password)
    {
        DatabaseContext db = new DatabaseContext(); 
        return await db.ValidateCredentialsAsync(username, password);
    }

    private async void OnRegisterLinkClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("register");
    }
}