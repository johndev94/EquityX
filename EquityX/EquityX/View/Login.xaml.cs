using EquityX.Services;
using Windows.Networking.NetworkOperators;

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

        if (IsLoginValid(username, password))
        {
            // Navigate to the next page or show a success message
        }
        else
        {
            // Show an error message
        }
    }

    public async Task LoginUser(string email, string password)
    {
        var dataService = new DatabaseContext();
        bool isValidUser = await dataService.ValidateCredentialsAsync(email, password);

        if (isValidUser)
        {
            // Login successful
            // Set user session data, navigate to the main page, etc.
            // For example:
            UserSession.CurrentUser = email; // Assume you have a UserSession class to handle logged-in user data
                                             // Navigate to the main application page
                                             // NavigationService.NavigateTo("MainPage"); // This will depend on your navigation service
                                             // Display a welcome message
                                             // DisplayAlert("Login Successful", "Welcome back!", "OK");
        }
        else
        {
            // Login failed
            // Inform the user that login has failed
            // DisplayAlert("Login Failed", "Incorrect email or password. Please try again.", "OK");
        }
    }

    private bool IsLoginValid(string username, string password)
    {
        // Implement your validation logic here
        return true; // Placeholder
    }
}